## Tyde
![Logo](./img/logo.jpeg)

A simple easy to use package to consume JWT APIs and manage sessions.

### How To
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
### Author
John Nyingi