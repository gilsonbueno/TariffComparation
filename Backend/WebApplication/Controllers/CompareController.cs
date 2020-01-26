using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Business.Interface;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompareController : ControllerBase
    {
        private readonly ITariffBusiness consumeBusiness;

        public CompareController(ITariffBusiness consumeBusiness)
        {
            this.consumeBusiness = consumeBusiness;
        }

        [HttpGet("{consumption}")]
        public List<ConsumeModel> GetList(int consumption)
        {
            var result = consumeBusiness.Compare(consumption);
            return result;
        }
    }
}
