using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace AspNetCore.Authentication.JwtBearer.Extension.TestApi.Controllers
{

    [Route("api/v1/auth")]
    public class TestsController : ControllerBase
    {
        private readonly IPolicyAuthorizationHandler _jwtHandler;
        public TestsController(IPolicyAuthorizationHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpPost]
        [Route("token")]
        //[ProducesResponseType(typeof(string), Status200OK)]
        public IActionResult GenerateJwt()
        {
            var claims = new JwtPolicyClaims
            {
                UserName = "jack",
                Roles = "administrator"
            };
            var jwt = _jwtHandler.BuildToken(claims);
            return Ok(jwt);
        }

        [HttpPost]
        [Route("token/valid")]
        //[ProducesResponseType(typeof(string), Status200OK)]
        public IActionResult ValidJwt([FromBody] Req req)
        {
            string msg;
            if (!_jwtHandler.ValidToken(req.Token, out msg))
                return BadRequest(msg);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(req.Token) as JwtSecurityToken;
            var claims = new JwtPolicyClaims
            {
                UserName = jwtToken.Claims.First(claim => claim.Type == "UserName").Value,
                Roles = jwtToken.Claims.First(claim => claim.Type == "Roles").Value
            };
            return Ok(claims);
        }

    }
    public class Req
    {
        public string Token { get; set; }
    }
}
