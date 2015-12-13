using RestaurantManager.Extensions;
using RestaurantManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Linq;

namespace RestaurantManager.ViewModels
{
    public class OrderViewModel : ViewModel
    {

        #region Properties
        public ICommand SubmitOrderCommand { get; set; }

        public ICommand AddToOrderCommand { get; set; }


        private string _specialRequest;

        public string SpecialRequest
        {
            get { return _specialRequest; }
            set { _specialRequest = value; }
        }

        private ObservableCollection<MenuItem> _menuItems = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                if (_menuItems != value)
                {
                    _menuItems = value;
                }
            }
        }

        private ObservableCollection<MenuItem> _currentlySelectedMenuItems = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> CurrentlySelectedMenuItems
        {
            get
            {
                return _currentlySelectedMenuItems;
            }
            set
            {
                _currentlySelectedMenuItems = value;
            }
        }

        #endregion

        public OrderViewModel()
        {
            SetDelegateCommands();
        }

        protected override void OnDataLoaded()
        {
            foreach(var item in Repository.StandardMenuItems)
            {
                MenuItems.Add(item);
            }

        }

        private void SetDelegateCommands()
        {
            SubmitOrderCommand = new DelegateCommand<object>(SaveOrder);
            AddToOrderCommand = new DelegateCommand<MenuItem>(AddToOrder);
        }

        private void AddToOrder(MenuItem menuItem)
        {
            if (menuItem != null)
            {
                CurrentlySelectedMenuItems.Add(menuItem);
            }
        }

        private async void SaveOrder(object obj)
        {
            if (CurrentlySelectedMenuItems != null && CurrentlySelectedMenuItems.Count > 0)
            {
                var newOrderID = Repository.Orders
                                .Select( o => o.Id )
                                .DefaultIfEmpty(0)
                                .Max() + 1;
                base.Repository.Orders.Add(new Order()
                {
                    Id = newOrderID,
                    Items = new List<MenuItem>( CurrentlySelectedMenuItems),
                    SpecialRequests = SpecialRequest
                });

                //Clear properties
                SpecialRequest = string.Empty;
                CurrentlySelectedMenuItems.Clear();
                await new DialogMessageService("New order submitted.", "Submit").ShowAsync();
            }
            else
            {
                await new DialogMessageService("Please add at least 1 item to the order.", "Submit").ShowAsync();
            }
        }

    }
}
