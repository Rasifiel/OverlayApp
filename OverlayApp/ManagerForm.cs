using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverlayApp
{
    public partial class ManagerForm : Form
    {
        KeyboardHook toggle_hook = new KeyboardHook();
        List<Form1> overlay_forms = new List<Form1>();
        bool overlay_enabled = false;
        public ManagerForm()
        {
            toggle_hook.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            toggle_hook.RegisterHotKey(OverlayApp.ModifierKeys.Control | OverlayApp.ModifierKeys.Alt,
                Keys.F12);
            InitializeComponent();
            TopMost = true;        // make the form always on top                     
            Visible = true;        // Important! if this isn't set, then the form is not shown at all
            var screens = System.Windows.Forms.Screen.AllScreens;
            foreach (var screen in screens)
            {
                Form1 form = new Form1(screen);
                form.Name = screen.DeviceName;
                form.Show();
                overlay_forms.Add(form);
            }
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            overlay_enabled ^= true;
            foreach (var form in overlay_forms)
            {
                form.ChangeState(overlay_enabled);
            }
        }

        private void hotkeyControl1_Enter(object sender, EventArgs e)
        {
        }
    }

    public sealed class KeyboardHook : IDisposable
    {
        // Registers a hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        // Unregisters the hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public Window()
            {
                // create the handle for the window.
                this.CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                // check if we got a hot key pressed.
                if (m.Msg == WM_HOTKEY)
                {
                    // get the keys.
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            #region IDisposable Members

            public void Dispose()
            {
                this.DestroyHandle();
            }

            #endregion
        }

        private Window _window = new Window();
        private int _currentId;
        private Dictionary<Tuple<ModifierKeys, Keys>, int> _registred = new Dictionary<Tuple<ModifierKeys, Keys>, int>();

        public KeyboardHook()
        {
            // register the event of the inner native window.
            _window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
            {
                KeyPressed?.Invoke(this, args);
            };
        }

        /// <summary>
        /// Registers a hot key in the system.
        /// </summary>
        /// <param name="modifier">The modifiers that are associated with the hot key.</param>
        /// <param name="key">The key itself that is associated with the hot key.</param>
        public void RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            if (!_registred.ContainsKey(Tuple.Create(modifier, key)))
            {
                // increment the counter.
                _currentId = _currentId + 1;

                // register the hot key.
                if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                    throw new InvalidOperationException("Couldn’t register the hot key.");
                _registred.Add(Tuple.Create(modifier, key), _currentId);
            }
        }

        public void UnregisterHotKey(ModifierKeys modifier, Keys key)
        {
            var tuple = Tuple.Create(modifier, key);
            if (_registred.ContainsKey(tuple))
            {
                if (!UnregisterHotKey(_window.Handle, _registred[tuple]))
                {
                    throw new InvalidOperationException("Couldn’t register the hot key.");
                }
                
            }
        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        public void Dispose()
        {
            // unregister all the registered hot keys.
            for (int i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }

            // dispose the inner native window.
            _window.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        private ModifierKeys _modifier;
        private Keys _key;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }

        public Keys Key
        {
            get { return _key; }
        }
    }

    /// <summary>
    /// The enumeration of possible modifiers.
    /// </summary>
    [Flags]
    public enum ModifierKeys : uint
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}
