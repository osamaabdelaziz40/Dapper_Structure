using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Structure.Abstracts.Messages.Base
{
    public abstract class QueryParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        //public bool EnablePaging { get; set; } = false;
        public QueryParameters()
        {

        }
    }
}