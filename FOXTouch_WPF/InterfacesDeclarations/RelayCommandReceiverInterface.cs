using GenericComponentsMVVM;
using System.Collections.Generic;

namespace FOXTouch_WPF.InterfacesDeclarations
{
    interface IRelayCommandReceiver
    {
        void SetRelayCommands(Dictionary<string, RelayCommand> relayCommands);
    }
}
