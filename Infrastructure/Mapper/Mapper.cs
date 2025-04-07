using System.Runtime;
using Application.DTO_s;
using AutoMapper;

namespace AccountAPI.Mapper
{
    public interface IMapperService
    {
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> sources);
    }
    public class Mapper : IMapperService
    {
        private readonly IMapper _mapper;

        public Mapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }

        public IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> sources)
        {
            return _mapper.Map<IEnumerable<TDestination>>(sources);
        }
    }
}
