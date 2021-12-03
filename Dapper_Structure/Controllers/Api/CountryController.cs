using Dapper_Structure.Abstracts.Contracts;
using Dapper_Structure.Abstracts.Messages;
using Dapper_Structure.Abstracts.Messages.Base;
using Dapper_Structure.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper_Structure.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : BaseController
    {
        private readonly ICountryRepository repo;

        public CountryController(ICountryRepository _repo)
        {
            repo = _repo;
        }
        [HttpPost]
        [ActionName("Get")]
        public IActionResult Get()
        {
            try
            {

                CountryRequest req = new CountryRequest();
                var res = repo.GetAll(req);
                var countries = new List<CountryVM>();
                if (res.Result.MessageType == MessageTypes.Success)
                {
                    countries = res.Result.ResponseData.ToList();
                    result.code = 200;
                    result.message = "Success";
                    result.data = countries;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.code = 404;
                result.message = "تحقق من الاتصال بالانترنت  ";
            }
            return Ok(result);
        }

    }
}
