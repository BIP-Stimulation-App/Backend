namespace StimulationAppAPI
{
    public static class EmailConfiguration
    {
        public static string Name { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string SmtpServer { get; set; }
        public static int SmtpPort { get; set; }
        public static bool SmtpSSL { get; set; }
    }
}
