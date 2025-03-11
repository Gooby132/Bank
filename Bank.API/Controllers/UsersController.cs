using Bank.API.Contracts.Commons.Dtos;
using Bank.API.Contracts.Users;
using Bank.API.Contracts.Users.Dtos;
using Bank.Domain.Commons;
using Bank.Domain.Users;
using Bank.Domain.Users.Entities.ValueObjects;
using Bank.Domain.Users.Services;
using Bank.Domain.Users.ValueObjects;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(ITransactionService transactionService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _transactionService = transactionService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("make-transaction")]
        public async Task<IActionResult> MakeTransaction(UserMakeTransactionRequest request, CancellationToken token = default)
        {

            var tz = Tz.Create(request.User?.Tz);

            if (tz.IsFailed)
                return BadRequest(new UserMakeTransactionResponse
                {
                    Errors = tz.Errors.Select(e => new ErrorDto
                    {
                        Code = (e as ErrorBase)!.Code,
                        Message = e.Message,
                    })
                });

            var operation = Operation.Create(
                    request.OperationType,
                    request.Amount,
                    request.SendTo
                );

            if (operation.IsFailed)
                return BadRequest(new UserMakeTransactionResponse
                {
                    Errors = operation.Errors.Select(e => new ErrorDto
                    {
                        Code = (e as ErrorBase)!.Code,
                        Message = e.Message,
                    })
                });

            var user = await _userRepository.GetUser(tz.Value);

            if (user.IsFailed)
                return Problem();

            if (user.Value is null)
                return NotFound();

            if (!user.Value.ValidatePassword(request.User?.Password))
                return Unauthorized();

            // should be in to steps as the db might fail persisting

            var transaction = await _transactionService.MakeTransaction(
                user.Value,
                user.Value.Account,
                operation.Value,
                token);

            if (transaction.IsFailed)
                return Problem();

            var userTransaction = user.Value.MakeTransaction(operation.Value);

            if (userTransaction.IsFailed)
                return BadRequest(new UserMakeTransactionResponse
                {
                    Errors = userTransaction.Errors.Select(e => new ErrorDto
                    {
                        Code = (e as ErrorBase)!.Code,
                        Message = e.Message,
                    })
                });

            var update = _userRepository.Update(user.Value);

            if (update.IsFailed)
                return Problem();

            var commit = await _unitOfWork.Commit();

            if (commit.IsFailed)
                return Problem();

            return Ok(new UserMakeTransactionResponse
            {
                UpdatedAccount = new AccountDto
                {
                    Currency = user.Value.Account.Currency,
                    Id = user.Value.Account.Id,
                    Operations = user.Value.Account.Operations.Select(o => new OperationDto
                    {
                        Type = (int)o.Type,
                        Value = o.Value,
                        SentTo = o.SentTo
                    })
                }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request, CancellationToken token = default)
        {

            var tz = Tz.Create(request.Tz);

            if (tz.IsFailed)
                return BadRequest(new UserLoginResponse
                {
                    Errors = tz.Errors.Select(e => new ErrorDto
                    {
                        Code = (e as ErrorBase)!.Code,
                        Message = e.Message,
                    })
                });

            var user = await _userRepository.GetUser(tz.Value, token);

            if (user.IsFailed)
                return Problem();

            if (user.Value is null)
                return NotFound();

            if (!user.Value.ValidatePassword(request.Password))
                return Unauthorized();

            return Ok(new UserLoginResponse
            {
                User = new UserDto // auto mapper can be used
                {
                    Tz = user.Value.Tz.Value,
                    FullName = user.Value.FullName.Value,
                    EnglishFullName = user.Value.EnglishFullName.Value,
                    BirthDateInUtc = user.Value.DateOfBirthInUtc.Value.ToString("O"),
                    Password = user.Value.Password.Value, // this is only for demo
                    Account = new AccountDto
                    {
                        Id = user.Value.Account.Id,
                        Currency = user.Value.Account.Currency,
                        Operations = user.Value.Account.Operations.Select(o => new OperationDto
                        {
                            Type = (int)o.Type,
                            Value = o.Value
                        })
                    }
                }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request, CancellationToken token = default)
        {

            var tz = Tz.Create(request.User?.Tz);

            if (tz.IsFailed)
                return BadRequest(new UserLoginResponse
                {
                    Errors = tz.Errors.Select(e => new ErrorDto
                    {
                        Code = (e as ErrorBase)!.Code,
                        Message = e.Message,
                    })
                });

            var doesExists = await _userRepository.GetUser(tz.Value, token);

            if (doesExists.IsFailed)
                return Problem();

            if (doesExists.Value is not null) // could be done with the unique of TZ on PG
            {
                var error = UserErrors.UserAlreadyExists();
                return UnprocessableEntity(
                    new UserRegisterResponse
                    {
                        Errors = [new ErrorDto
                        {
                            Code = error.Code,
                            Message = error.Message,
                        }]
                    });
            }

            var user = Domain.Users.User.Create(
                request.User.Tz,
                request.User.FullName,
                request.User.EnglishFullName,
                request.User.BirthDateInUtc,
                request.Password);

            if (user.IsFailed)
                return BadRequest(new UserRegisterResponse
                {
                    Errors = user.Errors
                        .Select(e => new ErrorDto
                        {
                            Code = (e as ErrorBase)!.Code, // always error base (but additional validation could be useful)
                            Message = e.Message
                        })
                });

            var register = await _userRepository.RegisterUser(user.Value, token);

            if (register.IsFailed)
                return Problem();

            var commit = await _unitOfWork.Commit();

            if (commit.IsFailed)
                return Problem();

            return Ok(new UserRegisterResponse
            {
                User = new UserDto // auto mapper can be used
                {
                    Tz = user.Value.Tz.Value,
                    FullName = user.Value.FullName.Value,
                    EnglishFullName = user.Value.EnglishFullName.Value,
                    BirthDateInUtc = user.Value.DateOfBirthInUtc.Value.ToString("O"),
                    Password = user.Value.Password.Value, // this is only for demo
                    Account = new AccountDto
                    {
                        Id = user.Value.Account.Id,
                        Currency = user.Value.Account.Currency,
                        Operations = [] // no operations are created on register
                    }
                }
            });
        }

    }
}
