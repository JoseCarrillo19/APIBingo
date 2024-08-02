using APIBingo.Application.DTOs;
using APIBingo.Application.Interfaces;
using APIBingo.Domain.Entities;
using APIBingo.Domain.IRepositories;

namespace APIBingo.Application.Services
{
    public class BingoNumberServices : IBingoNumberServices
    {
        private readonly IBingoNumberRepository _bingoNumberRepository;
        private readonly Random _random = new Random();

        public BingoNumberServices(IBingoNumberRepository bingoNumberRepository)
        {
            _bingoNumberRepository = bingoNumberRepository ?? throw new ArgumentNullException(nameof(bingoNumberRepository));
        }

        public async Task<string> FindNumber(BingoNumberRequest bingoNumberRequest)
        {
            if (bingoNumberRequest == null)
            {
                throw new ArgumentNullException(nameof(bingoNumberRequest));
            }

            try
            {
                int nextNumber;
                bool isValid;
                do
                {
                    nextNumber = GenerateRandomNumber();
                    isValid = await IsNumberValidAsync(nextNumber, bingoNumberRequest.ClientId);
                }
                while (!isValid);

                var bingoNumber = new BingoNumber
                {
                    ClientId = bingoNumberRequest.ClientId,
                    UserId = bingoNumberRequest.UserId,
                    ProductId = bingoNumberRequest.ProductId,
                    Number = nextNumber,
                    CreationDate = DateTime.UtcNow,
                    Status = "Active",
                    CreatedBy = bingoNumberRequest.CreatedBy ?? "System",
                    CreatedMachine = bingoNumberRequest.CreatedMachine ?? Environment.MachineName
                };

                _bingoNumberRepository.AddBingoNumber(bingoNumber);
                await _bingoNumberRepository.SaveChangesAsync();

                return nextNumber.ToString("D5");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while processing the bingo number request.", ex);
            }
        }

        private int GenerateRandomNumber()
        {
            const int minValue = 1;
            const int maxValue = 99999;
            return _random.Next(minValue, maxValue + 1);
        }

        private async Task<bool> IsNumberValidAsync(int number, int clientId)
        {
            var existingNumbers = await _bingoNumberRepository.GetExistingNumbersByClientAsync(clientId);
            var existingNumberForClient = existingNumbers.Any(bn => bn.Number == number && bn.ClientId == clientId);
            return !existingNumberForClient && IsValidFormat(number);
        }

        private bool IsValidFormat(int number)
        {
            string numStr = number.ToString("D5");
            for (int i = 0; i < numStr.Length - 2; i++)
            {
                if (numStr[i] == numStr[i + 1] && numStr[i + 1] == numStr[i + 2])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
