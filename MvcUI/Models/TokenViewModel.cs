using System;

namespace MvcUI.Models
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
