using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouch_WPF.InterfacesDeclarations
{
    public interface ILateInitializableDataContextWindow
    {
        void InitializeDataContext(object dataContext);
    }
}
