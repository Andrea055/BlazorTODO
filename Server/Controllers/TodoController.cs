using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Security;
using todo_blazor_auth.Server.Data;
using todo_blazor_auth.Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        private readonly ILogger<TodoController> _logger;

        private readonly string USER_NOT_FOUND_EXCEPTION = "User not found";

        public TodoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<TodoController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> Post([FromBody] Work new_work)
        {

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception(USER_NOT_FOUND_EXCEPTION);
                var user = await _userManager.FindByIdAsync(userId);    // Get User object
                if (user != null)
                {
                    new_work.User = user;
                    if (user.Email != null)
                        new_work.Email = user.Email;
                    else
                        throw new Exception(USER_NOT_FOUND_EXCEPTION.Concat(" - EMAIL NOT FOUND").ToString());
                    _context.Works.Add(new_work);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception(USER_NOT_FOUND_EXCEPTION);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during POST", ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize]
        async public Task<IEnumerable<Work>> Get()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception(USER_NOT_FOUND_EXCEPTION);
                return _context.Works.Where(work => work.User.Id == userId);

            }
            catch (Exception ex)
            {
                if (ex.Message != USER_NOT_FOUND_EXCEPTION)  // If user doesn't exist it's not an error
                    _logger.LogError("Error during GET", ex.ToString());
                return Enumerable.Empty<Work>();
            }
        }

        [HttpDelete]
        [Route("{workId}")]
        [Authorize]
        async public Task<IActionResult> Delete(int workId)
        {

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception(USER_NOT_FOUND_EXCEPTION);
                var user = await _userManager.FindByIdAsync(userId);    // Get User object

                var work_to_delete = _context.Works.Where(work => work.Id == workId).FirstOrDefault();
                if (work_to_delete != null && user != null)
                {
                    if (FirewallTODO.IsTheSameAuthor(user, work_to_delete))
                    {
                        _context.Works.Remove(work_to_delete);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(401);
                    }
                }
                else
                {
                    throw new Exception("Work not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.Message != "Work not found")
                {
                    if (ex.InnerException != null)
                        _logger.LogError("Error during delete", ex.InnerException.ToString());
                }

                return StatusCode(500);
            }
        }

        async public Task<IActionResult> Update([FromBody] Work new_work)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception(USER_NOT_FOUND_EXCEPTION);
                var user = await _userManager.FindByIdAsync(userId);    // Get User object

                var old_work = _context.Works.Where(work => work.Id == new_work.Id).FirstOrDefault();
                if (old_work != null && user != null)
                {
                    if (FirewallTODO.IsTheSameAuthor(user, old_work))
                    {
                        old_work.Name = new_work.Name;
                        await _context.SaveChangesAsync();
                        return Ok();
                    }else {
                        return StatusCode(401);
                    }
                }
                else
                {
                    throw new Exception("Work not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.Message != "Work not found")
                {
                    _logger.LogError("Error during update", ex.Message);
                }
                return StatusCode(500);
            }
        }
    }
}