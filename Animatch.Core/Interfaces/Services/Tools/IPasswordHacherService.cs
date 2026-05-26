using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Tools
{
    public interface IPasswordHacherService
    {
        string HachPassword(string password);
        bool VerifyPassword(string password, string storedPassword);
    }
}
