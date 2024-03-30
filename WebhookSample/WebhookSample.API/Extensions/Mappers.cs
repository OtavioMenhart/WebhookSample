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
            ClientsMapper();
        }

        private void ClientsMapper()
        {
            CreateMap<CreateClientRequest, Client>().ReverseMap();
            CreateMap<ClientCreatedResponse, Client>().ReverseMap();
            CreateMap<GetClientResponse, Client>().ReverseMap();
        }
    }
}
