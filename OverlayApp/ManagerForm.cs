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
using System.Threading;
using System.Xml.Serialization;
using System.IO;

namespace OverlayApp {
    public partial class ManagerForm : Form {
        OverlayForm overlay_form;
        Settings settings = Settings.CreateOrLoadSettings();
        Screen[] screens;
        OverlayData overlay_data;
        Thread refresher;
        HotKeyComboController add_spot;
        HotKeyComboController toggle_follow;
        HotKeyComboController remove_spots;
        System.Timers.Timer timer;

        public ManagerForm() {
            timer = new System.Timers.Timer();
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            overlay_data = new OverlayData(settings);
            InitializeComponent();
            Visible = true;
            RefreshScreenList();

            add_spot = new HotKeyComboController(addSpotControl, settings.Add_spot);
            add_spot.AddEvent(new EventHandler<KeyPressedEventArgs>(AddSpotlight));
            toggle_follow = new HotKeyComboController(toggleFollowControl, settings.Toggle_follow);
            toggle_follow.AddEvent(new EventHandler<KeyPressedEventArgs>(AddFollow));
            remove_spots = new HotKeyComboController(clearSpotsControl, settings.Remove_spots);
            remove_spots.AddEvent(new EventHandler<KeyPressedEventArgs>(RemoveSpots));

            fadeInControl.Value = (decimal)settings.FadeInTime;
            fadeOutControl.Value = (decimal)settings.FadeOutTime;
            spotlightRadiusControl.Value = settings.Radius;
            featheringRadiusControl.Value = settings.Feathering_radius;
            autoHideDelayControl.Value = settings.Autohide_delay;

            overlay_form = new OverlayForm(screens[0], overlay_data);
            overlay_form.Show();

            refresher = new Thread(new ThreadStart(this.UpdateState));
            refresher.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            overlay_data.RemoveAll();
        }

        private void UpdateState() {
            while (true) {
                overlay_data.Update();
                Thread.Sleep(50);
            }
        }

        void AddSpotlight(object sender, KeyPressedEventArgs e) {
            overlay_data.AddSpotlight(false);
            if (!overlay_data.FollowMode && settings.Autohide_delay>0) {
                timer.Interval = settings.Autohide_delay;
                timer.Start();
            }
        }
        void AddFollow(object sender, KeyPressedEventArgs e) {
            overlay_data.ToggleFollow();
            timer.Stop();
        }
        void RemoveSpots(object sender, KeyPressedEventArgs e) {
            overlay_data.RemoveAll();
            timer.Stop();
        }

        private void button1_Click(object sender, EventArgs e) {
            RefreshScreenList();
        }

        private void RefreshScreenList() {
            screens = Screen.AllScreens;
            screenSelectComboBox.Items.Clear();
            for (int i=0; i<screens.Length; i++) {
                var screen = screens[i];
                screenSelectComboBox.Items.Add(string.Format("#{0} - {1}x{2}", i, screen.Bounds.Width, screen.Bounds.Height));
            }
            screenSelectComboBox.SelectedIndex = settings.Screen;
        }

        private void ManagerForm_FormClosed(object sender, FormClosedEventArgs e) {
            refresher.Abort();
            overlay_form?.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            if (screenSelectComboBox.SelectedIndex!=settings.Screen) {
                overlay_form.Close();
                overlay_form = new OverlayForm(screens[screenSelectComboBox.SelectedIndex], overlay_data);
                overlay_form.Show();
                settings.Screen = screenSelectComboBox.SelectedIndex;
            }
            var new_combo_add = new HotKeyCombo(addSpotControl.HotkeyModifiers, addSpotControl.Hotkey);
            if (new_combo_add!=settings.Add_spot) {
                add_spot.UpdateCombo(new_combo_add);
            }
            new_combo_add = new HotKeyCombo(toggleFollowControl.HotkeyModifiers, toggleFollowControl.Hotkey);
            if (new_combo_add != settings.Toggle_follow) {
                toggle_follow.UpdateCombo(new_combo_add);
            }
            new_combo_add = new HotKeyCombo(clearSpotsControl.HotkeyModifiers, clearSpotsControl.Hotkey);
            if (new_combo_add != settings.Remove_spots) {
                remove_spots.UpdateCombo(new_combo_add);
            }
            settings.FadeInTime = (int)fadeInControl.Value;
            settings.FadeOutTime = (int)fadeOutControl.Value;
            settings.Radius = (int)spotlightRadiusControl.Value;
            settings.Feathering_radius = (int)featheringRadiusControl.Value;
            settings.Transparency = transparencyControl.Value;
            settings.Autohide_delay = (int)autoHideDelayControl.Value;

            settings.SaveSettings();
        }

        private void hotkeyControl1_TextChanged(object sender, EventArgs e) {
            applyButton.Enabled = true;
            if (((HotkeyControl)sender).Hotkey == Keys.None) {
                applyButton.Enabled = false;
            }
            HashSet<Tuple<Keys, Keys>> combo_set = new HashSet<Tuple<Keys, Keys>>();
            foreach (var hotkey_control in Controls.OfType<HotkeyControl>()) {
                if (!combo_set.Add(Tuple.Create(hotkey_control.Hotkey, hotkey_control.HotkeyModifiers))) {
                    applyButton.Enabled = false;
                }
            }
        }

    }

    public class Settings  {
        private HotKeyCombo add_spot;
        private HotKeyCombo toggle_follow;
        private HotKeyCombo remove_spots;
        private int screen;
        private float fadeInTime;
        private float fadeOutTime;
        private int radius;
        private int feathering_radius;
        private int transparency;
        private Color overlay_color;
        private int autohide_delay;

        public HotKeyCombo Add_spot {
            get {
                return add_spot;
            }

            set {
                add_spot = value;
            }
        }

        public HotKeyCombo Toggle_follow {
            get {
                return toggle_follow;
            }

            set {
                toggle_follow = value;
            }
        }

        public HotKeyCombo Remove_spots {
            get {
                return remove_spots;
            }

            set {
                remove_spots = value;
            }
        }

        public int Screen {
            get {
                return screen;
            }

            set {
                screen = value;
            }
        }

        public float FadeInTime {
            get {
                return fadeInTime;
            }

            set {
                fadeInTime = value;
            }
        }

        public float FadeOutTime {
            get {
                return fadeOutTime;
            }

            set {
                fadeOutTime = value;
            }
        }

        public int Radius {
            get {
                return radius;
            }

            set {
                radius = value;
            }
        }

        public int Feathering_radius {
            get {
                return feathering_radius;
            }

            set {
                feathering_radius = value;
            }
        }

        public int Transparency {
            get {
                return transparency;
            }

            set {
                transparency = value;
            }
        }

        public Color Overlay_color {
            get {
                return overlay_color;
            }

            set {
                overlay_color = value;
            }
        }

        public int Autohide_delay {
            get {
                return autohide_delay;
            }

            set {
                autohide_delay = value;
            }
        }

        private Settings() {
            Add_spot = new HotKeyCombo(ModifierKeys.Control | ModifierKeys.Alt, Keys.F12);
            Toggle_follow = new HotKeyCombo(ModifierKeys.Control | ModifierKeys.Alt, Keys.F11);
            Remove_spots = new HotKeyCombo(ModifierKeys.Control | ModifierKeys.Alt, Keys.F10);
            Screen = 0;
            FadeInTime = 750;
            FadeOutTime = 500;
            Radius = 100;
            Feathering_radius = 30;
            Transparency = 50;
            Overlay_color = Color.Black;
            autohide_delay = 5000;
        }

        public static Settings CreateOrLoadSettings() {
            var serializer = new XmlSerializer(typeof(Settings));
            FileInfo fi = new FileInfo(Application.LocalUserAppDataPath
               + @"\overlay_app.config");
            if (fi.Exists) {
                var fstram = fi.OpenRead();
                try {
                    Settings config = (Settings)serializer.Deserialize(fstram);
                    return config;
                } finally {
                    fstram.Close();
                }
            } else {
                var settings = new Settings();
                return settings;
            }
        }

        public void SaveSettings() {
            var serializer = new XmlSerializer(typeof(Settings));
            FileInfo fi = new FileInfo(Application.LocalUserAppDataPath
               + @"\overlay_app.config");
            var fsteam = fi.OpenWrite();
            try {
                serializer.Serialize(fsteam, this);
            } finally {
                fsteam.Close();
            }
        }
    }

    public sealed class KeyboardHook : IDisposable {
        // Registers a hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        // Unregisters the hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class Window : NativeWindow, IDisposable {
            private static int WM_HOTKEY = 0x0312;

            public Window() {
                // create the handle for the window.
                this.CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m) {
                base.WndProc(ref m);

                // check if we got a hot key pressed.
                if (m.Msg == WM_HOTKEY) {
                    // get the keys.
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            #region IDisposable Members

            public void Dispose() {
                this.DestroyHandle();
            }

            #endregion
        }

        private Window _window = new Window();
        private int _currentId;
        private Dictionary<Tuple<ModifierKeys, Keys>, int> _registred = new Dictionary<Tuple<ModifierKeys, Keys>, int>();

        public KeyboardHook() {
            // register the event of the inner native window.
            _window.KeyPressed += delegate (object sender, KeyPressedEventArgs args) {
                KeyPressed?.Invoke(this, args);
            };
        }

        /// <summary>
        /// Registers a hot key in the system.
        /// </summary>
        /// <param name="modifier">The modifiers that are associated with the hot key.</param>
        /// <param name="key">The key itself that is associated with the hot key.</param>
        public void RegisterHotKey(ModifierKeys modifier, Keys key) {
            if (!_registred.ContainsKey(Tuple.Create(modifier, key))) {
                // increment the counter.
                _currentId = _currentId + 1;

                // register the hot key.
                if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                    throw new InvalidOperationException("Couldn’t register the hot key.");
                _registred.Add(Tuple.Create(modifier, key), _currentId);
            }
        }

        public void RegisterHotKey(HotKeyCombo combo) {
            RegisterHotKey(combo.Modifier, combo.Key);
        }

        public void UnregisterHotKey(ModifierKeys modifier, Keys key) {
            var tuple = Tuple.Create(modifier, key);
            if (_registred.ContainsKey(tuple)) {
                if (!UnregisterHotKey(_window.Handle, _registred[tuple])) {
                    throw new InvalidOperationException("Couldn’t register the hot key.");
                }
                _registred.Remove(tuple);
            }
        }

        public void UnregisterHotKey(HotKeyCombo combo) {
            UnregisterHotKey(combo.Modifier, combo.Key);
        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        public void Dispose() {
            // unregister all the registered hot keys.
            foreach (int id in _registred.Values) {
                UnregisterHotKey(_window.Handle, id);
            }

            // dispose the inner native window.
            _window.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs {
        private ModifierKeys _modifier;
        private Keys _key;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key) {
            _modifier = modifier;
            _key = key;
        }

        public ModifierKeys Modifier {
            get { return _modifier; }
        }

        public Keys Key {
            get { return _key; }
        }
    }

    public class HotKeyCombo : IComparable<HotKeyCombo> {
        ModifierKeys modifier;
        Keys key;

        public ModifierKeys Modifier {
            get {
                return modifier;
            }
            set {
                modifier = value;
            }
        }

        public Keys ModifierAsKey {
            get {
                return ModifierKeys2Keys(modifier);
            }
        }

        public Keys Key {
            get {
                return key;
            }
            set {
                key = value;
            }
        }

        public static ModifierKeys Keys2ModifierKeys(Keys keys) {
            ModifierKeys result = 0;
            if ((keys & Keys.Alt) != 0) result |= ModifierKeys.Alt;
            if ((keys & Keys.Control) != 0) result |= ModifierKeys.Control;
            if ((keys & Keys.Shift) != 0) result |= ModifierKeys.Shift;
            if ((keys & Keys.LWin) != 0) result |= ModifierKeys.Win;
            return result;
        }

        public static Keys ModifierKeys2Keys(ModifierKeys keys) {
            Keys result = 0;
            if ((keys & ModifierKeys.Alt) != 0) result |= Keys.Alt;
            if ((keys & ModifierKeys.Control) != 0) result |= Keys.Control;
            if ((keys & ModifierKeys.Shift) != 0) result |= Keys.Shift;
            if ((keys & ModifierKeys.Win) != 0) result |= Keys.LWin;
            return result;
        }

        public int CompareTo(HotKeyCombo other) {
            if (key != other.key) return key.CompareTo(other.key);
            return modifier.CompareTo(other.modifier);
        }

        private HotKeyCombo() {

        }

        public HotKeyCombo(ModifierKeys modifier, Keys key) {
            this.modifier = modifier;
            this.key = key;
        }

        public HotKeyCombo(Keys modifier, Keys key) {
            this.modifier = Keys2ModifierKeys(modifier);
            this.key = key;
        }
    }

    class HotKeyComboController {
        HotkeyControl control;
        HotKeyCombo combo;
        KeyboardHook hook;

        public HotKeyComboController(HotkeyControl control, HotKeyCombo combo) {
            this.control = control;
            this.combo = combo;
            control.Hotkey = combo.Key;
            control.HotkeyModifiers = combo.ModifierAsKey;
            control.Redraw(false);
            hook = new KeyboardHook();
            hook.RegisterHotKey(combo);
            control.Leave += new EventHandler(Control_Leave);
            control.Enter += new EventHandler(Control_Enter);
        }

        public void AddEvent(EventHandler<KeyPressedEventArgs> event_handler) {
            hook.KeyPressed += event_handler;
        }

        private void Control_Enter(object sender, EventArgs e) {
            hook.UnregisterHotKey(combo);
        }

        private void Control_Leave(object sender, EventArgs e) {
            hook.RegisterHotKey(combo);
        }

        public void UpdateCombo(HotKeyCombo combo) {
            hook.UnregisterHotKey(this.combo);
            this.combo = combo;
            hook.RegisterHotKey(combo);
        } 
    }

    /// <summary>
    /// The enumeration of possible modifiers.
    /// </summary>
    [Flags]
    public enum ModifierKeys : uint {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}
