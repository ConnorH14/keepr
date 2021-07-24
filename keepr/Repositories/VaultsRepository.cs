using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    public List<Vault> GetVaults()
    {
      string sql = "SELECT v.*, a.* FROM vaults v JOIN accounts a ON v.creatorId = a.id;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, splitOn: "id"
      ).ToList();
    }

    public Vault GetVaultById(int id)
    {
      string sql = $"SELECT v.*, a.* FROM vaults v JOIN accounts a ON v.creatorId = a.id WHERE v.Id = @id;";
      Vault vault = _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { id }).FirstOrDefault();
      if (vault == null)
      {
        throw new Exception("Bad ID");
      }
      return vault;
    }


    public Vault CreateVault(Vault vaultData)
    {
      string sql = @"
        INSERT INTO vaults(creatorId, name, description, isPrivate)
        VALUES(@CreatorId, @Name, @Description, @IsPrivate);
        SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      sql = $"SELECT v.*, a.* FROM vaults v JOIN accounts a ON v.creatorId = a.id WHERE v.Id = {id};";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { id }).FirstOrDefault();
    }


    public Vault EditVault(Vault vaultData)
    {
      string sql = @"
        UPDATE vaults
          SET
          name = @Name,
          description = @Description,
          isPrivate = @IsPrivate
        WHERE id = @Id;
      ";
      int rowsAffected = _db.Execute(sql, vaultData);
      if (rowsAffected > 1)
      {
        throw new Exception("Multiple Lines Changed: Error");
      }
      if (rowsAffected < 1)
      {
        throw new Exception("Edit did not occur, possible bad Id");
      }
      return vaultData;
    }

    public void DeleteVault(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    public List<Vault> GetVaultsByUser(string searchedId, string userId)
    {
      if (searchedId == userId)
      {
        string sql = "SELECT v.*, a.* FROM vaults v JOIN accounts a ON v.creatorId = a.id WHERE @searchedId = creatorId;";
        return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
        {
          v.Creator = p;
          return v;
        }, new { searchedId }, splitOn: "id"
        ).ToList();
      }
      else
      {
        string sql = "SELECT v.*, a.* FROM vaults v JOIN accounts a ON v.creatorId = a.id WHERE @searchedId = creatorId AND isPrivate = 0;";
        return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
        {
          v.Creator = p;
          return v;
        }, new { searchedId }, splitOn: "id"
        ).ToList();
      }
    }
  }
}