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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;

    private readonly  VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpGet]
    public ActionResult<List<Vault>> Get()
    {
      try
      {
        List<Vault> vaults = _vs.GetVaults();
        return Ok(vaults);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetById(int id)
    {
      try
      {
        if (User.Identity.IsAuthenticated == true) {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          Vault v = _vs.GetVaultById(id, userInfo.Id);
          return Ok(v);
        }
        else
        {
          Vault v = _vs.GetVaultById(id, "null");
          return Ok(v);
        }
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;

        Vault v = _vs.CreateVault(vaultData);
        return Created("api/vaults/" + v.Id, v);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault vaultData, int id)
    {
      try
      {
        vaultData.Id = id;
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault v = _vs.EditVault(vaultData, userInfo.Id);
        return Ok(v);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vs.DeleteVault(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<VaultKeepView>>> GetVaultKeeps(int id)
    {
      try
      {
        
        if (User.Identity.IsAuthenticated == true) {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          List<VaultKeepView> keeps = _vks.GetVaultKeeps(id, userInfo.Id);
          return Ok(keeps);
        }
        else
        {
          List<VaultKeepView> keeps = _vks.GetVaultKeeps(id, "null");
          return Ok(keeps);
        }
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}