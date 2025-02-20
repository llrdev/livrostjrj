using Domain.Core.Result;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Webapi.Controllers.Core;
using Webapi.Core;
using Webapi.Core.Helpers;
using Webapi.Core.Response;

namespace Webapi.Extensions
{
    public static class ActionResultExtensions
    {
        public static ActionResult FromResult<T>(this BaseApiController controller, Result<T> result)
        {
            return result.Type switch
            {
                ResultTypeEnum.Success => controller.Ok(new Response<T>(result.Data, result.Success)),
                ResultTypeEnum.NotFound => controller.NotFound(new Response<T>(result.Errors)),
                ResultTypeEnum.NoContent => controller.NoContent(),
                ResultTypeEnum.Invalid => controller.BadRequest(new Response<T>(result.Errors)),
                _ => throw new Exception("Um resultado não tratado ocorreu em uma chamada de serviço."),
            };
        }

        public static ActionResult FromPageResult<T>(this BaseApiController controller, HttpRequest request, IUriHelper uriService, Pagination validFilter, Result<List<T>> result)
        {
            return result.Type switch
            {
                ResultTypeEnum.Page => controller.Ok(PaginationHelper.CreatePagedReponse(request, uriService, validFilter, result.Data, result.TotalRecords, result.Success)),
                _ => FromResult(controller, result),
            };
        }
    }
}
