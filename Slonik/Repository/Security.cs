using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slonik.Repository
{
    public class Security //Собственно сам класс с алгоритмами шифратора/дешифратора
    {
        //генератор повторений пароля
        private string GetRepeatKey(string s, int n)
        {
            var r = s;
            while (r.Length < n)
            {
                r += r;
            }

            return r.Substring(0, n);
        }

        private string GetRandomKey(int k, int len)
        {
            var gamma = string.Empty;
            var rnd = new Random(k);
            for (var i = 0; i < len; i++)
            {
                gamma += ((char)rnd.Next(35, 126)).ToString();
            }

            return gamma;
        }

        //метод шифрования/дешифровки
        private string Cipher(string login, string pass)
        {
            int k = 0;
            for(int i = 0; i < pass.Length; i++)
            {
                k += pass[i];
            }

            var currentKey = GetRepeatKey(GetRandomKey(k/2,pass.Max()), login.Length);
            var res = string.Empty;

           for (var i = 0; i < login.Length; i++)
           {
              res += ((char)(login[i] ^ currentKey[i])).ToString();
           }

            return res;
        }

        //шифрование текста
        public string Encrypt(string plainText, string password)
            => Cipher(plainText, password);

        //расшифровка текста
        public string Decrypt(string encryptedText, string password)
            => Cipher(encryptedText, password);

        public bool CheckCookie(HttpCookie cookie, string Position) //Проерка на валидность куки
        {
            try
            {
                if (cookie["Position"] != null && (cookie["Position"] == Position || cookie["Position"] == "admin")) { return true; } //Если куки есть, и должность == должности страниции (или пользователь админ), то вход разрешён
                else { return false; } //Доступа нет
            }
            catch (Exception) { return false; } //Когда куки нет, вылазит исключение. 
        }
    }
}