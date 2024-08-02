using APIBingo.Application.DTOs;
using APIBingo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIBingo.Api.Controllers
{
    [ApiController]
    [Route("api/Bingo")]
    public class BingoController: ControllerBase
    {
        private readonly IBingoNumberServices _bingoNumberServices;
        private readonly IProductServices _productServices;

        public BingoController(IBingoNumberServices bingoNumberServices, IProductServices productServices)
        {
            _productServices = productServices;
            _bingoNumberServices = bingoNumberServices;
        }

        [HttpPost("bingoNumber")]
        [Authorize]
        public async Task<IActionResult> findNumber([FromBody] BingoNumberRequest request)
        {
            var assignedNumber = await _bingoNumberServices.FindNumber(request);
            return Ok(assignedNumber);
        }

        [HttpPost("products")]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest request)
        {
            var response = await _productServices.AddProduct(request);
            return Ok(response);
        }
    }
}
