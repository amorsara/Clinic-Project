using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using Services.DTO;
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

        [HttpPost]
        [Route("/api/home/loginuser")]
        public async Task<ActionResult<int>> LoginUser(LoginDto user)
        {
            var res = await _iHomeData.LoginUser(user.name, user.password);
            if(res == 0)
            {
                return BadRequest();
            }
            return res;
        }

    }
}
