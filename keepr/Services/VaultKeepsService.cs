using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;
    
    public VaultKeepsService(VaultKeepsRepository vkr)
    {
      _vkr = vkr;
    }

    internal List<VaultKeepView> GetVaultKeeps(int id, string userId)
    {
      return _vkr.GetVaultKeeps(id, userId);
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vkData, string userId)
    {
      return _vkr.CreateVaultKeep(vkData, userId);
    }

    internal VaultKeep DeleteVaultKeep(int vkId, string userId)
    {
      return _vkr.DeleteVaultKeep(vkId, userId);
    }

    // internal List<VaultKeep> GetVaultKeeps()
    // {
    //   return _vkr.GetVaultKeeps();
    // }
  }
}