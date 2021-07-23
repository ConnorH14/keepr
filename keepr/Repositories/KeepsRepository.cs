using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    public List<Keep> GetKeeps()
    {
      string sql = "SELECT k.*, a.* FROM keeps k JOIN accounts a ON k.creatorId = a.id;";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, splitOn: "id"
      ).ToList();
    }

    public Keep GetKeepById(int id)
    {
      string sql = $"SELECT k.*, a.* FROM keeps k JOIN accounts a ON k.creatorId = a.id WHERE k.Id = @id;";
      Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { id }).FirstOrDefault();
      if (keep == null)
      {
        throw new Exception("Bad ID");
      }
      return keep;
    }

    public Keep CreateKeep(Keep keepData)
    {
      string sql = @"
        INSERT INTO keeps(creatorId, name, description, img, views, shares, keeps)
        VALUES(@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
        SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      sql = $"SELECT k.*, a.* FROM keeps k JOIN accounts a ON k.creatorId = a.id WHERE k.Id = {id};";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { id }).FirstOrDefault();
    }

    public Keep EditKeep(Keep keepData)
    {
      string sql = @"
        UPDATE keeps
          SET
          name = @Name,
          description = @Description,
          img = @Img,
          views = @Views,
          shares = @Shares,
          keeps = @Keeps
        WHERE id = @Id;
      ";
      int rowsAffected = _db.Execute(sql, keepData);
      if (rowsAffected > 1)
      {
        throw new Exception("Multiple Lines Changed: Error");
      }
      if (rowsAffected < 1)
      {
        throw new Exception("Edit did not occur, possible bad Id");
      }
      return keepData;
    }

    public void DeleteKeep(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}