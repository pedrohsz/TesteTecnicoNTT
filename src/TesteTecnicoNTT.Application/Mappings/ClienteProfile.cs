using AutoMapper;
using TesteTecnicoNTT.Application.Clientes.Commands.Create;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Events;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<AdicionarClienteCommand, Cliente>(); 
        CreateMap<Cliente, ClienteCriadoEvent>();  // corrigir id para ClienteId 
    }
}