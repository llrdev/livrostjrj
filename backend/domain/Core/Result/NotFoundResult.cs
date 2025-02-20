using Domain.Core.Resources;
using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Core.Result
{
    /// <summary>
    /// Resposta dados não encontrados padrão
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotFoundResult<T> : Result<T>
    {
        private readonly List<string> _errors = new List<string>();
        private readonly List<string> _warnings = new List<string>();

        public NotFoundResult(string error)
        {
            _errors = new List<string> { error ?? Mensagens.InvalidoDefault };
        }

        public NotFoundResult(List<string> errors)
        {
            _errors = errors ?? new List<string> { Mensagens.InvalidoDefault };
        }

        public override ResultTypeEnum Type => ResultTypeEnum.NotFound;

        public override List<string> Errors => _errors;

        public override T Data => default;

        public override int TotalRecords => 0;

        public override string Success => null;

        public override string Error => string.Empty;
    }
}
