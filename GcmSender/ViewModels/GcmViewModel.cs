﻿using GcmSender.Models;
using GcmSender.Utils;
using GcmSender.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GcmSender.ViewModels
{
    public class GcmViewModel : INotifyPropertyChanged
    {
        #region Constructor

        public GcmViewModel()
        {
            this.Model = new GcmModel();
            Model.LoadConfigurationStartup();
        }

        #endregion

        #region Fields

        private string _SenderID;
        private string _DeviceIDs;
        private string _Message;
        private bool _DryRun;
        private string _CollapseKey;
        private string _TimeToLife;
        private string _ApiKeysToTest;
        private string _DeviceTokensToTest;
        private GcmModel Model;

        #endregion

        #region Properties

        public GcmModel GetModel
        {
            get { return Model; }
            set { SetProperty(ref Model, value); }
        }
        public string SenderID
        {
            get { return _SenderID; }
            set { SetProperty(ref _SenderID, value); }
        }

        public string ApiKeysToTest
        {
            get { return _ApiKeysToTest; }
            set { SetProperty(ref _ApiKeysToTest, value); }
        }

        public string DeviceTokensToTest
        {
            get { return _DeviceTokensToTest; }
            set { SetProperty(ref _DeviceTokensToTest, value); }
        }

 


        #endregion

        #region Commands

        public Command SendCommand {
            get { return new Command(SendMessage);}
        }

        public Command ShowOptionsWindowCommand
        {
            get { return new Command(ShowOptionsWindow); }
        }

        public Command ShowValidityCheckWindowCommand
        {
            get { return new Command(ShowValidityCheckWindow); }
        }

        public Command CheckValidityCommand
        {
            get { return new Command(CheckKeyValidity); }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Sends the message.
        /// </summary>
        private void SendMessage()
        {

        }

        /// <summary>
        /// Shows the options window.
        /// </summary>
        private void ShowOptionsWindow()
        {
            OptionsWindow window = new OptionsWindow() { Owner = Application.Current.MainWindow, WindowStartupLocation = WindowStartupLocation.CenterOwner };
            window.ShowDialog();
        }

        private void ShowValidityCheckWindow()
        {
            ValCheckWindow window = new ValCheckWindow(this) { Owner = Application.Current.MainWindow, WindowStartupLocation = WindowStartupLocation.CenterOwner };
            window.Show();
        }

        private void CheckKeyValidity()
        {
            //Thread t = new Thread(() => this.Model.CheckKeyValidity(ApiKeysToTest));
            //Console.WriteLine("Thread Started");
            //t.Start();
            this.Model.CheckKeyValidity(ApiKeysToTest, DeviceTokensToTest);
        }
#endregion

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

    }
}
