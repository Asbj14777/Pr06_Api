using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor;
using Pr06_Api.Services;
using Pr06_Api.Interface;
using Pr06_Api.Model;
namespace Pr06_Api.Controllers
{
        public class RickAndMortyController : Controller
        {
            private readonly IRickAndMortyHttpService _rickAndMortyService;

            public RickAndMortyController(IRickAndMortyHttpService rickAndMortyService)
            {
                _rickAndMortyService = rickAndMortyService;
            }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var characters = await _rickAndMortyService.GetAllCharactersAsync();

            ViewBag.Characters = characters;

            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            var character = await _rickAndMortyService.GetCharacterByIdAsync(id);

            if (character == null)
            {
                ViewBag.Message = "Character not found.";
                return View();
            }

            return View(character);
        }


    }
}
