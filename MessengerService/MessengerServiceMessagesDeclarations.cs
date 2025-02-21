using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerService
{
    public static class MessengerServiceMessagesDeclarations
    {
        public class SliderValueChangedMessage
        {
            public double NewValue { get; }
            public object From { get; }

            public SliderValueChangedMessage(object from, double newValue)
            {
                NewValue = newValue;
                From = from;
            }
        }

        public class WindowClosedMessage
        {
            public object From { get; }

            public WindowClosedMessage(object from)
            {
                From = from;
            }
        }
    }
}
