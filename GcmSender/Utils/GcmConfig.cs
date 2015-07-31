using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GcmSender.Utils
{
    public class GcmConfig
    {
        public string _Message;
        public string _DeviceIDs;
        public string _GCMServerURL; 
        public string _SenderID;
        public string _CollapseKey;
        public string _TimeToLife ;
        public bool _DryRun;


        public string SenderID
        {
            get { return _SenderID; }
            set { SetProperty(ref _SenderID, value); }
        }


        public string CollapseKey
        {
            get { return _CollapseKey; }
            set { SetProperty(ref _CollapseKey, value); }
        }

        public string TimeToLife
        {
            get { return _TimeToLife; }
            set { SetProperty(ref _TimeToLife, value); }
        }

        public bool DryRun
        {
            get { return _DryRun; }
            set { SetProperty(ref _DryRun, value); }
        }

        public string DeviceIDs
        {
            get { return _DeviceIDs; }
            set { SetProperty(ref _DeviceIDs, value); }
        }

        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }

        public string GCMServerURL
        {
            get { return _GCMServerURL; }
            set { SetProperty(ref _GCMServerURL, value); }
        }

        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        public GcmConfig StandardConfiguration()
        {
            this.Message = "";
            this.DeviceIDs = "";
            this.GCMServerURL = "https://gcm-http.googleapis.com/gcm/send";
            this.SenderID = "";
            this.CollapseKey = "1";
            this.DryRun = false;
            this.TimeToLife = "100";
            return this;
        }
    }
}
