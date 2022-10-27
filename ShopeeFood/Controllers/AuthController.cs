using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopeeFood.Dtos.AuthDTO;
using ShopeeFood.Interfaces;
using ShopeeFood.Models;
using ShopeeFood.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace ShopeeFood.Controllers
{
	[ApiController]
	[Route("endUser/[controller]")]
	public class AuthController : Controller
	{
		private readonly todoAPIContext _todoAPIContext;
		private readonly IConfiguration _iConfiguration;
		private readonly IUser _iUser;
		public AuthController(todoAPIContext todoAPIContext, IConfiguration iConfiguration, IUser iUser)
		{
			_todoAPIContext = todoAPIContext;
			_iConfiguration = iConfiguration;
			_iUser = iUser;
		}

		private JwtSecurityToken GetToken(User user)
		{
			var tokenDescriptor = new List<Claim>
			{

					new Claim("Id", user.Id.ToString()),
					new Claim(JwtRegisteredClaimNames.Sub, user.Email),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					//The JTI is used for the refresh token
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};
			if (tokenDescriptor == null)
			{
				return new JwtSecurityToken();
			}
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["JWT:Secret"]));
			var token = new JwtSecurityToken(
			   issuer: _iConfiguration["JWT:ValidIssuer"],
			   audience: _iConfiguration["JWT:ValidAudience"],
			   expires: DateTime.Now.AddMinutes(5),
			   claims: tokenDescriptor,
			   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			   );

			return token;
		}

		public static string GenRefreshToken()
		{
			var randomNumber = new byte[64];
			var randomGen = RandomNumberGenerator.Create();
			randomGen.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		[HttpPost]
		[Route("Login")]
		public async Task<ActionResult> Login(LoginRequestDTO request)
		{


			var refreshToken = GenRefreshToken();
			var user = await _todoAPIContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
			if (user == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "User is not exist"
				});
			}
			//hash pass
			var hashPass = VerifyServices.Hash(user.Password);
			// Verify: it will compare the password input vs password in database
			string enteredPassword = request.Password;
			bool isPasswordCorrect = VerifyServices.Verify(enteredPassword, hashPass);
			if (isPasswordCorrect == false)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Password is not concect"
				});
			}
			var token = GetToken(user);
			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(5);
			_todoAPIContext.SaveChanges();
			return Ok(new
			{
				Success = true,
				Message = "Logged in successfully",
				Data = new TokenResponseDTO
				{
					Token = new JwtSecurityTokenHandler().WriteToken(token),
					RefreshToken = refreshToken
				}
			});
		}

		[HttpPost]
		[Route("RefreshToken")]
		public async Task<ActionResult> RefreshToken(RefreshTokenRequestDTO request)
		{
			if (request == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Refresh token is null"
				});
			}
			var user = await _todoAPIContext.Users.FirstOrDefaultAsync( u => u.RefreshToken == request.RefreshToken);
			if(user == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "User is not found"
				});
			}
			var timer = user.RefreshTokenExpiryTime - DateTime.Now;
			var checkedExpire = int.Parse(timer.Seconds.ToString());
			if (checkedExpire > 0)
			{
				return BadRequest( new
				{
					Success = false,
					Message = "Invalid client request"
				});
			}
			//var userName = principal.Identity.Name;
			//create new Token
			var newToken = GetToken(user);
			var reToken = GenRefreshToken();

			user.RefreshToken = reToken;
			user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(5);
			_todoAPIContext.SaveChanges();

			return Ok(new
			{
				Success = true,
				Message = "Refresh token is success",
				Data = new TokenResponseDTO
				{
					Token = new JwtSecurityTokenHandler().WriteToken(newToken),
					RefreshToken = reToken
				}
			});
		}

		[HttpPost]
		[Route("Register")]
		public async Task<ActionResult> RegisterUser(RegisterRequestDTO requestData)
		{
			if(requestData == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "the request is null"
				});
			}
			var result =  _iUser.Register(requestData);
			if (result)
			{
				return Ok(new
				{
					Success = result,
					Message = "Success"
				});
			} else
			{
				return BadRequest(new
				{
					Success = result,
					Message = "Fail"
				});
			}
			
		}

	}
}
