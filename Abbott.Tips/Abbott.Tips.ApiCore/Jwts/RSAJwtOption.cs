using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.ApiCore.Jwts
{
    public class RSAJwtOption : TipsJwtOption
    {
        public RSAJwtOption(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path can not be null", nameof(path));
            }

            Path = path;
        }

        public string Path { get; set; }
        public override SecurityKey GenerateKey()
        {
            if (RsaUtils.TryGetKeyParameters(Path, true, out var rsaParams) == false)
            {
                rsaParams = RsaUtils.GenerateAndSaveKey(Path);
            }

            return new RsaSecurityKey(rsaParams);
        }

        public override SigningCredentials GenerateCredentials()
        {
            return new SigningCredentials(GenerateKey(), SecurityAlgorithms.RsaSha256);
        }
    }
}
