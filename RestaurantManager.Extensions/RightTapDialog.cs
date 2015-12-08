using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.Extensions
{
    public class RightTapDialog : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject
        {
            get; private set;
        }

        public void Attach(DependencyObject associatedObject)
        {
            if ((AssociatedObject = associatedObject as Page) != null)
            {
                (AssociatedObject as Page).RightTapped += RightTapDialog_RightTapped;
            }
        }

        private async void RightTapDialog_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            await new MessageDialog("Thank you very much for trying out the RestaurantManager!", "Thanks").ShowAsync();
        }

        public void Detach()
        {
            if (AssociatedObject as Page != null)
                (AssociatedObject as Page).RightTapped -= RightTapDialog_RightTapped;
        }
    }
}
