using HubDeEntretenimientoMegaLiderlyBackend.Models;
using HubDeEntretenimientoMegaLiderlyBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HubDeEntretenimientoMegaLiderlyBackend.Controllers
{
    [ApiController] 
    [Route("access")] // ruta al AccessController
    public class AccessController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;

        public AccessController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        /// <summary>
        /// Crea un nuevo usuario en la DB
        /// </summary>
        /// <param name="modelUser"></param>
        /// <returns>Retorna un mensaje si el usuario fue creado exitosamente o no</returns>
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(User modelUser)
        {
            User user = new User();

            user.Name = modelUser.Name;
            user.LastName = modelUser.LastName;
            user.Email = modelUser.Email;
            user.Password = modelUser.Password;
            user.Age = modelUser.Age;

            await _appDBContext.Users.AddAsync(user);
            await _appDBContext.SaveChangesAsync();

            if (user.IdUser != 0)
            {
                return Ok(new { Message = "Usuario creado exitosamente" });

            }
            return BadRequest(new { Message = "No se pudo crear el usuario" });
        }


        /// <summary>
        /// Busca el usuario ingresado en la BD
        /// </summary>
        /// <param name="modelUser"></param>
        /// <returns>Retorna un mensaje si se encontro o no el usuario en la BD</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM modelUser)
        {
            User? userFound = await _appDBContext.Users.Where(u =>
                    u.Email == modelUser.Email && u.Password == modelUser.Password
                ).FirstOrDefaultAsync();

            if (userFound == null)
            {
                return NotFound(new { Message = "No se encontró el usuario" });
            }

            return Ok(userFound);
        }

        
    }
}
