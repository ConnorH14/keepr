using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;

    public KeepsService(KeepsRepository kr)
    {
      _kr = kr;
    }
    internal List<Keep> GetKeeps()
    {
      return _kr.GetKeeps();
    }

    internal Keep GetKeepById(int id)
    {
      return _kr.GetKeepById(id);
    }

    internal Keep CreateKeep(Keep keepData)
    {
      var keep = _kr.CreateKeep(keepData);
      return keep;
    }

    internal Keep EditKeep(Keep keepData, string userId)
    {
      Keep keep = _kr.GetKeepById(keepData.Id);
      if (keep == null)
      {
        throw new Exception("Bad ID");
      }
      if (keep.CreatorId != userId)
      {
        throw new Exception("Wrong user");
      }
      keep.Name = keepData.Name ?? keep.Name;
      keep.Description = keepData.Description ?? keep.Description;
      keep.Img = keepData.Img ?? keep.Img;
      if (keep.Shares == 0){keep.Shares = keepData.Shares;}

      return _kr.EditKeep(keep);
    }

    internal void DeleteKeep(int id, string userId)
    {
      Keep keep = _kr.GetKeepById(id);
      if (keep.CreatorId != userId)
      {
        throw new Exception("Wrong user");
      }
      _kr.DeleteKeep(keep.Id);
    }

    internal List<Keep> GetKeepsByUser(string id)
    {
      return _kr.GetKeepsByUser(id);
    }
  }
}