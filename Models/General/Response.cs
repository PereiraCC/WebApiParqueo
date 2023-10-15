using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.General
{
    public class Response
    {
        public Response()
        {
            Status = ResponseStatus.Success;
            currentException = null;
        }

        public Response(string currentException)
        {
            Status = ResponseStatus.Faild;
            this.currentException = currentException;
        }

        public Response(Exception currentException)
        {
            Status = ResponseStatus.Faild;
            this.currentException = currentException.Message;
        }

        public ResponseStatus Status { get; set; }

        public string currentException { get; set; }

        public enum ResponseStatus
        {
            Success = 0,
            Faild = 1
        }

    }
}
