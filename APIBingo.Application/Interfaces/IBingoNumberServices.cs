using APIBingo.Application.DTOs;

namespace APIBingo.Application.Interfaces
{
    public interface IBingoNumberServices
    {
        Task<string> FindNumber(BingoNumberRequest bingoNumberRequest);
    }
}
