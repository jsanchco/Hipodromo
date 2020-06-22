namespace Codere.SBGOnline.Hipodromo.API.Controllers
{
    #region Using

    using AutoMapper;
    using Codere.SBGOnline.API.Models;
    using Codere.SBGOnline.Domain.Models;
    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using Codere.SBGOnline.Hipodromo.Domain.Repositories;
    using Codere.SBGOnline.WebApi.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToteLinkController : ControllerBase
    {
        private readonly ILogger<ToteLinkController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryUser _repositoryUser;
        private readonly IOptions<JwtAppSettings> _config;

        public ToteLinkController(
         ILogger<ToteLinkController> logger,
         IMapper mapper,
         IRepositoryUser repositoryUser,
         IOptions<JwtAppSettings> config
        )
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _repositoryUser = repositoryUser ??
                throw new ArgumentNullException(nameof(repositoryUser));
            _config = config ??
                throw new ArgumentNullException(nameof(config));
        }

        [HttpGet("GetInfoFromToken")]
        public ActionResult<User> GetInfoFromToken()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("AccountLogon")]
        public async Task<IActionResult> AccountLogon(Login login)
        {
            try
            {
                var userAuthenticate = await _repositoryUser.GetByAccountNumberAndPinAsync(login.accountNumber, login.pin);

                if (userAuthenticate == null)
                {
                    var error = $"Error in AccountLogon: username [{login.accountNumber}] not registered or incorrect pin";
                    _logger.LogWarning(error);
                    return BadRequest(new { message = error });
                }

                return new ObjectResult(new Session
                {
                    user = _mapper.Map<UserViewModel>(userAuthenticate),
                    token = getToken(userAuthenticate)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        private string getToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.Value.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("AccoutNumber", user.AccountNumber),
                    new Claim("Pin", user.Pin)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_config.Value.ExpirationTokenMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
