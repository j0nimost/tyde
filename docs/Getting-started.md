## Getting Started
### What is Tyde
Tyde is a small library to manage sessions while you consume JWT tokens in your services. If you have ever integrated with a with an API service which requires JWT Session tokens, you know the hustle of writting a session handler to allow your service to run with no interruptions. 

Tyde is a simple package which uses the already robust `HttpClient` library from microsoft. Ensuring the library has a small footprint, while delivering a perfect solution.

### The Setup
Install the [Tyde]() package

Begin by injecting the package to your instance of HttpClient like so;
```csharp
        services.AddHttpClient<ITydeAuthService, TydeAuthService>(config =>
          {
              config.BaseAddress = new Uri("https://localhost:7157");
          })
        .AddTyde(opts =>
        {
            opts.AuthenticationUrl = new Uri("https://localhost:7157/api/AuthAPI/SignIn");
            opts.AuthorizingParameters = new Dictionary<string, string>()
            {
                {"username", "j0ni" },
                {"password", "sdfdsdsd" }
            };

        })
```

Finally, add `TydeDelegatingHandler` from `Tyde.Core`
```csharp
        services.AddHttpClient<ITydeAuthService, TydeAuthService>(config =>
          {
              config.BaseAddress = new Uri("https://localhost:7157");
          })
        .AddTyde(opts =>
        {
            opts.AuthenticationUrl = new Uri("https://localhost:7157/api/AuthAPI/SignIn");
            opts.AuthorizingParameters = new Dictionary<string, string>()
            {
                {"username", "j0ni" },
                {"password", "sdfdsdsd" }
            };

        }).AddHttpMessageHandler(c => c.GetService<Tyde.Core.TydeHttpDelegatingHandler>()); //mandatory
```

