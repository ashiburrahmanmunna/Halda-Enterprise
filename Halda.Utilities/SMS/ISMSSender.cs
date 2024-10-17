using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Utilities.SMS
{
    public interface  ISMSSender
    {
        Task<bool> SMSSend(string number, string msgbody);
    }
}
