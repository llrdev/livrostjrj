using Domain.Core.Resources;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Core.Result
{
    /// <summary>
    /// Resposta inválida da padrão
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InvalidResult<T> : Result<T>
    {
        private readonly T _data;
        private readonly List<string> _errors;
        //private readonly List<string> _warnings;

        public InvalidResult(string error)
        {
            _errors = new List<string> { error ?? Mensagens.InvalidoDefault };
        }

        public InvalidResult(List<string> errors)
        {
            _errors = errors ?? new List<string> { Mensagens.InvalidoDefault };
        }

        public InvalidResult(T data, List<string> errors = null, Dictionary<string, List<string>> failures = null)
        {
            _data = data;
            _errors = errors ?? new List<string> { Mensagens.InvalidoDefault };
            this.Failures = failures;
        }

        public override ResultTypeEnum Type => ResultTypeEnum.Invalid;

        public override List<string> Errors => _errors;

        public override T Data => default;

        public override int TotalRecords => 0;

        public override string Success => null;

        public override string Error => _errors.Any() ? _errors[0] : string.Empty;
    }
}
