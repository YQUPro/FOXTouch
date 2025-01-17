using static FOXTouchEnumerations.ErrorCodes.FunctionErrorCodeEnumerations;

namespace LightingBoards.Interfaces
{
    public interface ILightingBoardClass
    {
        EFunctionErrorCode Start();

        EFunctionErrorCode UpdatesLights(byte L0, byte L1, byte L2, byte L3, byte L4, byte L5, byte L6, byte L7);

        EFunctionErrorCode Stop();

    }
}
