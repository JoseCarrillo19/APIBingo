using APIBingo.Domain.Entities;

namespace APIBingo.Domain.IRepositories
{
    public interface IBingoNumberRepository
    {
        Task<int> GetNextAvailableNumber(int clientId);
        Task<IEnumerable<BingoNumber>> GetExistingNumbersByClientAsync(int clientId);
        void AddBingoNumber(BingoNumber bingoNumber);
        Task SaveChangesAsync();
    }
}
