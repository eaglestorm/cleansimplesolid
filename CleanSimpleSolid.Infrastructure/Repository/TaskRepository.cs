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
    public class TaskRepository: ITaskRepository
    {
        private readonly IMapper _mapper;
        private readonly DbSettings _dbSettings;

        public TaskRepository(IMapper mapper, IOptions<DbSettings> options)
        {
            _mapper = mapper;
            _dbSettings = options.Value;
        }
        
        public async Task<Todo> Get(long id)
        {
            var sql = "select id, name, description, dueDate, scheduledDate from tasks where id = @id";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.QueryAsync<ExampleRecord>(sql,new { id=id});
                return _mapper.Map<Todo>(result);
            }
        }

        public async Task<IList<Todo>> Get(int index, int size)
        {
            var sql = "select id, name, description, dueDate, scheduledDate from tasks limit @size offset @index";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.QueryAsync<ExampleRecord>(sql,new { index = index, size = size});
                return _mapper.Map<IList<Todo>>(result);
            }
        }

        public async Task<Todo> Save(Todo todo)
        {
            if (todo.Id.HasValue())
            {
                return await Update(todo);
            }

            return await Insert(todo);
        }

        private async Task<Todo> Insert(Todo todo)
        {
            var sql = "insert into tasks (name, description, dueDate, scheduledDate) values(@name,@desc,@due,@scheduled) returning id";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.ExecuteScalarAsync(sql,new
                {
                    name = todo.Name,
                    desc = todo.Description, 
                    due = todo.DueDate, 
                    scheduled = todo.ScheduleDate
                });
                todo.Id.SetIdentity((long)result);
            }

            return todo;
        }

        private async Task<Todo> Update(Todo todo)
        {
            var sql = "update tasks set name = @name, description = @desc, dueDate = @due, scheduledDate = @scheduled) values(@p1,@p2) returning id";

            using (var conn = new NpgsqlConnection(_dbSettings.GetConnectionString()))
            {
                var result = await conn.ExecuteScalarAsync(sql,new
                {
                    name = todo.Name,
                    desc = todo.Description, 
                    due = todo.DueDate, 
                    scheduled = todo.ScheduleDate
                });
            }

            return todo;
        }
    }
}