using java.lang;
using java.security;
using System.Text;

namespace betareborn
{
    public class MD5String
    {
        private readonly string field_27370_a;

        public MD5String(string var1)
        {
            field_27370_a = var1;
        }

        public string func_27369_a(string var1)
        {
            try
            {
                string var2 = field_27370_a + var1;
                MessageDigest var3 = MessageDigest.getInstance("MD5");
                var3.update(Encoding.UTF8.GetBytes(var2), 0, var2.Length);
                return (new java.math.BigInteger(1, var3.digest())).toString(16);
            }
            catch (NoSuchAlgorithmException var4)
            {
                throw new RuntimeException(var4);
            }
        }
    }
}