using Api.Models;
using Api.Repos;
using Api.Services.ThirdParty;

namespace Api.Services
{
    public class BoardService
    {
        private readonly BoardRepo _boardRepo;
        public BoardService(BoardRepo boardRepo)
        {
            _boardRepo = boardRepo;
        }

        public async Task<Board> Insert(Board item)
        {
            return await _boardRepo.Insert(item);
        }
    }
}
