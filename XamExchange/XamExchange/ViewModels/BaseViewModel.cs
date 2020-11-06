namespace XamExchange.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Xamarin.Forms;
    using XamExchange.Services;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public CurrencyDataStore DataStore => DependencyService.Get<CurrencyDataStore>();

        bool isBusy = false;
        /// <summary>
        /// Definisce se l'applicazione è intenta nell'eseguire un compito oppure no.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        /// <summary>
        /// Definisce il titolo della finestra.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        /// <summary>
        /// Metodo da invocare quando si vuole aggiornare una proprietà legata al ViewModel.
        /// Permette di far rilevare alla view l'aggiornamento della proprietà ed aggiornarsi
        /// automaticamente senza ulteriori interventi, grazie al Data Binding.
        /// </summary>
        /// <typeparam name="T">Tipo della proprietà da aggiornare.</typeparam>
        /// <param name="backingStore">Store della proprietà.</param>
        /// <param name="value">Valore da aggiornare.</param>
        /// <param name="propertyName">Nome della proprietà (rilevabile).</param>
        /// <param name="onChanged">Azione da eseguire al termine.</param>
        /// <returns>True se l'aggiornamento è stato eseguito, false altrimenti.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
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
