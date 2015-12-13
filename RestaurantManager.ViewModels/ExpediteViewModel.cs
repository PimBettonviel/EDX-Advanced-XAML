using RestaurantManager.Extensions;
using RestaurantManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

namespace RestaurantManager.ViewModels
{
    public class ExpediteViewModel : ViewModel
    {

        #region Properties
        public ICommand DeleteOrderCommand { get; set; }
        public ICommand DeleteAllOrdersCommand { get; set; }

        private ObservableCollection<Order> _orderItems = new ObservableCollection<Order>();
        public ObservableCollection<Order> OrderItems
        {
            get
            {
                return _orderItems;
            }
            set
            {
                _orderItems = value;
            }
        } 
        #endregion

        public ExpediteViewModel()
        {
            SetDelegateCommands();
        }

        private void SetDelegateCommands()
        {
            DeleteOrderCommand = new DelegateCommand<Order>(DeleteOrder);
            DeleteAllOrdersCommand = new DelegateCommand<object>(DeleteAllOrders);
        }

        protected override void OnDataLoaded()
        {
            foreach (var order in Repository.Orders)
            {
                OrderItems.Add(order);
            }
        }

        private void DeleteOrder(Order order)
        {
            if( OrderItems != null && OrderItems.Contains(order))
            {
                OrderItems.Remove(order);
                Repository.Orders.Remove(order);
            }
        }

        private void DeleteAllOrders(object obj)
        {
            if (OrderItems != null && OrderItems.Count > 0)
            {
                OrderItems.Clear();
                Repository.Orders.Clear();
            }
        }

    }
}
