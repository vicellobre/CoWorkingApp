namespace CoWorkingApp.Core.Application.Contracts.Adapters
{
    /// <summary>
    /// Interfaz para los adaptadores de mapeo que proporcionan métodos para mapear objetos entre diferentes tipos.
    /// </summary>
    public interface IMapperAdapter
    {
        /// <summary>
        /// Mapea un objeto de origen a un objeto de destino.
        /// </summary>
        /// <typeparam name="TSource">Tipo de objeto de origen.</typeparam>
        /// <typeparam name="TDestination">Tipo de objeto de destino.</typeparam>
        /// <param name="source">Objeto de origen a ser mapeado.</param>
        /// <returns>Objeto de destino mapeado.</returns>
        TDestination Map<TSource, TDestination>(TSource source);

        /// <summary>
        /// Mapea una colección de objetos de origen a una colección de objetos de destino.
        /// </summary>
        /// <typeparam name="TSource">Tipo de objeto de origen.</typeparam>
        /// <typeparam name="TDestination">Tipo de objeto de destino.</typeparam>
        /// <param name="source">Colección de objetos de origen a ser mapeados.</param>
        /// <returns>Colección de objetos de destino mapeados.</returns>
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
    }
}
