using System.Collections.Generic;
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
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
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
    public ActionResult<List<Keep>> GetKeeps(string id)
    {
      try
      {
        List<Keep> k = _ks.GetKeepsByUser(id);
        return Ok(k);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Profile>>> GetVaults(string id)
    {
      try
      {
        if (User.Identity.IsAuthenticated == true) {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          List<Vault> v = _vs.GetVaultsByUser(id, userInfo.Id);
          return Ok(v);
        }
        else
        {
          List<Vault> v = _vs.GetVaultsByUser(id, "null");
          return Ok(v);
        }
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}