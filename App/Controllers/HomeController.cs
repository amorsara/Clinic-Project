using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using Services.Home;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IHomeData _iHomeData;

        public HomeController(ClinicDBContext clinicDBContext, IHomeData homeData)
        {
           _iHomeData = homeData;
            _context = clinicDBContext;
        }

        [HttpGet]
        [Route("/api/home/loginuser/{name}/{password}")]
        public async Task<ActionResult<int>> LoginUser(string name, string password)
        {
            var res = await _iHomeData.LoginUser(name, password);
            if(res == 0)
            {
                return BadRequest();
            }
            return res;
        }

    }
}
