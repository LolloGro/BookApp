namespace BookApp.Services;

public interface IAuthService
{
    User Register(UserRegisterDto user);
    string Login(UserLoginDto user);
}