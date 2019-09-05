using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Extensions;
using Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountOwnerServer.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        public AccountController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            try
            {
                var accounts = _repository.Account.GetAllAccounts();
                _logger.LogInfo("$Retuned all accounts from the database.");
                return Ok(accounts);
            }
            catch (Exception ex)
            {

                _logger.LogInfo($"Something went wrong inside GetAllAccounts acction: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(Guid id)
        {
            try
            {
                var account = _repository.Account.GetAccountById(id);
                
                if(account.IsEmptyObject())
                {
                    _logger.LogError($"Account with id: {id} hasn't been found in the db");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned account with id: {id}");
                    return Ok(account);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAccountById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("owner")]
        public IActionResult GetAccountWithOwner()
        {
            try
            {
                var account = _repository.Account.GetAccountsWithOwners();
                _logger.LogInfo("$Retuened all accounts and their owners");
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogInfo("$Something went wrong inside GetAllAccountsWithOwners");
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpPost(Name ="ByAccountId")]
        public IActionResult CreateAccount([FromBody]Account account)
        {
            try
            {
                if (account.IsObjectNull())
                {
                    _logger.LogError("Account object sent from client is null");
                    return BadRequest("Owner Object in Null!");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Accont object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Account.CreateAccount(account);
                _repository.Save();

                return CreatedAtRoute("ByAccountId", new { account.Id }, account);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside CreateAccount action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
