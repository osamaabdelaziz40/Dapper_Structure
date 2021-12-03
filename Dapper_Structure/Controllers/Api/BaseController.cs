using Dapper_Structure.Abstracts.Contracts;
using Dapper_Structure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper_Structure.Controllers.Api
{
    public class BaseController : Controller
    {
        public ICountryRepository Country_Repository;

        public Result result = new Result();
        
    }
}
