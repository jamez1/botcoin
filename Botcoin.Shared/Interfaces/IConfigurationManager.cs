using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin
{
    public interface IConfigurationManager
    {
        string SMTPUserName { get; set; }

        string SMTPPassword { get; set; }

        string SMTPServer { get; set; }

        int SMTPPort { get; set; }

        string SMTPFrom { get; set; }

        string NoficationListeners { get; set; }
    }
}
