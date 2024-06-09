using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Application.Notifications.Base
{
    public interface ISmsProvider
    {
        Task SendOtp(string code, string phone);
    }
}
