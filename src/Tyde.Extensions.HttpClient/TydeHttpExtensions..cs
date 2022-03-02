﻿using Microsoft.Extensions.DependencyInjection;
using Tyde.Shared.Configurations;

namespace Tyde.Extensions.HttpClient;
public static class TydeHttpExtensions
{
    public static IHttpClientBuilder AddTyde(this IHttpClientBuilder httpClientBuilder)
    {
        if(httpClientBuilder == null)
            throw new ArgumentNullException(nameof(httpClientBuilder));

        // TODO: implement

        return httpClientBuilder;
    }

    public static IHttpClientBuilder AddTyde(this IHttpClientBuilder httpClientBuilder, Action<TydeConfiguration> configureTyde)
    {
        if (httpClientBuilder == null)
            throw new ArgumentNullException(nameof(httpClientBuilder));

        if (configureTyde == null)
            throw new ArgumentNullException(nameof(configureTyde));

        // TODO: implement

        return httpClientBuilder;
    }
}