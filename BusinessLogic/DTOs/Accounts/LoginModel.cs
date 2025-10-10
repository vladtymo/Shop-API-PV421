using System.ComponentModel;

namespace BusinessLogic.DTOs.Accounts
{
    public class LoginModel
    {
        [DefaultValue("rere@gmail.com")]
        public string Email { get; set; }

        [DefaultValue("Qwer-1234")]
        public string Password { get; set; }
    }

}