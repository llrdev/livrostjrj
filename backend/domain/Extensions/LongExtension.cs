namespace Domain.Extensions
{
    public static class LongExtension
    {
        public static string GetCnpjStr(this long? l)
        {
            return l.HasValue ? string.Format("{0:00000000000000}", l) : "";
        }

        public static string GetCnpjStr(this long l)
        {
            return string.Format("{0:00000000000000}", l);
        }

        public static string GetCpfStr(this long l)
        {
            return string.Format("{0:00000000000}", l);
        }

        public static string GetCpfStr(this long? l)
        {
            return l.HasValue ? string.Format("{0:00000000000}", l) : "";
        }

        public static string GetNumeroContratoSapStr(this long i)
        {
            return string.Format("{0:00000000000}", i);
        }

        public static string GetNumeroContratoSapStr(this long? i)
        {
            return i.HasValue ? string.Format("{0:00000000000}", i) : "";
        }
    }
}
