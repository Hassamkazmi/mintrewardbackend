using Microsoft.AspNetCore.Mvc.ModelBinding;
using MintRewards.Model;

namespace MintRewards.ServiceResponder
{
    public static class ResponseHelper
    {
        public static ResponseModel GetExceptionResponse(Exception Err)
        {
            if (Err is ArgumentException)
            {
                return new ResponseModel()
                {
                    StatusCode = ResponseStatusCode.Failed,
                    Message = Err.Message
                };
            }
            return new ResponseModel()
            {
                StatusCode = ResponseStatusCode.Exception,
                Message = Err.Message,
                Error = Err.ToString()

            };
        }
        public static ResponseModel GetSuccessResponse(dynamic o, int totalCount = 0, string message = "Success")
        {
            if (o == null)
                return new ResponseModel()
                {
                    Result = null,
                    Message = "No Row Found",
                    StatusCode = ResponseStatusCode.Failed
                };
            return new ResponseModel()
            {
                Result = o,
                Message = message,
                StatusCode = ResponseStatusCode.Success
            };
        }
        public static ResponseModel GetValidationFailureResponse(ModelStateDictionary dictionary = null)
        {
            var csvMessages = dictionary.Values.Select(x => String.Join(Environment.NewLine, x.Errors.Select(y => y.ErrorMessage)));
            var errors = String.Join(Environment.NewLine, csvMessages);
            return new ResponseModel()
            {
                Response = false,
                Result = null,
                Message = errors
            };
        }
        public static ResponseModel GetSuccessResponseBoolean(Boolean o, int totalCount = 0, string message = "Success")
        {
            if (!o)
                return new ResponseModel()
                {
                    Result = o,
                    Message = "Failed",

                    StatusCode = ResponseStatusCode.Failed
                };
            return new ResponseModel()
            {
                Result = o,
                Message = "Success",
                StatusCode = ResponseStatusCode.Success
            };
        }

        public static ResponseModel UnAuthorized()
        {

            return new ResponseModel()
            {
                Result = null,
                Message = "Un-UnAuthorized",
                StatusCode = ResponseStatusCode.Failed
            };
        }

        public static ResponseModel GetFailureResponse(string message = null, ModelStateDictionary dictionary = null)
        {
            var errors = dictionary?.Select(x => x.Value.Errors).Where(y => y.Any());
            return new ResponseModel()
            {
                Response = false,
                Result = errors,
                Message = message,
                StatusCode = ResponseStatusCode.Failed
            };
        }



        public class ResponseWrapper<T>
        {

            public ResponseStatusCode status_code;
            public T response_data;
            public string message_to_show;
            public string error_internal;

        }



    }
}

