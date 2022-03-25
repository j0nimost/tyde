Getting Started
===============
What is Tyde
------------
Tyde is a small library to manage sessions while you consume JWT tokens in your services. If you have ever integrated with a with an API service which requires JWT Session tokens, you know the hustle of writting a session handler to allow your service to run with no interruptions. 

Many a time, when creating an integration service we find ourselves, we find ourselves having to create a logic which refreshes JWT tokens or some form of authentication.

This sometimes can create design anomalies. Tyde, tries to solve this problem by abstracting the session management code. 

Install the package from `here <https://github.com/j0nimost/tyde/releases>`_

The Setup
---------
Example: Let's say you have a :code: `WeatherService`, and it requires Sessions to be refreshed after every `60` seconds.
Given an authentication response like this;

.. code-block:: json
    {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImowbmkiLCJuYmYiOjE2NDgwMzg2MDUsImV4cCI6MTY0ODAzODY2NSwiaWF0IjoxNjQ4MDM4NjA1fQ.T3_h3tQeXRZIbio3pTkAAdDCiKFWRxlzuQNrNd912Sw",
        "expiresIn": 60,
        "expiresAt": "2022-03-23T12:35:05.8707384Z"
    }

you can manage sessions using Tyde like so;

- Begin by injecting the package to your instance of HttpClient like so;

.. code-block:: csharp

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


Finally, add `TydeDelegatingHandler` from `Tyde.Core` to the HttpMessageHandler

.. code-block:: c#

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

Now, all requests in the `WeatherService` will be Authenticated as need.

Tyde is a simple package which uses the already robust `HttpClient` library from microsoft. Ensuring the library has a small footprint, while delivering a perfect solution.
