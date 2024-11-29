using AutoMapper;
using CoWorkingApp.Application.Contracts.Adapters;

namespace CoWorkingApp.Infrastructure.Adapters;

/// <summary>
/// Adaptador de AutoMapper que implementa la interfaz IMapperAdapter.
/// </summary>
public class AutoMapperAdapter : IMapperAdapter
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor de la clase AutoMapperAdapter.
    /// </summary>
    /// <param name="mapper">Instancia de IMapper proporcionada por AutoMapper.</param>
    public AutoMapperAdapter(IMapper? mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Mapea un objeto de origen a un objeto de destino.
    /// </summary>
    /// <typeparam name="TSource">Tipo de objeto de origen.</typeparam>
    /// <typeparam name="TDestination">Tipo de objeto de destino.</typeparam>
    /// <param name="source">Objeto de origen a ser mapeado.</param>
    /// <returns>Objeto de destino mapeado.</returns>
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TSource, TDestination>(source);
    }

    /// <summary>
    /// Mapea una colección de objetos de origen a una colección de objetos de destino.
    /// </summary>
    /// <typeparam name="TSource">Tipo de objeto de origen.</typeparam>
    /// <typeparam name="TDestination">Tipo de objeto de destino.</typeparam>
    /// <param name="source">Colección de objetos de origen a ser mapeados.</param>
    /// <returns>Colección de objetos de destino mapeados.</returns>
    public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
    {
        return _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
    }
}
