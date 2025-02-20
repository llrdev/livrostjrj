namespace Domain.Interfaces.Infra
{
    public interface IMyMapper
    {
        /// <summary>
        /// Realiza o mapeamento para o objeto de destino
        /// </summary>
        /// <typeparam name="T">Objeto de destino</typeparam>
        /// <param name="o">objeto a ser mapeado</param>
        /// <returns></returns>
        T Map<T>(object o);
    }
}
