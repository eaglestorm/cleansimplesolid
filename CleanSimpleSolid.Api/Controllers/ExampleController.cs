using System.Collections.Generic;
using AutoMapper;
using CleanSimpleSolid.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceBase.Core.Messages;
using ServiceBase.Core.UseCase;
using ServiceBase.Dto;

namespace ServiceBase.Controllers
{
    public class ExampleController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public ExampleController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        
        [Authorize]
        [HttpGet("/")]
        public IActionResult Index()
        {
            //we are just returning data and because the domain contains all the business logic we can just get the model 
            //and serialize the properties.
            var examples = _taskRepository.Get(0, 10);       
            return new JsonResult(_mapper.Map<List<TaskDto>>(examples));
        }

        [Authorize]
        [HttpPost("/")]
        public IActionResult Update(UpdateExampleDto dto)
        {
            //We are updating something so the use case manage the business and application logic.
            //The controller method handles transforming the response.
            var usecase = new UpdateExampleUseCase(_taskRepository);
            var ressponse = usecase.Process(new UpdateExampleRequest(dto.Id,dto.Property1));
            if (ressponse.Success)
            {
                return new OkResult();
            }

            return ApiHelpers.GetErrorResult(ressponse.Errors, _mapper);
        } 
    }
}