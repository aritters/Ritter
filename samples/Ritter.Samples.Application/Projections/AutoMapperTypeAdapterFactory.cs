using AutoMapper;
using Ritter.Infra.Crosscutting.TypeAdapter;
using Ritter.Samples.Application.Projections.Profiles;

namespace Ritter.Samples.Application.Projections
{
    public class AutoMapperTypeAdapterFactory : ITypeAdapterFactory
    {
        public AutoMapperTypeAdapterFactory()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<DomainToDtoProfile>();
                config.AddProfile<DtoToDomainProfile>();
            });
        }

        public ITypeAdapter Create() => new AutoMapperTypeAdapter();
    }
}