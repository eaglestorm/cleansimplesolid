using System.Threading.Tasks;
using AutoMapper;
using CleanConnect.Common.Model.Settings;
using CleanSimpleSolid.Core.Interfaces;
using CleanSimpleSolid.Core.Model.Tasks;
using CleanSimpleSolid.Core.Model.User;
using Dapper;
using Dapper.Contrib.Extensions;
using Npgsql;
using ServiceBase.Infrastructure.Records;

namespace ServiceBase.Infrastructure.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly DbSettings _dbSettings;
        private readonly IMapper _mapper;

        public UserRepository(DbSettings dbSettings, IMapper mapper)
        {
            _dbSettings = dbSettings;
            _mapper = mapper;
        }
        
        public async Task<CssUser> GetBySubject(string subject)
        {
            var sql = "select id, subject, name, email from css_user where subject = @sub";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.QueryAsync<CssUserRecord>(sql,new { subject=subject});
                return _mapper.Map<CssUser>(result);
            }
        }

        public async Task Insert(CssUser cssUser)
        {
            var sql = "insert into css_user (subject, name, email) values (@subject, @name, @email)";

            var record = _mapper.Map<CssUserRecord>(cssUser);
            
            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.InsertAsync(record);
            }
            
        }
    }
}