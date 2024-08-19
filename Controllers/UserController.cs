using Microsoft.AspNetCore.Mvc;
using HubDeEntretenimientoMegaLiderlyBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HubDeEntretenimientoMegaLiderlyBackend.Controllers
{
    [ApiController]
    [Route("users")] // ruta al AccessController
    public class UserController : Controller
    {
        private readonly AppDBContext _appDBContext;

        // Constructor
        public UserController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }


        /// <summary>
        ///     Devuelve la lista de todos los usuarios en la DB
        /// </summary>
        /// <returns>Lista de todos los Usuarios</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<User>? userList = new List<User>();
            userList = await _appDBContext.Users.Select(x => x).ToListAsync();
            if (userList == null || !userList.Any())
            {
                return NotFound(new { Message = "No se encontraron Usuarios"});
            }
            return Ok(userList);
        }


        /// <summary>
        ///     Devuelve un solo usuario en especifico por su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            User? user = new User();
            user = await _appDBContext.Users.Where(x => x.IdUser == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new { Message = "No se encontró el usuario" });
            }
            //return Ok(new { Message = "Usuario encontrado", User = user });
            return Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(User modelUser)
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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User modelUser)
        {
            // Busca el usuario por ID en la base de datos
            var user = await _appDBContext.Users.FindAsync(id);

            // Si no se encuentra devuelve un 404
            if (user == null)
            {
                return NotFound(new { Message = "Usuario no encontrado" });
            }

            user.Name = modelUser.Name;
            user.LastName = modelUser.LastName;
            user.Email = modelUser.Email;
            user.Password = modelUser.Password;
            user.Age = modelUser.Age;

            _appDBContext.Users.Update(user);
            await _appDBContext.SaveChangesAsync();

            return Ok(new { Message = "Usuario actualizado exitosamente" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Busca el usuario por ID en la base de datos
            var user = await _appDBContext.Users.FindAsync(id);

            // Si no se encuentra devuelve un 404
            if (user == null)
            {
                return NotFound(new { Message = "Usuario no encontrado" });
            }

            // Elimina el usuario de la base de datos
            _appDBContext.Users.Remove(user);
            await _appDBContext.SaveChangesAsync();

            return Ok(new { Message = "Usuario eliminado exitosamente" });
        }




    }
}
