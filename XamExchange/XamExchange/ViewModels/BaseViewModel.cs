﻿namespace XamExchange.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Xamarin.Forms;

    using XamExchange.Models;
    using XamExchange.Services;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Rate> DataStore => DependencyService.Get<IDataStore<Rate>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { _ = this.SetProperty(ref this.isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return this.title; }
            set { _ = this.SetProperty(ref this.title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
