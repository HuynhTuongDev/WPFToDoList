using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HashPassword
    {
        public string HashPass(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // Chuyển đổi mật khẩu sang byte và băm
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // Chuyển đổi mảng byte thành chuỗi hex
                var builder = new System.Text.StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
