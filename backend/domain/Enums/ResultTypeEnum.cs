namespace Domain.Enums
{
    /// <summary>
    /// Enumerador que tipa a resposta padrão das requisições dos services
    /// </summary>
    public enum ResultTypeEnum
    {
        /// <summary>
        /// Realizada com sucesso.
        /// </summary>
        Success,

        /// <summary>
        /// Ocorreu que algum dado de entrada ou regra não foi satisfeita.
        /// </summary>
        Invalid,

        /// <summary>
        /// Não foi possível carregar a(s) informação(ções).
        /// </summary>
        NotFound,

        /// <summary>
        /// Página de uma consulta com a lista das informações e total de registros para a condição de pesquisa.
        /// </summary>
        Page,

        /// <summary>
        /// Nenhum registro encontrato.
        /// </summary>
        NoContent,
    }
}