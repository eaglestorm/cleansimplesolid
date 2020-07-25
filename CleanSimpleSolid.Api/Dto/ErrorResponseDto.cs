using System.Collections.Generic;

namespace ServiceBase.Dto
{
    public class ErrorResponseDto
    {
        public ErrorResponseDto()
        {
            Errors = new List<ErrorDto>();
        }
        public IList<ErrorDto> Errors { get; set;  }
    }
}