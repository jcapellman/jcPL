using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using jcPL.PCL.Implementations;
using jcPL.UWP.Lib;

namespace jcPL.UnitTests.UWP.ViewModels {
    public class MainModel : INotifyPropertyChanged {
        private string _cacheString;

        public string CacheString {
            get { return _cacheString; }
            set { _cacheString = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            var cacheHandler = new JSONPL(new UWPPS());

            var item = await cacheHandler.GetAsync<string>("Test");

            if (!item.HasValue) {
                await cacheHandler.PutAsync("Test", DateTime.Now.ToString());

                CacheString = DateTime.Now.ToString();
            } else {
                CacheString = item.Value;
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}