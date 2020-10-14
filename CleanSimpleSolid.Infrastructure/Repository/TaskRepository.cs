using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanConnect.Common.Model.Settings;
using CleanDdd.Common.Model.Identity;
using CleanSimpleSolid.Core.Interfaces;
using CleanSimpleSolid.Core.Model.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using ServiceBase.Infrastructure.Records;

namespace ServiceBase.Infrastructure.Repository
{
    /// <summary>
    /// Repository implementation.
    /// Implements the interface in the Core.  Inversion of Control Pattern, Core retains control as it has the interface.
    /// Allows user cases and services in core to user the repository without core needing to reference this project.
    /// </summary>
    /// <remarks>
    /// TODO: caching.
    /// </remarks>
    public class TaskRepository: ITaskRepository
    {
        private readonly IMapper _mapper;
        private readonly DbSettings _dbSettings;

        public TaskRepository(IMapper mapper, IOptions<DbSettings> options)
        {
            _mapper = mapper;
            _dbSettings = options.Value;
        }
        
        public async Task<CssTask> Get(long id, long user)
        {
            var sql = "select id, name, description, dueDate, scheduledDate from task where id = @id and userId = @uid";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.QueryAsync<CssTaskRecord>(sql,new { id=id, uid = user});
                return _mapper.Map<CssTask>(result);
            }
        }

        public async Task<IList<CssTask>> Get(long user, int index, int size)
        {
            var sql = "select id, name, description, dueDate, scheduledDate from task where userId = @uid limit @size offset @index";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.QueryAsync<CssTaskRecord>(sql,new { index = index, size = size, uid = user});
                return _mapper.Map<IList<CssTask>>(result);
            }
        }

        public async Task<CssTask> Save(CssTask cssTask)
        {
            if (cssTask.Id != null && cssTask.Id.HasValue())
            {
                return await Update(cssTask);
            }

            return await Insert(cssTask);
        }

        private async Task<CssTask> Insert(CssTask cssTask)
        {
            var sql = "insert into task (name, description, dueDate, scheduledDate, createdDate, modifiedDate) values(@name,@desc,@due,@scheduled, @createdDate, @modifiedDate) returning id";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.ExecuteScalarAsync(sql,new
                {
                    name = cssTask.Name,
                    desc = cssTask.Description, 
                    due = cssTask.DueDate, 
                    scheduled = cssTask.ScheduleDate,
                    createdDate = cssTask.CreatedDate,
                    modifiedDate = cssTask.ModifiedDate
                });
                cssTask.Id.SetIdentity((long)result);
            }

            return cssTask;
        }

        private async Task<CssTask> Update(CssTask cssTask)
        {
            var sql = "update task set name = @name, description = @desc, dueDate = @due, scheduledDate = @scheduled, modifiedDate = @modifiedDate) values(@p1,@p2) returning id";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.ExecuteScalarAsync(sql,new
                {
                    name = cssTask.Name,
                    desc = cssTask.Description, 
                    due = cssTask.DueDate, 
                    scheduled = cssTask.ScheduleDate,
                    modifiedDate = cssTask.ModifiedDate
                });
            }

            return cssTask;
        }
    }
}