using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    [HttpPost("calculate")]
    public ActionResult<CalculationResponse> Calculate([FromBody] CalculationRequest request)
    {
        // Проверка валидности данных
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            return BadRequest("Название продукта не может быть пустым");
        }

        if (request.Price <= 0)
        {
            return BadRequest("Цена должна быть больше 0");
        }

        if (request.Quantity <= 0)
        {
            return BadRequest("Количество должно быть больше 0");
        }

        // Расчет итоговой цены
        decimal totalPrice = request.Price * request.Quantity;

        // Возврат результата
        var result = new CalculationResponse
        {
            ProductName = request.ProductName,
            Price = request.Price,
            Quantity = request.Quantity,
            TotalPrice = totalPrice
        };

        return Ok(result);
    }

    // Дополнительный GET метод для удобства тестирования
    [HttpGet("calculate")]
    public ActionResult<CalculationResponse> CalculateGet(
        [FromQuery] string productName,
        [FromQuery] decimal price,
        [FromQuery] int quantity)
    {
        var request = new CalculationRequest
        {
            ProductName = productName,
            Price = price,
            Quantity = quantity
        };

        return Calculate(request);
    }
}

