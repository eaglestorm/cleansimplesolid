using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using CleanConnect.Common.Model.Errors;
using CleanDdd.Common.Model.Errors;
using Microsoft.AspNetCore.Mvc;
using ServiceBase.Dto;

namespace ServiceBase
{
    public static class ApiHelpers
    {
        public static JsonResult GetErrorResult(Validations validations, IMapper mapper)
        {
            var result = new JsonResult(new ErrorResponseDto()
            {
                Errors = mapper.Map<IList<ClientError>,IList<ErrorDto>>(validations.Errors.ToList())
            });
            result.StatusCode = (int?)HttpStatusCode.BadRequest;
            return result;
        }
    }
}