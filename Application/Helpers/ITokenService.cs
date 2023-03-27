﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
