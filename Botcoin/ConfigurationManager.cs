using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botcoin
{
    class ConfigurationManager : IConfigurationManager
    {
        public string SMTPUserName
        {
            get
            {
                return "botcoin.alerts@gmail.com";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string SMTPPassword
        {
            get
            {
                return "botcoin123";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string SMTPServer
        {
            get
            {
                return "smtp.gmail.com";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int SMTPPort
        {
            get
            {
                return 587;
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public string SMTPFrom
        {
            get
            {
                return "Botcoin";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string NoficationListeners
        {
            get
            {
                return "jamez1@gmail.com,angeloperera@gmail.com,victor.feoktistov@gmail.com,daniel.vakht@gmail.com,steven-johnson@live.com.au";
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
