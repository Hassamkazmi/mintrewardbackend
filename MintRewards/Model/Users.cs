using Microsoft.AspNetCore.Http.HttpResults;
using MintRewards.Model.CustomModels;
using System.Net.Mail;

namespace MintRewards.Model
{
    public class Users : BaseEntity
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<int> RoleId { get; set; }

    }
    public class SignIn 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
 
    }
    public class SignInResponse:BaseEntity
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
         public string EmailAddress { get; set; }
        public int? RoleId { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
    }
    public class SignUp
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    }
    public class Email 
    {
        public dynamic EmailAddress { get; set; }
    }
}
