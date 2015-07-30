using GcmSender.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GcmSender.ViewModels
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        private string _GCMUrl;
        private IDialogCallback callback;

        public string GCMUrl
        {
            get { return _GCMUrl; }
            set { SetProperty(ref _GCMUrl, value); }
        }


        public Command ExitCommand
        {
            get { return new Command(Exit); }
        }

        public Command ResetConfigCommand
        {
            get { return new Command(Reset); }
        }


        public OptionsViewModel(IDialogCallback callback)
        {
            this.callback = callback;
        }

        private void Exit()
        {
            //TODO EXIT;
            callback.getGCMUrl("Test");
        }

        private void Reset()
        {
            //TODO : Reset Configuration
        }

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
    }
}
