using System.Collections.Generic;

namespace Webapi.Core.Response
{
    public class Response<T>
    {
        protected Response()
        {
        }

        public Response(T data, string success = null)
        {
            Success = success ?? Mensagens.SucessoDefault;
            Errors = null;
            Data = data;
            Failures = null;
        }

        public Response(string error)
        {
            Errors = new List<string> { error ?? Mensagens.ErroDefault };
            Data = default;
            Failures = null;
        }

        public Response(List<string> errors)
        {
            Errors = errors ?? new List<string> { Mensagens.ErroDefault };
        }

        public Response(IDictionary<string, IEnumerable<string>> failures)
        {
            Errors = null;
            Data = default;
            Failures = failures;
        }

        public T Data { get; protected set; }
        public string Success { get; protected set; }
        public List<string> Errors { get; protected set; }
        public IDictionary<string, IEnumerable<string>> Failures { get; protected set; }
    }
}
