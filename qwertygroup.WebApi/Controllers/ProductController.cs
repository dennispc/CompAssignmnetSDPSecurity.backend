using System.Collections.Generic;
using CompAssignmnetSDPSecurity.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompAssignmnetSDPSecurity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        public ActionResult<List<Product>> GetAll()
        {
            return null;
        }

    }
}