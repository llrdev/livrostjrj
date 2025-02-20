using System;

namespace Domain.Core.Extensions
{
    public static class DataHelper
    {
        /// <summary>
        /// Formata uma string resumida de data de acordo com seu padrão definido
        /// </summary>
        /// <param name="formato">"MM/YYYY" ou  "YYYY/MM"</param>
        /// <returns></returns>
        public static string FormataData(this string data, string formato)
        {
            var result = "";
            var primeira = "";
            var segunda = "";

            switch (formato)
            {
                case "MM/YYYY":

                    primeira = data.Substring(0, 4);
                    segunda = data.Substring(4, 2);

                    result = $"{segunda}/{primeira}";

                    break;

                case "YYYY/MM":

                    primeira = data.Substring(0, 4);
                    segunda = data.Substring(4, 2);

                    result = $"{primeira}/{segunda}";

                    break;
            }

            return result;
        }

        public static DateTime AaMmToDatetime(this string data, string formato = "YYYY/MM")
        {

            DateTime date;
            DateTime.TryParse(data.FormataData(formato), out date);
            return date;
        }

        public static bool ValidaMesAno(this string data)
        {
            if (string.IsNullOrEmpty(data) || data.Length != 6)
            {
                return false;
            }
            var result = false;
            var resultMes = false;
            var resultAno = false;

            var primeira = data.Substring(0, 4);
            var segunda = data.Substring(4, 2);

            int mes;

            if (int.TryParse(segunda, out mes))
            {
                resultMes = mes >= 1 && mes <= 12;
            }

            int ano;

            if (int.TryParse(primeira, out ano))
            {

                resultAno = ano >= 2000 && ano <= 2199;
            }

            if (resultAno && resultMes)
            {
                result = true;
            }

            return result;
        }

        public static int GetAAMMformat(this DateTime date)
        {

            return int.Parse(date.Year + (date.Month >= 10 ? "" + date.Month : "0" + date.Month));
        }
    }
}
