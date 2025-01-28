using Microsoft.AspNetCore.Mvc;
using Steam_1.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Steam_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        //GET: api/<GamesController>
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return Game.ReadGame();
        }

        [HttpGet("{id}")]
        public List<Game> ReadMyGamesList(int id)
        {
            return Game.ReadMyGamesList(id);
        }

        [HttpGet("GetMyListGameByPrice/{id}/{Price}")]
        public List<Game> Get(int id, double Price)
        {
            Game game = new Game();
            return game.GetMyListGameByPrice(id,Price);
        }

        [HttpGet("GetMyListGameByRank/{id}/{Score_Rank}")]
        public List<Game> Get(int id, int Score_Rank)
        {
            Game game = new Game();
            return game.GetMyListGameByRank(id, Score_Rank);
        }

        //POST api/Games/SaveGameToMyList
        [HttpPost("SaveGameToMyList")]
        public IActionResult SaveGameToMyList(int UserId, long GameId)
        {
            Game game = new Game();
            bool isSuccess = game.SaveGameToMyList(UserId, GameId);
            if (isSuccess)
            {
                return Ok(new { Message = "Game added seccessfully", userId = UserId, gameId = GameId });
            }

            else
            {
                return Conflict(new { Message = "Game already in library or purchase failed", userId = UserId, gameId = GameId });
            }
        }
        [HttpDelete("DeleteFromMyList/{userId}/{gameId}")]
        public IActionResult DeleteFromMyList(int userId, long gameId)
        {
            Game game = new Game();
            bool isSuccess = game.DeleteFromMyList(userId, gameId);

            if (!isSuccess)
            {
                return NotFound(new { Message = $"Game with ID {gameId} does not exist in user {userId}'s list" });
            }

            else
            {
                return Ok(new { Message = $"Game with ID {gameId} was successfully deleted from user {userId}'s list" });
            }

        }

        [HttpGet("GameDetails")]
        public Object GetGameDetails()
        {
            return Game.GetGameDetails(); // קריאה למתודה ב-Game והחזרת התוצאה
        }


    }
}

