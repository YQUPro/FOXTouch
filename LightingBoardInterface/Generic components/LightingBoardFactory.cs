using LightingBoards.Interfaces;
using System;

namespace LightingBoards.Factory
{
    public static class LightingBoardFactory
    {
        public static ILightingBoardClass CreateLightingCard(string cardType)
        {
            //switch (cardType)
            //{
            //    case "FOXLB":
            //        return new FoxLightingBoardControllerModel();
            //    //case "CardB":
            //    //    return new LightingCardB();
            //    default:
            throw new ArgumentException("Invalid card type");
            //}
        }
    }
}
