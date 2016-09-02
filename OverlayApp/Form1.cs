using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Microsoft.Xna.Framework.Input.ButtonState;

namespace OverlayApp
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        // Directx graphics device
        GraphicsDevice dev = null;
        BasicEffect effect = null;

        // Wheel vertexes
        VertexPositionColor[] v = new VertexPositionColor[99];

        float clickX = 0.0f;
        float clickY = 0.0f;
        int ttl = 0;
        MouseState _currentMouseState;
        MouseState _previousMouseState;
        System.Windows.Forms.Screen screen;
        bool state = false;

        public Form1(System.Windows.Forms.Screen screen)
        {
            this.screen = screen;
            InitializeComponent();

            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders
            System.Console.Out.WriteLine("{0} {1} {2} {3}", screen.Bounds.Left, screen.Bounds.Top, screen.Bounds.Width, screen.Bounds.Height);
            SetBounds(screen.Bounds.Left, screen.Bounds.Top, screen.Bounds.Width, screen.Bounds.Height);
            TopMost = true;        // make the form always on top                     
            Visible = true;        // Important! if this isn't set, then the form is not shown at all

            // Set the form click-through
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
            // Create device presentation parameters

            PresentationParameters p = new PresentationParameters();
            p.IsFullScreen = false;
            p.DeviceWindowHandle = this.Handle;
            p.BackBufferFormat = SurfaceFormat.Vector4;
            p.PresentationInterval = PresentInterval.One;

            // Create XNA graphics device
            dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, p);

            // Init basic effect
            effect = new BasicEffect(dev);

            _currentMouseState = Mouse.GetState();
            _previousMouseState = _currentMouseState;
            // Extend aero glass style on form init
            OnResize(null);
        }

        public void ChangeState(bool state)
        {
            this.state = state;
        }

        protected override void OnResize(EventArgs e)
        {
            int[] margins = new int[] { 0, 0, Width, Height };

            // Extend aero glass style to whole form
            DwmExtendFrameIntoClientArea(this.Handle, ref margins);
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            // do nothing here to stop window normal background painting
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            float scaleX = 2.0f / (float)dev.Viewport.Width;
            float scaleY = 2.0f / (float)dev.Viewport.Height;
            float rX = 100f * scaleX;
            float rY = 100f * scaleY;
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
            bool mouse_over = screen.Bounds.Contains(_currentMouseState.X, _currentMouseState.Y);
            if (state)
            {
                if (ttl < 100) ttl++;
            } else
            {
                if (ttl > 0) ttl--;
            }
            clickX = (_currentMouseState.X-screen.Bounds.Left)*scaleX-1.0f;
            clickY = 1.0f-(_currentMouseState.Y-screen.Bounds.Top)*scaleY;
            float ttl_k = ttl / 100.0f;
            // Clear device with fully transparent black
            dev.Clear(new Microsoft.Xna.Framework.Color(0, 0, 0, ttl_k/2.0f));

            if (ttl > 0 && mouse_over)
            {
                // Make the wheel vertexes and colors for vertexes
                for (int i = 0; i < v.Length; i++)
                {
                    if (i % 3 == 1)
                        v[i].Position = new Microsoft.Xna.Framework.Vector3(clickX + rX * ttl_k * (float)Math.Sin((i) * (Math.PI * 2f / (float)v.Length)), clickY + rY * ttl_k * (float)Math.Cos((i) * (Math.PI * 2f / (float)v.Length)), 0);
                    else if (i % 3 == 2)
                        v[i].Position = new Microsoft.Xna.Framework.Vector3(clickX + rX * ttl_k * (float)Math.Sin((i + 2) * (Math.PI * 2f / (float)v.Length)), clickY + rY * ttl_k * (float)Math.Cos((i + 2) * (Math.PI * 2f / (float)v.Length)), 0);
                    else
                        v[i].Position = new Microsoft.Xna.Framework.Vector3(clickX, clickY, 0);
                    v[i].Color = new Microsoft.Xna.Framework.Color(0, 0, 0, 0.0f);
                }   

                // Enable position colored vertex rendering
                effect.VertexColorEnabled = true;
                foreach (EffectPass pass in effect.CurrentTechnique.Passes) pass.Apply();

                // Draw the primitives (the wheel)
                dev.DrawUserPrimitives(PrimitiveType.TriangleList, v, 0, v.Length / 3, VertexPositionColor.VertexDeclaration);
            }

            // Present the device contents into form
            dev.Present();

            // Redraw immediatily
            Invalidate();
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        private void Form1_Click(object sender, EventArgs e)
        {
            var mouseState = Mouse.GetState();
            clickX = mouseState.X;
            clickY = mouseState.Y;
            ttl = 100;
        }
    }
}
