using Budget.Database;
using Budget.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Budget.API.Controllers
{
    /// <summary>
    /// Контроллер для доступа к ресурсу пользователь.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BudgetDbContext _database;

        public UserController(BudgetDbContext database) => _database = database;

        /// <summary>
        /// Выполняет регистрацию пользователя в системе.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult> CreateUser([FromBody] RegistrationGroupModel model)
        {
            User entity = new() { Name = model.Name, Password = model.Password };
            await _database.Users.AddAsync(entity).ConfigureAwait(false);
            try
            {
                await _database.SaveChangesAsync().ConfigureAwait(false);
                return Ok(entity.Id);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


    }

    /// <summary>
    /// Класс модели для создания пользователя.
    /// </summary>
    /// <param name="Name">Имя пользователя(логин).</param>
    /// <param name="Password">Пароль для получения доступа.</param>
    public record RegistrationUserModel(
        [Required(ErrorMessage = "Укажите имя")]
        string Name, 
        [Required(ErrorMessage = "Укажите пароль")]
        string Password);
}
