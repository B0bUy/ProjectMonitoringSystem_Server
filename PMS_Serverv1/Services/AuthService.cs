using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PMS_Serverv1.Data;
using PMSv1_Shared.Entities.UserManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMS_Serverv1.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> Register(RegisterRequest user)
        {
            try
            {
                if (_context.Users.Any(u => u.Username.ToLower() == user.Username.ToLower()))
                    throw new Exception("Username already exists.");

                if (_context.Users.Any(u => u.Email.ToLower() == user.Email.ToLower()))
                    throw new Exception("Email already exists.");

                var newUser = new User
                {
                    Username = user.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Email = user.Email
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return newUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new();
            }
        }

        public async Task<User> Authenticate(string username, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    throw new Exception("Invalid username or password.");
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new();
            }
        }

        public async Task ForgotPassword(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                    throw new Exception("User not found.");

                user.ResetToken = Guid.NewGuid().ToString();
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task ResetPassword(string email, string token, string newPassword)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.ResetToken == token);

                if (user == null)
                    throw new Exception("Invalid token.");

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.ResetToken = string.Empty;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
