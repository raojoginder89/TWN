using System.Net;
using System.Web.Http;
using HRA.Common.Helpers;

namespace HRA.Helpers
{
    public static class ApiControllerExtensions
    {
        /// <summary>
        /// Creates the custom response.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="apiController">The API controller.</param>
        /// <param name="content">The content.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns> The <see cref="CustomResult{T}" />. </returns>
        public static CustomResult<T> CreateCustomResponse<T>(this ApiController apiController, T content, HttpStatusCode statusCode)
        {
            return new CustomResult<T>(content, apiController, statusCode);
        }

        /// <summary>
        /// Creates the custom response.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="apiController">The API controller.</param>
        /// <param name="content">The content.</param>
        /// <returns> The <see cref="CustomResult{T}" />. </returns>
        public static CustomResult<ResultMessage<T>> CreateCustomResponse<T>(this ApiController apiController, ResultMessage<T> content)
        {
            return new CustomResult<ResultMessage<T>>(content, apiController, content.IsError ? HttpStatusCode.BadRequest : HttpStatusCode.OK);
        }

        /// <summary>
        /// The create custom response.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="apiController">The api controller.</param>
        /// <param name="content">The content.</param>
        /// <returns> The <see cref="CustomResult{T}" />. </returns>
        public static CustomResult<T> CreateCustomResponse<T>(this ApiController apiController, T content)
        {
            return new CustomResult<T>(content, apiController, HttpStatusCode.OK);
        }
    }
}
