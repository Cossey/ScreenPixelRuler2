using System;
using System.Windows.Forms;

namespace ScreenPixelRuler2
{
    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        private readonly ModifierKeys _modifier;
        private readonly Keys _key;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        public ModifierKeys Modifier => _modifier;

        public Keys Key => _key;
    }

}
