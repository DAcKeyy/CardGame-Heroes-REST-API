using CardGame_Heroes_REST_API.DataAccessObject;
using CardGame_Heroes_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardGame_Heroes_REST_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsInfoController: ControllerBase
    {
        private  ILogger<CardsInfoController> _logger;
        private CardsRepository cardsRepository;

        public CardsInfoController(ILogger<CardsInfoController> logger, CardsRepository cardsRepository)
        {
            _logger = logger;
            this.cardsRepository = cardsRepository;
            
        }


        [HttpGet("search")]
        public JsonResult Get(string? name, string? classes, string? elements, string? type, string? setname, short? cost, short? health, short? attack, string? rareness)
        {   
            var response = cardsRepository.Get(new Card()
            {
                Name = name,
                Class = classes,
                Element = elements,
                Type = type,
                SetName = setname,
                Cost = cost,
                Health = health,
                Attack = attack,
                Rareness = rareness
            });
            
            return new JsonResult(response);
        }

        [HttpGet("classes_list")]
        public JsonResult GetClasses()
        {
            var response = cardsRepository.GetClasses();

            return new JsonResult(response);
        }

        [HttpGet("elements_list")]
        public JsonResult GetElements()
        {
            var response = cardsRepository.GetElements();

            return new JsonResult(response);
        }

        [HttpGet("rarenesses_list")]
        public JsonResult GetRarenesses()
        {
            var response = cardsRepository.GetRarenesses();

            return new JsonResult(response);
        }

        [HttpGet("sets_list")]
        public JsonResult GetSets()
        {
            var response = cardsRepository.GetSets();

            return new JsonResult(response);
        }

        [HttpGet("types_list")]
        public JsonResult GetTypes()
        {
            var response = cardsRepository.GetTypes();

            return new JsonResult(response);
        }
    }
}