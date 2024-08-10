using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService: IAuthenticationService{
    private IJwtTokenGenerator _jwtTokenGenerator;
    private IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository){
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password){
        // 1. Validate the user exists
        if(_userRepository.GetUserByEmail(email) is not User user){
            throw new Exception("User with email doesn't exit.");
        }
        // 2. Validate the password is correct
        if(user.Password != password){
            throw new Exception("email or password are invalid.");
        }
        // 3. Create JWT token 
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
    public AuthenticationResult Register(string firstName, string lastName, string email, string password){
        // 1.Validate User Doesn't exist
            if(_userRepository.GetUserByEmail(email) is not null){
                throw new Exception("User with given email already exists.");
            }
        // 2.Create User (generate unique Id) & Persist to DB
            User user = new() { 
                FirstName = firstName, 
                LastName = lastName, 
                Email = email, 
                Password = password 
            };
            _userRepository.Add(user);
        // 3.Create JWT Token
        
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}