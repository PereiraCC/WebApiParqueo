using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.General
{
    public class ResponseGeneric<T> : Response
    {
        public T Responses { get; set; }
        public ResponseGeneric(T response) : base()
        {
            this.Responses = response;
        }
        public ResponseGeneric(Exception currentException) : base(currentException)
        {
            this.Responses = default(T);
        }
        public ResponseGeneric(string currentException) : base(currentException)
        {
            this.Responses = default(T);
        }
    }
}
