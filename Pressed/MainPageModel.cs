using Pressed.Pressed;
using Xamarin.Forms;

namespace Pressed
{
    public class MainPageModel
    {
        private Command _tappedCommand;
        public Command OnTouchedCommand => _tappedCommand ?? (_tappedCommand = new Command(OnTappedCommand));

        private void OnTappedCommand(object obj)
        {
            var args = (TouchEventArgs) obj;
            
        }
    }
}