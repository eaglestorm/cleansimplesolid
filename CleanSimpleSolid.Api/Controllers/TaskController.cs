using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using CleanSimpleSolid.Core.Interfaces;
using CleanSimpleSolid.Core.Interfaces.Repository;
using CleanSimpleSolid.Core.Services;
using CleanSimpleSolid.Core.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceBase.Core.Messages;
using ServiceBase.Core.UseCase;
using ServiceBase.Dto;

namespace ServiceBase.Controllers
{
    /// <summary>
    /// Controller for the Task domain model
    /// </summary>
    /// <remarks>
    /// Notice that validation errors are returned from the use case and not
    /// resulting from exceptions.
    /// Any unhandled exceptions mean something bad has happened.
    /// </remarks>
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IUserService _userService;

        public TaskController(ITaskRepository taskRepository, IMapper mapper, IUserRepository userRepository,
            ILifetimeScope lifetimeScope, IUserService userService)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _lifetimeScope = lifetimeScope;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("/task")]
        public async Task<IActionResult> Index([FromQuery] TaskSearchDto searchDto)
        {
            //we are just returning data and because the domain contains all the business logic we can just get the model 
            //and serialize the properties.
            var examples = await _taskRepository.Get(searchDto.Index, searchDto.Size == 0 ? 10 : searchDto.Size);
            return new JsonResult(_mapper.Map<List<TaskDto>>(examples));
        }

        [Authorize]
        [HttpPost("/task")]
        public async Task<IActionResult> Update([FromBody] UpdateToDoDto dto)
        {
            //We are updating something so the use case manages the business and application logic.
            //The controller method handles transforming the response.
            var usecase = new UpdateTaskUseCase(_taskRepository, _userRepository); //not sure this is good.
            var request = _mapper.Map<UpdateTaskRequest>(dto);
            request.CreatedBy = _userService.GetCurrentUser(User).Id;
            var response = await usecase.Process(request);
            if (response.Success)
            {
                return new OkResult();
            }

            return ApiHelpers.GetErrorResult(response.Errors, _mapper);
        }

        [Authorize]
        [HttpPut("/task")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            //not sure if this is the best way but can't think of a better way.
            var usecase =
                new TaskCreatedUseCase(_taskRepository, _lifetimeScope.Resolve<ILogger<TaskCreatedUseCase>>());
            var response = await usecase.Process(_mapper.Map<CreateTaskRequest>(dto));
            if (response.Success)
            {
                return new OkResult();
            }

            return ApiHelpers.GetErrorResult(response.Errors, _mapper);
        }
    }
}