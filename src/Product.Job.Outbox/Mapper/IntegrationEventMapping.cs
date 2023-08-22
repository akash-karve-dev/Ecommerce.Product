using AutoMapper;
using Product.Job.Outbox.Models.Incoming;
using Product.Job.Outbox.Models.IntegrationEvents;

namespace Product.Job.Outbox.Mapper
{
    internal sealed class IntegrationEventMapping : Profile
    {
        public IntegrationEventMapping()
        {
            CreateMap<OutboxEntityEvent, ProductIntegrationEvent>()
                .ForMember(dest => dest.EventId, cfg => cfg.MapFrom(src => src.Id));
        }
    }
}