using FluentValidation;
using PierreMilo.Infrastructure.Exceptions;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace PierreMilo.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;

        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                ErrorModel error = new ErrorModel();
                error.Message = "Ocurrio un error";
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                switch (exception)
                {
                    case ArgumentNullException://204 - Servicio aceptado pero resultado nulo
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        error.Status = (int)HttpStatusCode.NoContent;
                        error.Exception = "Content Exception";
                        error.Message = "No se encontraron resultados";
                        break;
                    case ValidationException e://400 - Error en la validacion de campos
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        error.Status = (int)HttpStatusCode.BadRequest;
                        error.Exception = "Validation Excepction";
                        error.Message = "Error en la validación de campos.";
                        if (!env.IsProduction())
                        {
                            var errors = new Dictionary<string, string[]>();
                            foreach (var err in e.Errors)
                            {
                                errors.Add(err.PropertyName, new string[] { err.ErrorMessage });
                            }
                            error.Data = errors;
                        }
                        break;
                    case UnauthorizedException e://401 - Error de Cliente (Unauthorized)
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        error.Status = (int)HttpStatusCode.Unauthorized;
                        error.Exception = "Unauthorized Exception";
                        error.Message = e.Message;
                        break;
                    case ForbiddenException e://403 - Error de Cliente (Forbidden)
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        error.Status = (int)HttpStatusCode.Forbidden;
                        error.Exception = "Forbidden Exception";
                        error.Message = e.Message;
                        break;
                    case NotFoundException e://404 - Error de Cliente (Not Found)
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        error.Status = (int)HttpStatusCode.NotFound;
                        error.Exception = "Not Found Exception";
                        error.Message = e.Message;
                        break;
                    case InternalServerErrorException e://500 - Error de Servidor (Internal Server Error)
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        error.Status = (int)HttpStatusCode.InternalServerError;
                        error.Exception = "Internal Server Exception";
                        error.Data = e.Message;
                        break;
                    case SqlException e://502 - Error en Servidor (Bad Gateway)
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        error.Status = (int)HttpStatusCode.BadGateway;
                        error.Exception = "Sql Exception";
                        error.Message = e.Message;
                        break;
                    case DBConcurrencyException e://502 - Error de Servidor (Bad Gateway)
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        error.Status = (int)HttpStatusCode.BadGateway;
                        error.Exception = "Query Exception";
                        error.Message = e.Message;
                        break;
                    case NetworkAuthenticationRequiredException e://511 - Error de Servidor (Network Authentication Required)
                        response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                        error.Status = (int)HttpStatusCode.NetworkAuthenticationRequired;
                        error.Exception = "Internal Server Exception";
                        error.Data = e.Message;
                        break;
                    default: //500 - unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        error.Status = (int)HttpStatusCode.InternalServerError;
                        error.Message = exception.Message;
                        error.Exception = "Not Handler Exception";
                        break;
                }
                await response.WriteAsJsonAsync(error);
            }
        }
    }
    public class ErrorModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public dynamic? Data { get; set; }
    }
}
