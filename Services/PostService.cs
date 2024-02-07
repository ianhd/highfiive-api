using Api.Models;
using Api.Repos;
using Api.Services.ThirdParty;
using System.Reflection.Metadata.Ecma335;

namespace Api.Services
{
    public class PostService
    {
        private readonly PostRepo _postRepo;
        private readonly HashIdsService _hashIdsService;
        public PostService(PostRepo postRepo, HashIdsService hashIdsService)
        {
            _postRepo = postRepo;
            _hashIdsService = hashIdsService;
        }

        public async Task Insert(Post item)
        {
            item.board_id = _hashIdsService.Decode(item.board_eid);
            await _postRepo.Insert(item);
        }

        public async Task Delete(string post_eid)
        {
            var post_id = _hashIdsService.Decode(post_eid);
            await _postRepo.Delete(post_id);
        }

        public async Task<List<Post>> GetForBoard(string board_eid)
        {
            var board_id = _hashIdsService.Decode(board_eid);
            return await _postRepo.GetForBoard(board_id);
        }
    }
}
