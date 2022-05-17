namespace Webapi_BitirmeProjesi.DTOs
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
