using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FOXTouch_WPF.ViewModels
{
    class ButtonViewModel
    {
        public ICommand Command { get; set; }
        public string ImageSource { get; set; }
    }
}
