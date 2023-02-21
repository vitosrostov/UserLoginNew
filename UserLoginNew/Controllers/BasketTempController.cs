using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserLoginNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketTempController : ControllerBase
    {
        private readonly IBasketTempService _baskettempservice;
        private readonly BuyTovar _boolBuyTovar;
        // dependency injection in constructor Basket service and class for buy, AddScoped 
        public BasketTempController(IBasketTempService basketTempService, BuyTovar boolBuyTovar) 
        {
            _baskettempservice = basketTempService;
            _boolBuyTovar = boolBuyTovar;
        }

        [HttpPost("add-tovars-temp")]
        public IActionResult AddTovars([FromBody] TovarCredentialsModel prms)
        {

            _baskettempservice.AddAnyTovars(prms.TovarNum);
            var numTovars = _baskettempservice.Tovars;
            _boolBuyTovar.BoolBuy();
            return Ok(new
            {
                success = "True",
                numTovars,
                tovarBuy = _boolBuyTovar
            });
        }

    }
}
