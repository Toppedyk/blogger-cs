using System;
using blogger_cs.Models;
using blogger_cs.Repositories;

namespace blogger_cs.Services
{
    public class AccountsService
    {
        private readonly AccountsRepository _repo;

    public AccountsService(AccountsRepository repo)
    {
      _repo = repo;
    }

    internal Account GetOrCreateAccount(Account userInfo)
    {
      Account account = _repo.GetById(userInfo.Id);
      if(account == null)
      {
        return _repo.Create(userInfo);
      }
      return account;
    }

    internal Account GetProfileById(string id)
    {
      Account account = _repo.GetById(id);
      if(account == null)
      {
        throw new Exception("Invalid ID");
      }
      return account;
    }

    internal Account Update(Account update)
    {
      Account original = GetProfileById(update.Id);
      original.Name = update.Name.Length > 0 ? update.Name : original.Name;
      original.Email = update.Email.Length > 0 ? update.Email : original.Email;
      original.Picture = update.Picture.Length > 0 ? update.Picture : original.Picture;
      if(_repo.Update(original))
      {
        return original;
      }
      throw new Exception("Something went Wrong!");
    }
  }
}