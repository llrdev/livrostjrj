namespace Domain
{
    public class FileServerConfig
    {
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DefaultClassName { get; set; }
        public string DefaultRecords { get; set; }
        public string DefaultPathFolder { get; set; }
        public string DefaultCookieAnonymousName { get; set; }
    }

    //public class SystemImportFromDiskConfig
    //{
    //    public string SAPContratosFileName { get; set; }
    //    public string SAPContratosDiskPath { get; set; }

    //    public string SAPFornecedoresFileName { get; set; }
    //    public string SAPFornecedoresDiskPath { get; set; }

    //    public string SAPContratoItensFileName { get; set; }
    //    public string SAPContratoItensDiskPath { get; set; }
    //}

    public class LdapServerConfig
    {
        public string Domain { get; set; }
        public string Uri { get; set; }
        public string SearchFilter { get; set; }
    }

    public class EmailServerConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string FromMail { get; set; }
        public string FromMailDisplayName { get; set; }
        public string FromPassword { get; set; }
    }

    public class SecurityConfig
    {
        public string Secret { get; set; }
    }
}