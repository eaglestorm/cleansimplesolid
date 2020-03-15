using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;
using CleanSimpleSolid.Core.Interfaces;
using ServiceBase.Core.Messages;

namespace ServiceBase.Core.UseCase
{
    public class UpdateExampleUseCase : IUseCase<UpdateExampleRequest, UpdateExampleResponse>
    {
        private readonly ITaskRepository _repository;

        public UpdateExampleUseCase(ITaskRepository repository)
        {
            _repository = repository;
        }
        
        public UpdateExampleResponse Process(UpdateExampleRequest request)
        {
            try
            {
                var example = _repository.Get(request.Id).Result;

                example.SetDescription(request.Property1);
                if (example.IsValid())
                {
                    _repository.Save(example);
                    return new UpdateExampleResponse(true);
                }
                else
                {
                    return new UpdateExampleResponse(false, example.Errors);
                }
            }
            catch (Exception ex)
            {
                var validations = new Validations();
                validations.AddError(ErrorCode.Unexpected, "Unable to update example model.");
                return new UpdateExampleResponse(false, validations);
            }
        }
    }
}