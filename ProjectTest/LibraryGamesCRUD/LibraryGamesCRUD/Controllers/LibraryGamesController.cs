using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryGamesCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using static LibraryGamesCRUD.Models.ContexDb;

namespace LibraryGamesCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ContextDb _context;

        public GamesController()
        {
            _context = new ContextDb();
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            List<Games> games = _context.GetGames();
            return Ok(games);
        }

        [HttpPost]
        public IActionResult CreateGame(Games game)
        {
            if (ModelState.IsValid)
            {
                bool result = _context.CreateGame(game);
                if (result)
                {
                    return Created("", game);
                }
                else
                {
                    return BadRequest("Ошибка при создании игры");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("byGenre")]
        public IActionResult GetGamesByGenre(string genre)
        {
            List<Games> games = _context.GetGamesByGenre(genre);
            if (games != null)
            {
                return Ok(games);
            }
            else
            {
                return BadRequest("Ошибка при получении списка игр по жанру");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, Games game)
        {
            if (ModelState.IsValid)
            {
                game.Id = id;
                bool result = _context.UpdateGame(game);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Ошибка при обновлении игры");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
    public IActionResult DeleteGame(int id)
    {
        bool result = _context.DeleteGame(id);
        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest("Ошибка при удалении игры");
        }
    }
    }
}

