using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace Anything.Messages
{
    internal class FocusMessage<TViewModel>
    {
        public FocusMessage(Key keyPressed)
        {
            this.KeyPressed = keyPressed;
        } 

        public Key KeyPressed { get; }
    }
}
