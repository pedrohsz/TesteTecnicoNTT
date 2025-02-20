namespace TesteTecnicoNTT.Domain.Interfaces
{
    public interface IPagamentoReadRepository
    {
        Task<int> ObterTotalPagamentosAsync();
        Task<int> ObterTotalPagamentosAtrasadosEVencerAsync();
        Task<Dictionary<string, int>> ObterPagamentosAgrupadosPorEstadoAsync();
        Task<Dictionary<string, double>> ObterMediaRendaPorStatusAsync();
        Task<Dictionary<string, Dictionary<string, int>>> ObterClientesPorEstadoEStatusAsync();
    }
}