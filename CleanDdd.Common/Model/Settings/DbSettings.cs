namespace CleanConnect.Common.Model.Settings
{
    /// <summary>
    /// Database settings
    /// </summary>
    public class DbSettings
    {
        public string Host { get; set; }
        
        public string Database { get; set; }
        
        public string EncryptedPassword { get; set; }
        
        public string PlainPassword { get; set; }
        
        public string Username { get; set; }
        
        public string Port { get; set; }

        public string GetConnectionString()
        {
            //TODO: encrypt password.
            return $"User ID={Username};Password={PlainPassword};host={Host};Port={Port};Database={Database};Pooling=True;Enlist=True";
        }
    }
}