using System;
using static FOXTouchEnumerations.AxisState.AxisStateEnumerations;
using static FOXTouchEnumerations.ErrorCodes.AxisMovementFunctionErrorCodeEnumerations;
using static FOXTouchEnumerations.ErrorCodes.FunctionErrorCodeEnumerations;

namespace FOXTouch_WPF.InterfacesDeclarations
{
    interface AxisInterface
    {
        string Name { get; }
        EAxisState State { get; }
        int Position { get; }

        EFunctionErrorCode Initialize();
        EFunctionErrorCode Stop();
        EMoveFunctionErrorCode Home();
        EMoveFunctionErrorCode MoveToPosition();

        event EventHandler PositionChanged;
        event EventHandler StateChanged;
    }
}
