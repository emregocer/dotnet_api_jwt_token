using System;
using System.Security.Cryptography;
using AuthServer.Api.Models;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace AuthServer.Api
{
    public static class AudienceStore
    {
        public static Audience GetTestAudience()
        {
            return new Audience { Name = "test_resource1", ClientId = "91493eb12406451599cd6f2f868f71dc", Base64Secret = "FtbS5vFaccdXuLWKAxJjhaYqZKoLF543xG4vA6JEMOw" };
        }

        public static Audience GenerateAudience(string name)
        {
            //"N" -> 32 digits not seperated and not in braces. 
            //"D" -> 32 digits seperated by hyphens. 
            //"B" -> 32 digits seperated by hyphens and enclosed in braces.
            var clientId = Guid.NewGuid().ToString("N");
            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64secret = TextEncodings.Base64Url.Encode(key);

            return new Audience { Name = name, ClientId = clientId, Base64Secret = base64secret };
        }


    }
}