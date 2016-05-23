﻿using System;

namespace Aritter.Infra.Crosscutting.Security
{
    [Flags]
    public enum Rule
    {
        None = 0,
        Get = 1,
        Post = 2,
        Put = 4,
        Delete = 8
    }
}
