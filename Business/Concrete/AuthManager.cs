using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entity.DTOs;
using Entity.Concrete;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        private ICustomerService _customerService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICustomerService customerService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _customerService = customerService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "AccessToken Successfully Created.");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("User doesn't exist!");
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,
                userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Wrong password!");
            }
            return new SuccessDataResult<User>(userToCheck, "Successfully Logged In.");
        }

        // public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        // {
        //     byte[] passwordHash, passwordSalt;
        //     HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        //     var user = new User()
        //     {
        //         Email = userForRegisterDto.Email,
        //         FirstName = userForRegisterDto.FirstName,
        //         LastName = userForRegisterDto.LastName,
        //         PasswordSalt = passwordSalt,
        //         PasswordHash = passwordHash,
        //         Status = true
        //     };
        //     _userService.Add(user);
        //     return new SuccessDataResult<User>(user, "Successfully Registered.");
        //}
        
        public IDataResult<User> Register(CustomerForRegisterDto customerForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var customer = new Customer()
            {
                Email = customerForRegisterDto.Email,
                FirstName = customerForRegisterDto.FirstName,
                LastName = customerForRegisterDto.LastName,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Status = true,
                CompanyName = customerForRegisterDto.CompanyName
            };
            _customerService.Add(customer);
            return new SuccessDataResult<User>(customer, "Successfully Registered.");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("This User Is Already Exists!");
            }
            return new SuccessResult("This User Is Not Exists.");
        }
    }
}
