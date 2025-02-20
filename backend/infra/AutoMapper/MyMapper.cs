using AutoMapper;
using Domain.Interfaces.Infra;

namespace Infra.AutoMapper
{
    public class MyMapper : IMyMapper
    {
        private readonly IMapper _mapper;
        public MyMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T Map<T>(object o)
        {
            return _mapper.Map<T>(o);
        }
    }
}
