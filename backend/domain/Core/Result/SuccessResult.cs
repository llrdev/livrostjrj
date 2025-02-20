using Domain.Core.Resources;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Core.Result
{
    /// <summary>
    /// Resposta de sucesso padrão
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuccessResult<T> : Result<T>
    {
        private readonly T _data;
        private readonly string _success;
        private readonly List<string> _errors = new List<string>();

        public SuccessResult(T data, string success)
        {
            _data = data;
            _success = success ?? Mensagens.SucessoDefault;
        }

        public SuccessResult(T data, string success, List<string> errors)
        {
            _data = data;
            _success = success ?? Mensagens.SucessoDefault;
            _errors = errors == null ? new List<string>() : errors;
        }

        public SuccessResult(string success)
        {
            _data = default(T);
            _success = success ?? Mensagens.SucessoDefault;
        }

        public override ResultTypeEnum Type => ResultTypeEnum.Success;

        public override List<string> Errors => _errors;

        public override T Data => _data;

        public override int TotalRecords => 0;

        public override string Success => _success;

        public override string Error => _errors.Any() ? _errors[0] : string.Empty;
    }
}
