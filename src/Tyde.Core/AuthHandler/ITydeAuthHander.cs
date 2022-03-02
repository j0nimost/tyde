﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyde.Core.AuthHandler
{
    internal interface ITydeAuthHander
    {
        Task<T> SendRequestAsync<T>();
        Task SendRequestAsync();
    }
}