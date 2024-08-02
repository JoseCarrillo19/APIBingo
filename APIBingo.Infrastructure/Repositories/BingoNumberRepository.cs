using APIBingo.Domain.Entities;
using APIBingo.Domain.IRepositories;
using APIBingo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APIBingo.Infrastructure.Repositories
{
    public class BingoNumberRepository : IBingoNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public BingoNumberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetNextAvailableNumber(int clientId)
        {
            try
            {
                var maxNumber = await _context.BingoNumbers
                    .Where(r => r.ClientId == clientId)
                    .MaxAsync(r => (int?)r.Number) ?? 0;
                return maxNumber + 1;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while retrieving the next available number.", ex);
            }
        }

        public async Task<IEnumerable<BingoNumber>> GetExistingNumbersByClientAsync(int clientId)
        {
            try
            {
                return await _context.BingoNumbers
                .Where(bn => bn.ClientId == clientId)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error Existing Numbers By Client.", ex);
            }
        }

        public void AddBingoNumber(BingoNumber bingoNumber)
        {
            try
            {
                _context.BingoNumbers.Add(bingoNumber);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while adding a bingo number.", ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while saving changes to the database.", ex);
            }
        }
    }
}
