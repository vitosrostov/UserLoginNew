using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserLoginNew.Controllers // Controller of basket with add and remove tovars and save numbers tovars? buy tovars
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketservice;
        private readonly BuyTovar _boolBuyTovar;
        // dependency injection in constructor Basket service and class for buy, AddSingleton
        public BasketController(IBasketService basketService, BuyTovar boolBuyTovar) 
        {
            _basketservice = basketService;
            _boolBuyTovar= boolBuyTovar;
        }

        [HttpGet("tovars")]
        public IActionResult Get()
        {
            return Ok(new {
                success = "True",
                _basketservice.Tovars
            });
        }
        [HttpPost("add-tovars")] // add number tovars to basket
        public IActionResult AddTovars([FromBody] TovarCredentialsModel prms)
        {

            _basketservice.AddAnyTovars(prms.TovarNum);
            var numTovars = _basketservice.Tovars;
            return Ok(new
            {
                success = "True",
                numTovars
            });
        }

        [HttpPost("remove-tovars")] //remove tovars of basket
        public IActionResult RemoveTovars([FromBody] TovarCredentialsModel prms){

                if(prms.TovarNum > _basketservice.Tovars) { 
                    return Ok(new 
                    { 
                    success = "False",
                    error = "the number of tovars cannon be negative"
                    });
                }
                _basketservice.RemoveAnyTovars(prms.TovarNum);
                var numTovars = _basketservice.Tovars;
                return Ok(new { 
                    success = "True",
                    numTovars });
            }
        [HttpGet("buy")] //buy tovars of basket
        public IActionResult Buy()
        {
            if (_basketservice.Tovars > 0)
            {
                _boolBuyTovar.BoolBuy();
                return Ok(new
                {
                    number = _basketservice.Tovars,
                    success = "True",
                    _boolBuyTovar.TovarBoolBuy
                });
            }

            return BadRequest(new
            {
                success = "False",
                error = $"tovarNumber = {_basketservice.Tovars}"
            });
        }
    }
}
