using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Application
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public bool IsSuccess()
        {
            return Errors == null || Errors.Count == 0;
        }

        public bool IsFailure()
        {
            return !IsSuccess();
        }

        public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T>
            {
                Data = data,
                HttpStatusCode = statusCode
            };
        }

        public static ServiceResult<T> Failure(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                Errors = errors,
                HttpStatusCode = statusCode
            };
        }

        public static ServiceResult<T> Failure(string errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                Errors = [errors],
                HttpStatusCode = statusCode
            };
        }

    }

    public class ServiceResult
    {
        

        public List<string>? Errors { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public bool IsSuccess()
        {
            return Errors == null || Errors.Count == 0;
        }

        public bool IsFailure()
        {
            return !IsSuccess();
        }

        public static ServiceResult Success( HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult
            {
                
                HttpStatusCode = statusCode
            };
        }

        public static ServiceResult Failure(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                Errors = errors,
                HttpStatusCode = statusCode
            };
        }

        public static ServiceResult Failure(string errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                Errors = [errors],
                HttpStatusCode = statusCode
            };
        }

    }
}
