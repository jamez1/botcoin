using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin.Shared.Interfaces
{
    public interface ISMTPProvider
    {
        bool Send(string subject, string body);
    }
}
