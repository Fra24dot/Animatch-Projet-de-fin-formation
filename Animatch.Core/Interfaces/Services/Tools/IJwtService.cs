using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Tools
{
    public interface IJwtService
    {
        string GenerateToken(User user);

    }
}
