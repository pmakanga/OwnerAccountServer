using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class AccountRepository: RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                //.Include(o => o.Owners)
                .OrderBy(a => a.AccountType)
                .ToList();
            
                
        }

        public Account GetAccountById(Guid accountId)
        {
            return FindByCondition(account => account.Id.Equals(accountId))
                .DefaultIfEmpty(new Account())
                .FirstOrDefault();
        }

        public IEnumerable<OwnerAccount> GetAccountsWithOwners()
        {
            return (from o in RepositoryContext.Owners
                    from a in RepositoryContext.Accounts
                    where o.Id == a.OwnerId
                    select new OwnerAccount
                    {
                        OwnerId = o.Id,
                        Owner = o.Name,
                        AccountId = a.Id,
                        AccountType = a.AccountType,
                        DateCreated = a.DateCreated
                    }).ToList();
        }

        public void CreateAccount(Account account)
        {
            account.Id = Guid.NewGuid();
            Create(account);
            
        }

        public OwnerAccount GetAccountWithOwner(Guid accountId)
        {
            return (from a in RepositoryContext.Accounts
                    join o in RepositoryContext.Owners on a.Id equals accountId
                    select new OwnerAccount
                    {
                        OwnerId = o.Id,
                        Owner = o.Name,
                        AccountId = a.Id,
                        AccountType = a.AccountType,
                        DateCreated = a.DateCreated
                    }).FirstOrDefault();


            //return (from o in RepositoryContext.Owners
            //        from a in RepositoryContext.Accounts
            //        where a.Id == accountId
            //        select new OwnerAccount
            //        {
            //            OwnerId = o.Id,
            //            Owner = o.Name,
            //            AccountId = a.Id,
            //            AccountType = a.AccountType,
            //            DateCreated = a.DateCreated
            //        }).FirstOrDefault();

        }

        public void UpdateAccount(Account dbAccount, Account account)
        {
            dbAccount.Map(account);
            Update(dbAccount);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }
    }
}
