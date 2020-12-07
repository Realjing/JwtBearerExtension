namespace AspNetCore.Authentication.JwtBearer.Extension
{
    /// <summary>
    /// 功能描述    ：JwtConfiguration - 配置文件信息 
    /// 创 建 者    ：jinghe
    /// 创建日期    ：2020/12/6 14:27:08
    /// 版权说明    ：Copyright ©- 2020 -xxx 所有
    /// </summary>
    public class JwtPolicyConfiguration
    {

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; } = "system";

        /// <summary>
        /// 受众
        /// </summary>
        public string Audience { get; set; } = "everyone";

        /// <summary>
        /// 过期时间
        /// 默认：10小时
        /// </summary>
        public double Expire_in { get; set; } = 10;

        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey { get; set; }
    }
}
