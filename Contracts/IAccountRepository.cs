using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IEnumerable<Account> AccountsByOwner(Guid ownerId);
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<OwnerAccount> GetAccountsWithOwners();
        OwnerAccount GetAccountWithOwner(Guid accountId);
        Account GetAccountById(Guid accountId);
        void CreateAccount(Account account);
        void UpdateAccount(Account dbOwner, Account owner);
        void DeleteAccount(Account account);

    }

  
}
