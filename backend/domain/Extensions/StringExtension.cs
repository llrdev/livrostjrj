using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Domain.Extensions
{
    public static class StringExtension
    {
        public static string GetMD5Hash(this string input)
        {
            try
            {
                // Use input string to calculate MD5 hash
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Convert the byte array to hexadecimal string
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }
                    return sb.ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        public static string GetTipoUsoPagamento(this string input)
        {
            string result = "Agua e Esgoto";


            if (input == "1")
            {
                result = "Agua";
            }

            if (input == "2")
            {
                result = "Esgoto";
            }

            return result;

        }

        public static bool AreThereOnlyNumbers(this string str)
        {
            bool isIt = true;
            if (string.IsNullOrWhiteSpace(str))
                return false;

            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (char c in str)
            {
                if (!numbers.Contains(c))
                {
                    isIt = false;
                    break;
                }
            }

            return isIt;
        }

        public static bool AreThereOnlyAlphabet(this string str)
        {
            bool isIt = true;
            if (string.IsNullOrWhiteSpace(str))
                return false;

            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (char c in str)
            {
                if (numbers.Contains(c))
                {
                    isIt = false;
                    break;
                }
            }

            return isIt;
        }

        public static string GetOnlyNumbers(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            string newStr = "";
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (char c in str)
            {
                if (numbers.Contains(c))
                    newStr += c;
            }

            return newStr;
        }

        public static string GetOnlyAlphabet(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            string newStr = "";
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (char c in str)
            {
                if (!numbers.Contains(c))
                    newStr += c;
            }

            return newStr;
        }
        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressToBase64(this string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressFromBase64(this string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static bool IsOnlyNumber(this string str)
        {
            return str.All(char.IsDigit);
        }

    }
}
