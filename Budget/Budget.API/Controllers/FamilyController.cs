using Budget.Database;
using Budget.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Budget.API.Controllers
{
    /// <summary>
    /// Контроллер для доступа к ресурсу семья.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly BudgetDbContext _database;

        public FamilyController(BudgetDbContext database) => _database = database;

        /// <summary>
        /// Выполняет регистрацию семьи в системе.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult> CreateFamily([FromBody] RegistrationGroupModel model)
        {
            Family entity = new() { Name = model.Name, Password = model.Password };
            await _database.Families.AddAsync(entity).ConfigureAwait(false);
            try
            {
                await _database.SaveChangesAsync().ConfigureAwait(false);
                return Ok(entity.Id);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


    }

    /// <summary>
    /// Класс модели для создания семьи.
    /// </summary>
    /// <param name="Name">Название (может повторяться).</param>
    /// <param name="Password">Пароль для получения доступа.</param>
    public record RegistrationGroupModel(
        [Required(ErrorMessage = "Укажите имя")]
        string Name, 
        [Required(ErrorMessage = "Укажите пароль")]
        string Password);
}
