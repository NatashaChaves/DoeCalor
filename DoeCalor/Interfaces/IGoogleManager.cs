using DoeCalor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoeCalor.Interfaces
{
    public interface IGoogleManager
    {
        void Login(Action<GoogleUser, string> OnLoginComplete);
        void Logout();
    }
}
