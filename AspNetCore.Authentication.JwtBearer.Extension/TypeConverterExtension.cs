using System;

namespace AspNetCore.Authentication.JwtBearer.Extension
{
    /// <summary>
    /// 功能描述    ：TypeConverterExtension  
    /// 创 建 者    ：jinghe
    /// 创建日期    ：2020/12/6 15:04:54
    /// 版权说明    ：Copyright ©- 2020 -xxx 所有
    /// </summary>
    public static class TypeConverterExtension
    {
        /// <summary>
        /// 扩展方法-
        /// base64字符串 转化为字节数组
        /// </summary>
        /// <param name="source">base64字符串</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string source) => Convert.FromBase64String(source);
    }
}
