using System;
using System.Net.Security;
using System.Threading.Tasks;
using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;
using CleanSimpleSolid.Core.Interfaces;
using CleanSimpleSolid.Core.Interfaces.Repository;
using CleanSimpleSolid.Core.Model.Tasks;
using Microsoft.Extensions.Logging;
using ServiceBase.Core.Messages;

namespace CleanSimpleSolid.Core.UseCase
{
    /// <summary>
    /// As a user I need to be able to create a task.
    /// </summary>
    public class TaskCreatedUseCase: IUseCase<CreateTaskRequest, CreateTaskResponse>
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger<TaskCreatedUseCase> _logger;


        public TaskCreatedUseCase(ITaskRepository repository, ILogger<TaskCreatedUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public async Task<CreateTaskResponse> Process(CreateTaskRequest request)
        {
            var task = new CssTask(request.Name);
            task.SetDueDate(request.DueDate);
            task.SetDescription(request.Description);
            task.SetScheduledDate(request.ScheduledDate);
            
            //we don't allow them to save invalid tasks.
            if (task.IsValid())
            {
                try
                {
                    await _repository.Save(task);
                    return new CreateTaskResponse(true);
                }
                catch (Exception ex)
                {
                    //Oh crap errors, validations are handled by is valid.
                    _logger.LogError(ex,"Unable to create task.");
                    var errors = new Validations();
                    errors.AddError(ErrorCode.Unexpected, ex.Message);
                    return new CreateTaskResponse(false,errors);
                }
            }
            return new CreateTaskResponse(false,task.Errors);
        }
    }
}