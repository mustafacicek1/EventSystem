namespace Webapi_BitirmeProjesi.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(string name,string mail, string role);
    }
}
