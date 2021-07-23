using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }
    internal List<Vault> GetVaults()
    {
      return _vr.GetVaults();
    }

    internal Vault GetVaultById(int id, string userId)
    {
      Vault vault = _vr.GetVaultById(id);
      if (vault.IsPrivate == true) {
        if (vault.CreatorId != userId)
        {
          throw new Exception("Vault is private.");
        }
      }
      return vault;
    }

    internal Vault CreateVault(Vault vaultData)
    {
      return _vr.CreateVault(vaultData);
    }

    internal Vault EditVault(Vault vaultData, string userId)
    {
      Vault vault = _vr.GetVaultById(vaultData.Id);
      if (vault == null)
      {
        throw new Exception("Bad ID");
      }
      if (vault.CreatorId != userId)
      {
        throw new Exception("Wrong user");
      }
      vault.Name = vaultData.Name ?? vault.Name;
      vault.Description = vaultData.Description ?? vault.Description;

      return _vr.EditVault(vault);
    }

    internal void DeleteVault(int id, string userId)
    {
      Vault vault = _vr.GetVaultById(id);
      if (vault.CreatorId != userId)
      {
        throw new Exception("Wrong user");
      }
      _vr.DeleteVault(vault.Id);
    }
  }
}