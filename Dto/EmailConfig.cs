﻿namespace vyroute.Dto
{
    public class EmailConfig
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}
