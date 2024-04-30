using Karma.API.Extensions;
using Karma.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Karma.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected string AccessToken => Request.GetAccessToken();

        protected virtual Guid UserId => ClaimHelper.GetClaim<Guid>(this.AccessToken, "id");

        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            if (value is string)
                return base.Ok(new { Message = value });

            return base.Ok(value);
        }
    }
}
