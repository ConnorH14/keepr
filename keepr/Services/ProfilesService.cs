using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
    }
    internal Profile GetProfile(string accountId)
    {
      return _pr.GetProfile(accountId);
    }
  }
}