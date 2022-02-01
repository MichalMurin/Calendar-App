using Calendar.Models;
using Calendar.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Calendar.ViewModels
{
    /// <summary>
    /// Trieda z ktorj dedia zobrazované karty, udržiava v sebe zoznam kontaktov
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Zoznam kontaktov
        /// </summary>
        public IDataStore<ContactModel> DataStore => DependencyService.Get<IDataStore<ContactModel>>();

        /// <summary>
        /// Informácia o zamestnanosti programu
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// Property informácie o zamestnanosti programu
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        /// <summary>
        /// Titulok karty
        /// </summary>
        string _title = string.Empty;

        /// <summary>
        /// property titulku
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Metóda na nastavenie hodnoty property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value">Hodnota ktorú priraďujeme</param>
        /// <param name="propertyName">Meno vlastnosti ktorú priraďujeme</param>
        /// <param name="onChanged"></param>
        /// <returns>True ak sa vlastnosť podarilo priradiť</returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
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

        /// <summary>
        /// Udalosť na zmenu vlastnosti
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Vyvolá sa udalosť pri zmene vlastnosti
        /// </summary>
        /// <param name="propertyName">Názov vlastnosti</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
