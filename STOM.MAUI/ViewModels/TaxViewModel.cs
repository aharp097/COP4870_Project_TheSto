using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STO.Library.Services;

namespace STOM.MAUI.ViewModels
{
    public class TaxViewModel : INotifyPropertyChanged
    {
        public string? CurrentTax
        {
            get
            {
                return ContactServerProxy.Current?.tax.ToString("C");

            }

        }

        public decimal taxu;
        public decimal Taxu
        {
            get => taxu;
            set
            {
                taxu = value;
                NotifyPropertyChanged();
            }
        }
        public void UpdateTax()
        {
            ContactServerProxy.Current?.SetTax(Taxu);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
