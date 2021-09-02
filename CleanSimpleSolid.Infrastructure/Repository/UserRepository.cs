using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanConnect.Common.Model.Settings;
using CleanSimpleSolid.Core.Interfaces;
using CleanSimpleSolid.Core.Interfaces.Repository;
using CleanSimpleSolid.Core.Model.Tasks;
using CleanSimpleSolid.Core.Model.User;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using Npgsql;
using ServiceBase.Infrastructure.Caches;
using ServiceBase.Infrastructure.Records;

namespace ServiceBase.Infrastructure.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly DbSettings _dbSettings;
        private readonly IMapper _mapper;
        //cache the user by jwt subject as that is how we will access it.
        private readonly IKeyValueCache<CssUser> _cache;

        public UserRepository(IOptions<DbSettings> dbSettings, IMapper mapper, IKeyValueCache<CssUser> cache)
        {
            _dbSettings = dbSettings.Value;
            _mapper = mapper;
            _cache = cache;
        }
        
        public async Task<CssUser> GetBySubject(string subject)
        {
            var cssUser = _cache.TryGet(subject);
            if (cssUser != null)
            {
                return cssUser;
            }
            var sql = "select id, subject as Subject, fullName as Name, email as Email, createdDate as CreatedDate, modifiedDate as ModifiedDate from css_user where subject = @sub";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.QueryAsync<CssUserRecord>(sql,new { sub=subject});
                var record = result.ToList().FirstOrDefault();
                return record == null ? null : _mapper.Map<CssUser>(record);
            }
        }

        public async Task Insert(CssUser cssUser)
        {
            var sql = "insert into css_user (subject, fullname, email, createddate, modifieddate) values (@subject, @name, @email, @createdDate, @modifiedDate)";
            var record = _mapper.Map<CssUserRecord>(cssUser);
            
            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var command = conn.CreateCommand();
                var result = conn.ExecuteScalar<long>(sql,record);
                cssUser.Id.SetIdentity(result);
                _cache.Add(cssUser.Subject, cssUser);
            }
        }

        public Task Update(CssUser user)
        {
            var sql = "update";
        }
    }
}