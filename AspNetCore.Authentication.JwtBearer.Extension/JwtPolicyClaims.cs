namespace AspNetCore.Authentication.JwtBearer.Extension
{
    /// <summary>
    /// 功能描述    ：JwtPollicyClaims - 鉴权授权信息 
    /// 创 建 者    ：jinghe
    /// 创建日期    ：2020/12/6 14:29:12
    /// 版权说明    ：Copyright ©- 2020 -xxx 所有
    /// </summary>
    public class JwtPolicyClaims
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Roles {get;set;}
    }
}
