using AutoMapper;
using TesteTecnicoNTT.Application.Pagamentos.Commands.Create;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Events;

public class PagamentoProfile : Profile
{
    public PagamentoProfile()
    {
        CreateMap<AdicionarPagamentoCommand, Pagamento>();
        CreateMap<AdicionarPagamentoCommand, PagamentoCriadoEvent>();
        CreateMap<Pagamento, PagamentoCriadoEvent>(); // corrigir id para pagamentoID
    }
}