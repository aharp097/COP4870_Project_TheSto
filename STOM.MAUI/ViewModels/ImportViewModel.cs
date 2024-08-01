using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STO.Library.DTO;
using STO.Library.Services;

namespace STOM.MAUI.ViewModels
{
    public class ImportViewModel : INotifyPropertyChanged
    {
        public string path;
        public string Path
        {
            get => path;
            set
            {
                path = value;
            }
        }
        //public void ImportCSV()
        //{
        //    ContactServerProxy.Current?.SetTax(Taxu);
        //}
        private ProductDTO? ImportModel { get; set; }
        public async void ImportCSV()
        {//20min last vid
            var csv = (@Path);

            using (var reader = new StreamReader(Path))
            {
                while (reader.EndOfStream == false)
                {
                    var content = reader.ReadLine();
                    var cell = content.Split(',').ToList();
                    if (cell.Any(c => c.Length > 0))
                    {
                        ImportModel = new ProductDTO();
                        ImportModel.Name = cell[0];
                        ImportModel.Description = cell[1];
                        ImportModel.Price = decimal.Parse(cell[2]);
                        ImportModel.Stock = int.Parse(cell[3]);
                        ImportModel.Bogo = bool.Parse(cell[4]);
                        ImportModel.MarkedDown = bool.Parse(cell[5]);
                        ImportModel.MarkDownPercent = decimal.Parse(cell[6]);
                        await ContactServerProxy.Current.AddOrUpdate(ImportModel);
                    }

                }
            }
            NotifyPropertyChanged();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
