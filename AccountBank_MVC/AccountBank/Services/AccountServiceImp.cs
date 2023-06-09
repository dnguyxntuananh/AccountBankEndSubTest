﻿using AccountBank.Models;

namespace AccountBank.Services;

public class AccountServiceImp : AccountService
{
    private DatabaseContext db;
    public AccountServiceImp(DatabaseContext _db)
    {
        db = _db;
    }

    public bool Create(Account account)
    {
        try
        {
            db.Accounts.Add(account);
            return db.SaveChanges() > 0;
        }catch (Exception ex)
        {
            return false;
        }
    }

    public bool Exists(string username)
    {
        return db.Accounts.Count(x => x.Username == username) > 0;

    }

    public bool Login(string username, string password)
    {
        try
        {
            var account = db.Accounts.SingleOrDefault(acc => acc.Username == username && acc.Status == true);
            if (account != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, account.Password);
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}
