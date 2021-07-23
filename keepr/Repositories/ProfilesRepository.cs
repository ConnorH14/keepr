using System;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }
    public Profile GetProfile(string accountId)
    {
      string sql = "SELECT * FROM accounts WHERE id = @accountId;";
      return _db.QueryFirstOrDefault<Profile>(sql, new { accountId });
    }
  }
}