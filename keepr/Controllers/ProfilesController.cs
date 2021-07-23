using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile p = _ps.GetProfile(id);
        return Ok(p);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Profile>> GetOwn()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Profile p = _ps.GetProfile(userInfo.Id);
        return Ok(p);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<Profile> GetKeeps(string id)
    {
      try
      {
        // TODO get keeps by profile
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public ActionResult<Profile> GetVaults(string id)
    {
      try
      {
        // TODO get vaults by profile
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}