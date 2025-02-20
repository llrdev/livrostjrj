using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Core.Result
{
    /// <summary>
    /// Resposta
    /// </summary>
    public abstract class Result<T>
    {
        /// <summary>
        /// Tipo
        /// </summary>
        public abstract ResultTypeEnum Type { get; }

        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public abstract List<string> Errors { get; }

        public abstract string Error { get; }

        /// <summary>
        /// Mensagem de sucesso
        /// </summary>
        public abstract string Success { get; }

        /// <summary>
        /// Objeto a ser retornado
        /// </summary>
        public abstract T Data { get; }

        /// <summary>
        /// Total de registros
        /// </summary>
        public abstract int TotalRecords { get; }

        public bool isNotFound
        {
            get => (Type == ResultTypeEnum.NotFound);
        }

        public bool isInvalid
        {
            get => (Type == ResultTypeEnum.Invalid);
        }

        public bool isNoContent
        {
            get => (Type == ResultTypeEnum.NoContent);
        }

        public bool isSuccess
        {
            get => (Type == ResultTypeEnum.Success);
        }

        public Dictionary<string, List<string>> Failures = new Dictionary<string, List<string>>();

        public void AddFailure(string key, string msg)
        {
            if (!Failures.ContainsKey(key))
            {
                Failures.Add(key, new List<string>());
            }
            Failures[key].Add(msg);
        }
    }
}