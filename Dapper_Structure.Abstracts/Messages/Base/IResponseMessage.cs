using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Structure.Abstracts.Messages.Base
{
    public interface IResponseMessage<T> where T : class
    {
        IEnumerable<T> ResponseData { get; set; }
        MessageTypes MessageType { get; set; }
        Exception exception { get; set; }
    }
}
