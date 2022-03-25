## Tyde

<!-- ![Logo](./img/logo.jpeg){style="display: block; margin: 0 auto"} -->

<p align="center">
    <img src="./img/logo.jpeg" alt="logo">
</p>

[![Documentation Status](https://readthedocs.org/projects/tyde/badge/?version=latest)](https://tyde.readthedocs.io/en/latest/?badge=latest)

A simple easy to use package to manage sessions for your Client Services.

### Docs
You can find docs [here](https://tyde.readthedocs.io/en/latest/index.html)

### How To
Install the [Tyde](https://github.com/j0nimost/tyde/releases) package

### Summary
Example: Let's say you have a :code: `WeatherService`, and it requires Sessions to be refreshed after every `60` seconds.
Given an authentication response like this;

``` json
    {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImowbmkiLCJuYmYiOjE2NDgwMzg2MDUsImV4cCI6MTY0ODAzODY2NSwiaWF0IjoxNjQ4MDM4NjA1fQ.T3_h3tQeXRZIbio3pTkAAdDCiKFWRxlzuQNrNd912Sw",
        "expiresIn": 60,
        "expiresAt": "2022-03-23T12:35:05.8707384Z"
    }
```

you can manage sessions using Tyde like so;

- Begin by injecting the package to your instance of HttpClient like so;

```C#

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

Finally, add `TydeDelegatingHandler` from `Tyde.Core` to the HttpMessageHandler

```c#

    services.AddHttpClient<IWeatherService, WeatherService>(config =>
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

Now, all requests in the `WeatherService` will be Authenticated as need.

Tyde is a simple package which uses the already robust `HttpClient` library from microsoft. Ensuring the library has a small footprint, while delivering a perfect solution.


### Author
John Nyingi