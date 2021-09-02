using System;
using System.Data;
using System.Text;
using DbUp.Engine;

namespace NjantPublish.Db.Scripts.Code
{
    /// <summary>
    /// Used for development only to drop all the tables and recreate them.
    /// </summary>
    public class DropScript: IScript
    {
        public string ProvideScript(Func<IDbCommand> dbCommandFactory)
        {
            var cmd = dbCommandFactory();
            cmd.CommandText = @"
                DROP SCHEMA public CASCADE;
                CREATE SCHEMA public;
                GRANT ALL ON SCHEMA public TO cssdemo;
                GRANT ALL ON SCHEMA public TO public;
            ";

            cmd.ExecuteNonQuery();
                
            return "";
        }
    }
}