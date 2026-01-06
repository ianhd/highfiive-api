using Api.Models;
using Api.Models.Api;
using Api.Repos;
using Api.Services.ThirdParty;

namespace Api.Services
{
    public class BoardService
    {
        private readonly BoardRepo _boardRepo;
        private readonly HashIdsService _hashIdsService;
        private readonly PexelsService _pexelsService;
        public BoardService(BoardRepo boardRepo, HashIdsService hashIdsService, PexelsService pexelsService)
        {
            _boardRepo = boardRepo;
            _hashIdsService = hashIdsService;
            _pexelsService = pexelsService;
        }

        public async Task<Board> Insert(Board item)
        {
            return await _boardRepo.Insert(item);
        }

        public async Task<List<Board>> GetAll(int user_id)
        {
            var data = await _boardRepo.GetAll(user_id);
            var photos = new Dictionary<int, PexelsPhoto>();

            // load portraits for each board (pexels)
            foreach(var b in data)
            {
                if (photos.ContainsKey(b.pexels_photo_id)) continue;
                var photo = await _pexelsService.GetPhoto(b.pexels_photo_id);
                photos.Add(b.pexels_photo_id, photo);

                b.pexels_photo = photos[b.pexels_photo_id];
            }

            return data;
        }

        public async Task<List<Board>> GetAllAdmin()
        {
            var data = await _boardRepo.GetAllAdmin();
            var photos = new Dictionary<int, PexelsPhoto>();

            // load portraits for each board (pexels)
            foreach (var b in data)
            {
                if (photos.ContainsKey(b.pexels_photo_id)) continue;
                var photo = await _pexelsService.GetPhoto(b.pexels_photo_id);
                photos.Add(b.pexels_photo_id, photo);

                b.pexels_photo = photos[b.pexels_photo_id];
            }

            return data;
        }

        public async Task<string> DbPing()
        {
            var count = await _boardRepo.DbPing();
            var msg = $"{count} board record(s).";
            return msg;
        }

        public async Task<Board> Get(string board_eid)
        {
            var board_id = _hashIdsService.Decode(board_eid);
            var board = await _boardRepo.Get(board_id);
            var photo = await _pexelsService.GetPhoto(board.pexels_photo_id);
            board.pexels_photo = photo;
            return board;
        }

        public async Task Delete(string board_eid)
        {
            var board_id = _hashIdsService.Decode(board_eid);
            await _boardRepo.Delete(board_id);
        }
    }
}
