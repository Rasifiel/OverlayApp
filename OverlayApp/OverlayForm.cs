using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace OverlayApp {
    public partial class OverlayForm : System.Windows.Forms.Form {
        // Directx graphics device
        public GraphicsDevice dev = null;
        BasicEffect effect = null;

        System.Windows.Forms.Screen screen;
        OverlayData overlay_data;
        public Thread refresher;
        float scaleX;
        float scaleY;

        public OverlayForm(System.Windows.Forms.Screen screen, OverlayData overlay_data) {
            this.screen = screen;
            this.overlay_data = overlay_data;
            InitializeComponent();

            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders
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
            scaleX = 2.0f / dev.Viewport.Width;
            scaleY = 2.0f / dev.Viewport.Height;
            // Init basic effect
            effect = new BasicEffect(dev);

            // Extend aero glass style on form init
            OnResize(null);
            refresher = new Thread(new ThreadStart(this.RedrawCycle));
            refresher.Start();
        }

        private void RedrawCycle() {
            while (true) {
                Invalidate();
                Thread.Sleep(50);
            }
        }

        protected override void OnResize(EventArgs e) {
            int[] margins = new int[] { 0, 0, Width, Height };

            // Extend aero glass style to whole form
            DwmExtendFrameIntoClientArea(this.Handle, ref margins);
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e) {
            // do nothing here to stop window normal background painting
        }

        const int CIRCLE_TRIANGLES = 50;
        const int FEATHER_LEVELS = 50;
        // Wheel vertexes
        VertexPositionColor[] v = new VertexPositionColor[CIRCLE_TRIANGLES * FEATHER_LEVELS * 3];

        private Vector3 CirclePoint(float x, float y, float r, int i, int max) {
            float rX = r * scaleX;
            float rY = r * scaleY;
            return new Vector3(x + rX * (float)Math.Sin((i) * (Math.PI * 2f / max)), y + rY * (float)Math.Cos((i) * (Math.PI * 2f / max)), 0);
        }

        struct Circle {
            public float x, y, r, tr;
            public Circle(float x, float y, float r, float tr) {
                this.x = x;
                this.y = y;
                this.r = r;
                this.tr = tr;
            }
        }

        private int CreateFeatheredCircle(float r, float f, Color c, float tr, float max_p) {
            List<Circle> spots_draw = new List<Circle>();
            foreach (var spotlight in overlay_data.Spotlights) {
                if (screen.Bounds.Contains(spotlight.X, spotlight.Y)) {
                    float x = (spotlight.X - screen.Bounds.Left) * scaleX - 1.0f;
                    float y = 1.0f - (spotlight.Y - screen.Bounds.Top) * scaleY;
                    for (int i = 0; i < FEATHER_LEVELS; i++) {
                        float middle_tr = tr * max_p * (max_p - spotlight.Progress);
                        if (middle_tr < 0) middle_tr = 0.0f;
                        float high = tr * max_p;
                        float t = i * 1.0f / (FEATHER_LEVELS - 1);
                        float level;
                        if (t < 0.5f) {
                            level = 2.0f * t * t;
                        } else {
                            t -= 0.5f;
                            level = 2.0f * t * (1.0f - t) + 0.5f;
                        }
                        float cur_tr = tr * max_p + (middle_tr - tr * max_p) * level;
                        float cur_r = r + f - f * i / (FEATHER_LEVELS - 1);
                        spots_draw.Add(new Circle(x, y, cur_r, cur_tr));
                    }
                }
            }
            if (spots_draw.Count == 0) return 0;
            spots_draw.Sort((x, y) => -x.tr.CompareTo(y.tr));
            int length = v.Length;
            while (length < spots_draw.Count * CIRCLE_TRIANGLES * 3) {
                length *= 2;
            }
            if (length != v.Length) {
                v = new VertexPositionColor[length];
            }
            for (int i = 0; i < spots_draw.Count * CIRCLE_TRIANGLES * 3; i++) {
                int cur_f = i / (CIRCLE_TRIANGLES * 3);
                v[i].Color = c;
                v[i].Color.A = (byte)(255 * spots_draw[cur_f].tr);
            }
            for (int i = 0; i < spots_draw.Count; i++) {
                Circle circle = spots_draw[i];
                for (int j = 0; j < CIRCLE_TRIANGLES; j++) {
                    int indx = (i * CIRCLE_TRIANGLES + j) * 3;
                    v[indx].Position = new Vector3(circle.x, circle.y, 0);
                    v[indx + 1].Position = CirclePoint(circle.x, circle.y, circle.r, j, CIRCLE_TRIANGLES);
                    v[indx + 2].Position = CirclePoint(circle.x, circle.y, circle.r, j + 1, CIRCLE_TRIANGLES);
                }
            }
            return spots_draw.Count;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
            if (dev.GraphicsDeviceStatus == GraphicsDeviceStatus.Lost) {
                dev.Reset();
            } else {
                float transparency = (100 - overlay_data.Settings.Transparency) / 100f;
                var overlay_color = overlay_data.Settings.Overlay_color;
                Color fill_color = new Color(0, 0, 0, transparency * overlay_data.MaxProgress);
                fill_color.R = overlay_color.R;
                fill_color.G = overlay_color.G;
                fill_color.B = overlay_color.B;
                if (overlay_data.MaxProgress > 0.0f) {
                    dev.Clear(fill_color);
                } else {
                    dev.Clear(new Color(0, 0, 0, 0));
                }
                effect.VertexColorEnabled = true;
                foreach (EffectPass pass in effect.CurrentTechnique.Passes) pass.Apply();

                if (overlay_data.MaxProgress > 0.0f) {
                    int circles = CreateFeatheredCircle(overlay_data.Settings.Radius, overlay_data.Settings.Feathering_radius, fill_color, transparency, overlay_data.MaxProgress);
                    if (circles > 0) {
                        dev.DrawUserPrimitives(PrimitiveType.TriangleList, v, 0, circles * CIRCLE_TRIANGLES, VertexPositionColor.VertexDeclaration);
                    }
                }
                dev.Present();
            }
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        private void Form1_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
            refresher.Abort();
        }
    }
}
