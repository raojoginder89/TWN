﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Results;

namespace HRA.Helpers
{
    /// <summary>
    /// The custom response.
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    public class CustomResult<T> : NegotiatedContentResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResult{T}" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="statusCode">The status Code.</param>
        public CustomResult(T content, ApiController controller, HttpStatusCode statusCode)
            : base(statusCode, content, controller)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResult{T}" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentNegotiator">The content negotiator.</param>
        /// <param name="request">The request.</param>
        /// <param name="formatters">The formatters.</param>
        /// <param name="statusCode">The status code.</param>
        public CustomResult(T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters, HttpStatusCode statusCode)
            : base(statusCode, content, contentNegotiator, request, formatters)
        {
        }
    }
}
