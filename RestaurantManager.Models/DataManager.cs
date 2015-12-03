using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantManager.Models
{
    public abstract class DataManager : INotifyPropertyChanged
    {
        protected RestaurantContext Repository { get; private set; }

        public DataManager()
        {
            LoadData();
        }


        private async void LoadData()
        {
            this.Repository = new RestaurantContext();
            await this.Repository.InitializeContextAsync();
            OnDataLoaded();
        }

        protected abstract void OnDataLoaded();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if( null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
