using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.ApiCore.Jwts
{
    public class JwtAuthenticationParameters
    {
        /// <summary>
        /// the value must be password
        /// the value must be refresh_token
        /// </summary>
        public string grant_type { get; set; }

        /// <summary>
        /// the client_id is assigned by manager
        /// </summary>
        public string client_id { get; set; }

        /// <summary>
        /// the client_secret is assigned by manager
        /// </summary>
        public string client_secret { get; set; }

        /// <summary>
        /// the name of the user
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// the password of the user
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// after authentication the server will return a refresh_token
        /// </summary>
        public string refresh_token { get; set; }
    }

    public class JwtAuthenticationResult
    {
        public string Id { get; set; }

        public string RefreshToken { get; set; }

        public string Token { get; set; }

        public int IsStop { get; set; }

        public string ClientId { get; set; }

        public string[] Claims { get; set; }
    }

}
