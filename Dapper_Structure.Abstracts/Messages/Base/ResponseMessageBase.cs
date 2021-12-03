using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Structure.Abstracts.Messages.Base
{
    public class ResponseMessageBase<T> : IResponseMessage<T> where T : class
    {
        public IEnumerable<T> ResponseData { get; set; }
        public MessageTypes MessageType { get; set; }
        public Exception exception { get; set; }
    }
}
