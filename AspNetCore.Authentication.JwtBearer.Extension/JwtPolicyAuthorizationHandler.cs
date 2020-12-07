using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore.Authentication.JwtBearer.Extension
{
    /// <summary>
    /// 功能描述    ：JwtPolicyAuthorizationHandler  
    /// 创 建 者    ：jinghe
    /// 创建日期    ：2020/12/6 14:41:02
    /// 版权说明    ：Copyright ©- 2020 -xxx 所有
    /// </summary>
    public class JwtPolicyAuthorizationHandler : IPolicyAuthorizationHandler
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly JwtPolicyConfiguration _settings;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="setting"></param>
        public JwtPolicyAuthorizationHandler(IOptions<JwtPolicyConfiguration> setting)
        {
            this._settings = setting.Value;
        }

        /// <summary>
        /// 构建令牌
        /// </summary>
        /// <param name="claims">通用参数</param>
        /// <returns></returns>
        public dynamic BuildToken(JwtPolicyClaims claims)
        {
            var privateKey = _settings.PrivateKey?.ToByteArray();
            if (privateKey == null || privateKey.Length == 0)
                throw new Exception("private key is null...");
            RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);

            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };

            /**
             * DateTime.UtcNow与DateTime.Now的区别：
             * UtcNow：读取的是世界标准时区的当前时间
             * Now：读取的是当前操作系统的时间。
             */
            var now = DateTime.UtcNow;
            var unixTimeSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();

            //access_token 创建参数
            var jwt = new JwtSecurityToken(
                audience: _settings.Audience,
                issuer: _settings.Issuer,
                claims: new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Iat, unixTimeSeconds.ToString(), ClaimValueTypes.Integer64),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(nameof(claims.UserName), claims.UserName),
                    new Claim(nameof(claims.Roles), claims.Roles)
                },
                notBefore: now,
                expires: now.AddHours(_settings.Expire_in),
                signingCredentials: signingCredentials
                );
            //refresh_token 创建参数
            string access_token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new
            {
                status = 200,
                access_token = access_token,
                //refresh_token = "",
                expire_in = TimeSpan.FromHours(_settings.Expire_in).TotalMilliseconds,
                token_type = "Bearer"
            };
        }

        /// <summary>
        /// 校验令牌
        /// </summary>
        /// <param name="token">token参数</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public bool ValidToken(string token, out string message)
        {
            try
            {
                var publicKey = _settings.PublicKey.ToByteArray();
                if (publicKey == null || publicKey.Length == 0)
                {
                    message = "public key is null...";
                    return false;
                }
                RSA rsa = RSA.Create();
                rsa.ImportRSAPublicKey(publicKey, out _);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _settings.Issuer,
                    ValidAudience = _settings.Audience,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new RsaSecurityKey(rsa)
                };
                
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, validationParameters, out _);
                message = "token is approved ...";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
