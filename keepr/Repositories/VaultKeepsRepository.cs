using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;
using keepr.Services;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    private readonly VaultsService _vs;

    public VaultKeepsRepository(IDbConnection db, VaultsService vs)
    {
      _db = db;
      _vs = vs;
    }

    public List<VaultKeepView> GetVaultKeeps(int id, string userId)
    {
      string sql = "SELECT * FROM vault_keeps WHERE vaultId = @id;";
      List<VaultKeep> vaultKeeps = _db.Query<VaultKeep>(sql, new { id }).ToList();
      if(vaultKeeps != null)
      {
        List<VaultKeepView> vkv = new List<VaultKeepView>();
        foreach(VaultKeep vk in vaultKeeps)
        {
          int newId = vk.Id;
          sql = $"SELECT k.*, a.*, v.* FROM keeps k JOIN accounts a ON k.creatorId = a.id JOIN vaults v ON {vk.VaultId} = v.id WHERE k.id = { vk.KeepId };";
          VaultKeepView keepView = _db.Query<VaultKeepView, Profile, Vault, VaultKeepView>(sql, (vk, p, v) =>
        {
          if(v.IsPrivate == true && v.CreatorId != userId)
          {
            throw new Exception("Not authorized");
          }
          vk.VaultKeepId = newId;
          vk.Creator = p;
          return vk;
        }, new { vk }, splitOn: "id").FirstOrDefault();
        vkv.Add(keepView);
        }
        return vkv;
      }
      else
      {
        throw new Exception("Vault has no keeps.");
      }
    }

    public VaultKeep CreateVaultKeep(VaultKeep vkData, string userId)
    {
      Vault vault = _vs.GetVaultById(vkData.VaultId, userId);
      if (vault != null && vault.CreatorId == userId)
      {
        string sql = @"
          INSERT INTO vault_keeps(creatorId, keepId, vaultId)
          VALUES(@CreatorId, @KeepId, @VaultId);
          SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, vkData);
        vkData.Id = id;
        sql = $"SELECT * FROM vault_keeps WHERE id = {id};";
        return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
      }
      else
      {
        throw new Exception("Private vault, not your vault, or does not exist");
      }
    }

    public VaultKeep DeleteVaultKeep(int vkId, string userId)
    {
      string sql = "SELECT * FROM vault_keeps WHERE id = @vkId;";
      VaultKeep vk = _db.Query<VaultKeep>(sql, new { vkId }).FirstOrDefault();
      if (vk.CreatorId == userId)
      {
        sql = "DELETE FROM vault_keeps WHERE id = @vkId LIMIT 1;";
        _db.Execute(sql, new { vkId });
        return vk;
      }
      else
      {
        throw new Exception("Wrong User");
      }
    }

    // public List<VaultKeep> GetVaultKeeps()
    // {
    //   string sql = "SELECT * FROM vault_keeps;";
    //   return _db.Query<VaultKeep>(sql).ToList();
    // }
  }
}