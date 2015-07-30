using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcmSender.Utils
{
    public class GcmConfig
    {
        public string Message { get; set; }
        public string Devices { get; set; }
        public string GCMServerURL { get; set; }
        public string ApitToken { get; set; }
        public string CollapseKey { get; set; }
        public string TimeToLife { get; set; }
        public bool DryRun { get; set; }

        public GcmConfig StandardConfiguration()
        {
            this.Message = "";
            this.Devices = "";
            this.GCMServerURL = "https://gcm-http.googleapis.com/gcm/send";
            this.ApitToken = "";
            this.CollapseKey = "1";
            this.DryRun = false;
            this.TimeToLife = "100";
            return this;
        }
    }
}
