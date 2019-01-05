using Abbott.Tips.ApiCore.Jwts.Exts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.ApiCore.Jwts
{
    public class RSAJwtOption : BasicJwtOption
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
            if (RSAUtil.TryGetKeyParameters(Path, true, out var rsaParams) == false)
            {
                rsaParams = RSAUtil.GenerateAndSaveKey(Path);
            }

            return new RsaSecurityKey(rsaParams);
        }

        public override SigningCredentials GenerateCredentials()
        {
            return new SigningCredentials(GenerateKey(), SecurityAlgorithms.RsaSha256);
        }
    }

    public class MD5JwtOption : BasicJwtOption
    {
        public MD5JwtOption(string secret)
        {
            Secret = secret ?? throw new ArgumentNullException(nameof(secret));
            Secret = Secret.GetMd5();
        }

        public string Secret { get; set; }
        public override SecurityKey GenerateKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }

        public override SigningCredentials GenerateCredentials()
        {
            return new SigningCredentials(GenerateKey(), SecurityAlgorithms.HmacSha256);
        }
    }
}
