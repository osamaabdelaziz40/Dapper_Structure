using Dapper_Structure.Abstracts.Contracts.Base;
using Dapper_Structure.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Structure.Abstracts.Contracts
{
    public interface ICountryRepository : IRepositoryBase<CountryVM>
    {
    }
}
