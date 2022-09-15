﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace PBIPortal.Core.Middleware
{
    public class HttpRequestMiddleware
    {
        public static Func<RequestDelegate, RequestDelegate> Context
        {
            get
            {
                return next => async context =>
                {

                    var stream = context.Request.Body;
                    if (stream == Stream.Null || stream.CanSeek)
                    {
                        await next(context);

                        return;
                    }
                    try
                    {
                        using (var buffer = new MemoryStream())
                        {
                            // Copy the request stream to the memory stream.
                            await stream.CopyToAsync(buffer);

                            // Rewind the memory stream.
                            buffer.Position = 0L;

                            // Replace the request stream by the memory stream.
                            context.Request.Body = buffer;

                            // Invoke the rest of the pipeline.
                            await next(context);
                        }
                    }

                    finally
                    {
                        // Restore the original stream.
                        context.Request.Body = stream;
                    }

                };

            }
        }
    }
   
}
