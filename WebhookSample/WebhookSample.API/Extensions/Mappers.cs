using AutoMapper;
using WebhookSample.Domain.Entities;
using WebhookSample.Domain.Requests.Clients;
using WebhookSample.Domain.Responses.Clients;

namespace WebhookSample.API.Extensions
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateClientMapper();
        }

        private void CreateClientMapper()
        {
            CreateMap<CreateClientRequest, Client>().ReverseMap();
            CreateMap<ClientCreatedResponse, Client>().ReverseMap();
            CreateMap<GetClientResponse, Client>().ReverseMap();            
        }
    }
}
