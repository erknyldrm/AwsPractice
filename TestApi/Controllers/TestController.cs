using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DynamoDbTestApi.Dtos;
using DynamoDbTestApi.Repositories;

namespace TestApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TestController(TestRepository testRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestDto dto)
        {
            bool result = await testRepository.CreateAsync(dto);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTestDto dto)
        {
            bool result = await testRepository.UpdateAsync(dto, DateTime.UtcNow);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            bool result = await testRepository.DeleteByIdAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await testRepository.GetAllAsync();

            return Ok(result);
        }
    }
}
