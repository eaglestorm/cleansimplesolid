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
            cmd.CommandText = @"DO $$ DECLARE
            r RECORD;
            BEGIN
                -- if the schema you operate on is not 'current', you will want to
            -- replace current_schema() in query with 'schematodeletetablesfrom'
                -- *and* update the generate 'DROP...' accordingly.
                FOR r IN (SELECT tablename FROM pg_tables WHERE schemaname = current_schema()) LOOP
                EXECUTE 'DROP TABLE IF EXISTS ' || quote_ident(r.tablename) || ' CASCADE';
            END LOOP;
            END $$;";

            cmd.ExecuteNonQuery();
                
            return "";
        }
    }
}