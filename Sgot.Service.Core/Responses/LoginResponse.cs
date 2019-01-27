using System;
namespace Sgot.Service.Core.Responses
{
    public class LoginResponse
    {
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public bool Authenticated { get; private set; }
        public string Created { get; private set; }
        public string Expiration { get; private set; }
        public string AccessToken { get; private set; }
        public string Message { get; set; }
        public LoginResponse(string id, string userName, bool authenticated, string accessToken, string message)
        {
            Id = id;
            UserName = userName;
            Authenticated = authenticated;
            AccessToken = accessToken;
            Message = message;
            Created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Expiration = DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public LoginResponse(bool authenticated, string message)
        {
            Authenticated = authenticated;
            Message = message;
        }
    }
}