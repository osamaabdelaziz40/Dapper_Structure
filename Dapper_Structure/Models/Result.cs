﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper_Structure.Models
{
    public class Result
    {
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
