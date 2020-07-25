using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;
using CleanSimpleSolid.Core.Interfaces;
using ServiceBase.Core.Messages;

namespace ServiceBase.Core.UseCase
{
    /// <summary>
    /// Use case for updating a todo.
    /// </summary>
    public class UpdateTaskUseCase : IUseCase<UpdateTaskRequest, UpdateTaskResponse>
    {
        private readonly ITaskRepository _repository;
        private readonly IUserRepository _userRepository;

        public UpdateTaskUseCase(ITaskRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        
        public async Task<UpdateTaskResponse> Process(UpdateTaskRequest request)
        {
            try
            {
                var user = await _userRepository.GetBySubject(request.Subject);
                var cssTask = _repository.Get(request.Id, user.Identity.Id).GetAwaiter().GetResult();

                cssTask.SetDescription(request.Description);
                cssTask.SetName(request.Name);
                cssTask.SetDueDate(request.DueDate);
                cssTask.SetScheduledDate(request.ScheduledDate);

                if (cssTask.IsValid())
                {
                    await _repository.Save(cssTask);
                    return new UpdateTaskResponse(true);
                }
                else
                {
                    return new UpdateTaskResponse(false, cssTask.Errors);
                }
            }
            catch (Exception ex)
            {
                var validations = new Validations();
                validations.AddError(ErrorCode.Unexpected, "Unable to update example model.");
                return new UpdateTaskResponse(false, validations);
            }
        }
    }
}