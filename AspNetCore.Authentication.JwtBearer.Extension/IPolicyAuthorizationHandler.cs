namespace AspNetCore.Authentication.JwtBearer.Extension
{
    /// <summary>
    /// 功能描述    ：IAuthorizationHandler  
    /// 创 建 者    ：jinghe
    /// 创建日期    ：2020/12/6 14:41:46
    /// 版权说明    ：Copyright ©- 2020 -xxx 所有
    /// </summary>
    public interface IPolicyAuthorizationHandler
    {
        dynamic BuildToken(JwtPolicyClaims claims);
        bool ValidToken(string token, out string message);
    }
}
