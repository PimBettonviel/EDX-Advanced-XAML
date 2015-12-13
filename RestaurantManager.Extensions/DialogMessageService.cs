using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace RestaurantManager.Extensions
{
    public class DialogMessageService
    {
        protected string Message;
        protected string Title;

        public DialogMessageService( string message, string title)
        {
            Message = message ?? string.Empty;
            Title = title ?? string.Empty;
        }

        public async Task ShowAsync()
        {
            await new MessageDialog(Message, Title).ShowAsync();
        }
    }
}
