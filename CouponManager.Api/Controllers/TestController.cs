using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouponManager.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("cm/api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : ControllerBase
    {
        private WaitForSeconds _waitForSeconds;

        public TestController(WaitForSeconds waitForSeconds)
        {
            _waitForSeconds = waitForSeconds;
        }

        [HttpGet]
        public async Task<IActionResult> WaitSeconds()
        {
            await _waitForSeconds.WaitSeconds(2000);
            return new JsonResult(new { Result = "Done" });
        }
    }
}