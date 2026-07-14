using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TicketCentral.Infrastructure.Data;
using TicketCentral.Modules.Auth.DTOs;
using TicketCentral.Infrastructure.Models;
using TicketCentral.Modules.Auth.Services;
using Microsoft.Extensions.Logging;

namespace TicketCentral.Modules.Auth.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly JwtService _jwtService;
    private readonly RefreshTokenService _refreshTokenService;
    private readonly PasswordService _passwordService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        AppDbContext dbContext,
        JwtService jwtService,
        RefreshTokenService refreshTokenService,
        PasswordService passwordService,
        ILogger<AuthController> logger)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
        _refreshTokenService = refreshTokenService;
        _passwordService = passwordService;
        _logger = logger;
    }

    // ---------------- SIGNUP ----------------
    [HttpPost("signup")]
    public async Task<IActionResult> Register(SignUpDTO request)
    {
        try
        {
            _logger.LogInformation("Register attempt for {Email}", request.Email);

            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed: user exists {Email}", request.Email);
                return BadRequest("User already exists");
            }

            var passwordHash = _passwordService.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Password = passwordHash,
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow,
                IsLoggedIn = false
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("User registered successfully {Email}", request.Email);

            return Ok("User registered successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            return StatusCode(500, "Internal server error");
        }
    }

    // ---------------- LOGIN ----------------
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO request)
    {
        try
        {
            _logger.LogInformation("Login attempt {Email}", request.Email);

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                _logger.LogWarning("Login failed: user not found {Email}", request.Email);
                return Unauthorized("Invalid credentials");
            }

            var isValidPassword = _passwordService.VerifyPassword(request.Password, user.Password);

            if (!isValidPassword)
            {
                _logger.LogWarning("Login failed: invalid password {Email}", request.Email);
                return Unauthorized("Invalid credentials");
            }

            var accessToken = _jwtService.GenerateToken(user);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            user.IsLoggedIn = true;
            user.LastLoginAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Login successful {Email}", request.Email);

            return Ok(new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return StatusCode(500, "Internal server error");
        }
    }

    // ---------------- REFRESH TOKEN ----------------
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(TokenRequestDTO request)
    {
        try
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == request.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            var newAccessToken = _jwtService.GenerateToken(user);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Token refreshed for {Email}", user.Email);

            return Ok(new AuthResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during token refresh");
            return StatusCode(500, "Internal server error");
        }
    }

    // ---------------- LOGOUT ----------------
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] TokenRequestDTO request)
    {
        try
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == request.RefreshToken);

            if (user == null)
                return Unauthorized("Invalid token");

            // invalidate refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            user.IsLoggedIn = false;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("User logged out {Email}", user.Email);

            return Ok("Logged out successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout");
            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- UPDATE USER DETAILS ----------------
    [Authorize]
    [HttpPut("userDetails/{id:guid}")]
    public async Task<IActionResult> UpdateUserDetails(Guid id, UpdateUserDetailsDto request)
    {
        try
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
                return NotFound("user not found");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Gender = request.Gender;
            user.IDPassportNumber = request.IDPassportNumber;
            user.DateOfBirth = request.DateOfBirth;
            user.PhoneNumber = request.PhoneNumber;

            await _dbContext.SaveChangesAsync();
            return Ok(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                IDPassportNumber = user.IDPassportNumber,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user");
            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- CREATE ORGANISER PROFILE ----------------
    [Authorize]
    [HttpPost("organiserProfile")]
    public async Task<IActionResult> CreateOrganiserProfile(
        CreateOrganiserProfileDto request)
    {
        try
        {
            _logger.LogInformation(
                "Creating organiser profile {ProfileName}",
                request.ProfileName);


            var profile = new OrganiserProfile
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                ProfileName = request.ProfileName,
                ProfileEmail = request.ProfileEmail,
                Description = request.Description,
                ProfileImage = request.ProfileImage,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                BankName = request.BankName,
                BankAccountName = request.BankAccountName,
                BankAccountNumber = request.BankAccountNumber,
                CreatedAt = DateTime.UtcNow
            };


            _dbContext.OrganiserProfiles.Add(profile);

            await _dbContext.SaveChangesAsync();


            return Ok(profile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating organiser profile");

            return StatusCode(500, "Internal server error");
        }
    }


    // ---------------- UPDATE ORGANISER PROFILE ----------------
    [Authorize]
    [HttpPut("organiserProfile/{id:guid}")]
    public async Task<IActionResult> UpdateOrganiserProfile(
        Guid id,
        UpdateOrganiserProfileDto request)
    {
        try
        {
            var profile = await _dbContext.OrganiserProfiles
                .FindAsync(id);


            if (profile == null)
                return NotFound("Organiser profile not found");


            profile.ProfileName = request.ProfileName;
            profile.ProfileEmail = request.ProfileEmail;
            profile.Description = request.Description;
            profile.ProfileImage = request.ProfileImage;
            profile.PhoneNumber = request.PhoneNumber;
            profile.Address = request.Address;
            profile.BankName = request.BankName;
            profile.BankAccountName = request.BankAccountName;
            profile.BankAccountNumber = request.BankAccountNumber;
            profile.EditedAt = DateTime.UtcNow;


            await _dbContext.SaveChangesAsync();


            return Ok(profile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating organiser profile");

            return StatusCode(500, "Internal server error");
        }
    }


    // ---------------- GET MY ORGANISER PROFILE ----------------
    [Authorize]
    [HttpGet("myOrganiserProfile")]
    public async Task<IActionResult> GetOrganiserProfile()
    {
        try
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            if (userId == null)
                return Unauthorized();


            var profile = await _dbContext.OrganiserProfiles
                .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId));


            if (profile == null)
                return NotFound("Organiser profile not found");


            return Ok(profile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching organiser profile");

            return StatusCode(500, "Internal server error");
        }
    }
}