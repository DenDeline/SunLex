using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunLex.WebAPI.Endpoints.Home
{
    public class Get : BaseEndpoint
        .WithoutRequest
        .WithResponse<string>
    {
        [HttpGet("/")]
        [SwaggerOperation(
            Summary = "Test swagger annotations",
            Description = "Test",
            OperationId = "HomeEndpoings.Get")
        ]
        public override ActionResult<string> Handle()
        {
            return Ok("SunLex WebAPI");
        }
    }
}
