using System.Windows.Input;

namespace GenericComponentsMVVM
{
    public class ButtonViewModel
    {
        public ICommand Command { get; set; }
        public string ImageSource { get; set; }
    }
}
