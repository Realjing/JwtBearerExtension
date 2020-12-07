using System;
using System.Security.Cryptography;

namespace AspNetCore.Authentication.JwtBearer.Extension.TestRsaKey
{
    class Program
    {
        static void Main(string[] args)
        {
            var rsa = RSA.Create();

            //导出
            Console.WriteLine("RSA PKCS#1 私钥：");
            var privatekey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            Console.WriteLine(privatekey);
           
            Console.WriteLine("RSA PKCS#1 公钥：");
            var publickey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            Console.WriteLine(publickey);
           
            //导入
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privatekey), out _);
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publickey), out _);
            Console.WriteLine("秘钥创建导入成功！");            
            Console.ReadKey(true);
        }
    }
}
