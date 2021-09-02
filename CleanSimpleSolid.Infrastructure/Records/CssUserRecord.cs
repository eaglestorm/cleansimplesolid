using System;

namespace ServiceBase.Infrastructure.Records
{
    /// <summary>
    /// A record in the cssuser table.
    /// </summary>
    [Dapper.Contrib.Extensions.Table("css_user")]
    public class CssUserRecord
    {
        /// <summary>
        /// Database identity.
        /// </summary>
        public long Id { get; set;  }
        
        /// <summary>
        /// When the record was created.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
        
        /// <summary>
        /// The last time the record was modified.
        /// </summary>
        public DateTimeOffset ModifiedDate { get; set; }
        
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// External identifier for the user.  Should only change once.
        /// </summary>
        public string Subject { get; set;  }

        /// <summary>
        /// We require all users to have an email.
        /// </summary>
        public string Email { get; set; }
    }
}