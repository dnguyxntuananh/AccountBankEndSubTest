using AccountBank.Models;

namespace AccountBank.Services;

public interface AccountService
{
    public bool Exists(string username);
    public bool Create(Account account);
    public bool Login(string username, string password);
}
