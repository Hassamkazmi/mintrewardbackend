using Microsoft.AspNetCore.Authentication;
using MintRewards.Model;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using  MintRewards.ServiceResponder;
using MintRewards.Model;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;
using Microsoft.Owin.Security.OAuth;

namespace MintRewards.Services
{
    public interface IUsersService
    {
        Task<SignInResponse> AuthenticateUser(SignIn dto);
        Task<ResponseModel> CreateAccount(SignUp dto);
        Task<string> Authenticate(Users Account);
        Task<ResponseModel> SendOtpViaEmail(string senderEmail, string receiverEmail);
    }
    public class UsersService : IUsersService
    {
        private IDbConnection dbconnection;
        public UsersService()
        {
            dbconnection = new SqlConnection(Properties.Resource.ConnectionString);
        }
        public async Task<SignInResponse> AuthenticateUser(SignIn dto)
        {
            try
            {

                var user = await dbconnection.QueryFirstOrDefaultAsync<Users>(@"select * from Users where UserName=@UserName and [Password]=@Password and IsActive=1 ", dto);
                if (user == null) throw new ArgumentException("Invalid email address or password. Please try again!");

                if (user?.Id > 0)
                    return new SignInResponse
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        RoleId = user.RoleId,
                        Password = user.Password,
                    };
                return null;
            }

            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> Authenticate(Users Account)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Properties.Resource.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[] {
                   new Claim(ClaimTypes.NameIdentifier,Account.Id.ToString()),
                    //new Claim(ClaimTypes.Role,Account.RoleId.ToString()),
                    new Claim(ClaimTypes.Name,Account.UserName.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = new JwtSecurityToken(Properties.Resource.Issuer,
                  Properties.Resource.Issuer,
                  claims,
                  expires: DateTime.Now.AddDays(1),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<ResponseModel> CreateAccount(SignUp dto)
        {
            try
            {
                var Query = await dbconnection.QueryAsync($@"insert into Users(UserName,PhoneNumber,Password,EmailAddress) values(@UserName,@PhoneNumber,@Password,@EmailAddress)", new { dto.UserName, dto.PhoneNumber, dto.Password, dto.EmailAddress });
                var senderEmail = await dbconnection.QueryFirstOrDefaultAsync<Email>($@"select EmailAddress from Users where RoleId = 1");
                var Email = senderEmail;
                
                SendOtpViaEmail(senderEmail.EmailAddress,dto.EmailAddress);
                return ResponseHelper.GetSuccessResponse(Query);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<ResponseModel> SendOtpViaEmail(string senderEmail, string receiverEmail)
        {

            senderEmail = "m.sharyarzahid@gmail.com";
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(senderEmail, "03142759616"),
                    EnableSsl = true,
                    UseDefaultCredentials = false
                };

                client.EnableSsl = true;
              
                string subject = "Your One Time Password (OTP)";
                string body = $"Your OTP is: {otp}. Please use it to proceed with your request.";
                client.UseDefaultCredentials = false;
                await client.SendMailAsync(senderEmail, receiverEmail, subject, body);
                Console.WriteLine("OTP sent successfully!");

            }
            catch (Exception ex)
            {
                return ResponseHelper.GetExceptionResponse(ex);
            }
            return ResponseHelper.GetSuccessResponse("Email sent successfully!");
        }
     

    }
}
