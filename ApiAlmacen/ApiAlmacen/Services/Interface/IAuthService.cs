namespace ApiAlmacen.Services.Interface
{
    public interface IAuthService
    {
        bool ValidateLogin(string user, string pass);
        string GenerateToken(DateTime date, string user, TimeSpan validDate, string Password);
    }
}
