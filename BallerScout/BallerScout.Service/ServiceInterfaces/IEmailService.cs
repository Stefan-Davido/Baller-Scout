using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface IMyEmailService
    {
        public void SendEmail(string email, string code);
    }
}
