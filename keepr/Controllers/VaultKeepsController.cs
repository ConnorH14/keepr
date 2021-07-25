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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vkData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vkData.CreatorId = userInfo.Id;
        VaultKeep vk = _vks.CreateVaultKeep(vkData, userInfo.Id);
        return Ok(vk);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> DeleteVaultKeep(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        VaultKeep vk = _vks.DeleteVaultKeep(id, userInfo.Id);
        return Ok(vk);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    // [HttpGet]
    // public ActionResult<List<VaultKeep>> Get()
    // {
    //   try
    //   {
    //     List<VaultKeep> vkeeps = _vks.GetVaultKeeps();
    //     return Ok(vkeeps);
    //   }
    //   catch (System.Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

  }
}