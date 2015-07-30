using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcmSender.Utils
{
    public interface IDialogCallback
    {
        string getGCMUrl(string url);
    }
}
