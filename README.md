## Tyde
![Logo](./img/logo.jpeg){margin: 0, auto}

A simple easy to use package to consume JWT APIs and manage sessions.

### How To
Install the [Tyde](https://github.com/j0nimost/tyde/releases) package

Given an jwt auth api response like this one;
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImowbmkiLCJuYmYiOjE2NDgwMzg2MDUsImV4cCI6MTY0ODAzODY2NSwiaWF0IjoxNjQ4MDM4NjA1fQ.T3_h3tQeXRZIbio3pTkAAdDCiKFWRxlzuQNrNd912Sw",
  "expiresIn": 60,
  "expiresAt": "2022-03-23T12:35:05.8707384Z"
}
```

You can easily consume it and manage session expiry using the following steps;

- Begin by injecting the package to your instance of HttpClient like so;
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

- Finally, add `TydeDelegatingHandler` from `Tyde.Core`
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