using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayApp {
    public class OverlayData {
        Settings settings;
        List<Spotight> spotlights = new List<Spotight>();
        long lastUpdate;
        float maxProgress = 0.0f;
        bool followMode = false;

        public float MaxProgress {
            get {
                return maxProgress;
            }

            set {
                maxProgress = value;
            }
        }

        public long LastUpdate {
            get {
                return lastUpdate;
            }

            set {
                lastUpdate = value;
            }
        }

        public List<Spotight> Spotlights {
            get {
                return spotlights;
            }
        }

        public bool FollowMode {
            get {
                return followMode;
            }

            set {
                followMode = value;
            }
        }

        public Settings Settings {
            get {
                return settings;
            }
        }

        public OverlayData(Settings settings) {
            this.settings = settings;
            lastUpdate = DateTime.Now.Ticks;
            maxProgress = 0.0f;
        }

        public void AddSpotlight(bool follow) {
            var spotlight = new Spotight();
            spotlight.Rising = true;
            spotlight.EventStart = DateTime.Now.Ticks;
            spotlight.X = Mouse.GetState().X;
            spotlight.Y = Mouse.GetState().Y;
            spotlight.Follow = follow;
            spotlights.Add(spotlight);
        }

        public void ToggleFollow() {
            Spotight spot = spotlights.Find(x => x.Follow);
            if (spot != null) {
                if (spot.Decline) {
                    spot.Rising = true;
                    spot.Decline = false;
                    followMode = true;
                } else {
                    spot.Rising = false;
                    spot.Decline = true;
                    RemoveAll();
                    followMode = false;
                }
            } else {
                AddSpotlight(true);
                followMode = true;
            }
        }

        public void RemoveAll() {
            foreach (var spot in spotlights) {
                spot.Decline = true;
                spot.Rising = false;
            }
        }

        public void Update() {
            var time = DateTime.Now.Ticks;
            LastUpdate = time;
            MaxProgress = 0.0f;
            foreach (var spotlight in spotlights) {
                var delta = (time - spotlight.EventStart)/10000;
                if (spotlight.Rising) {
                    spotlight.Progress = spotlight.Start_p + 1.0f * delta / settings.FadeInTime;
                    if (spotlight.Progress>1.0f) {
                        spotlight.Progress = 1.0f;
                        spotlight.Rising = false;
                    }
                }
                if (spotlight.Decline) {
                    spotlight.Progress = spotlight.Start_p  - 1.0f * delta / settings.FadeOutTime;
                    if (spotlight.Progress < 0.0f) {
                        spotlight.Progress = 0.0f;
                        spotlight.Decline = false;
                    }
                }
                if (spotlight.Follow) {
                    spotlight.X = Mouse.GetState().X;
                    spotlight.Y = Mouse.GetState().Y;
                }
                if (spotlight.Progress > MaxProgress) MaxProgress = spotlight.Progress;
            }
            spotlights.RemoveAll(x => (!x.Rising && x.Progress <=0.0f));
        }
    }

    public class Spotight {
        int x;
        int y;
        float progress;
        float start_p;
        bool follow;
        long eventStart;
        bool rising;
        bool decline;

        public int X {
            get {
                return x;
            }

            set {
                x = value;
            }
        }

        public int Y {
            get {
                return y;
            }

            set {
                y = value;
            }
        }

        public float Progress {
            get {
                return progress;
            }

            set {
                progress = value;
            }
        }

        public bool Follow {
            get {
                return follow;
            }

            set {
                follow = value;
            }
        }

        public long EventStart {
            get {
                return eventStart;
            }

            set {
                eventStart = value;
            }
        }

        public bool Rising {
            get {
                return rising;
            }

            set {
                start_p = progress;
                eventStart = DateTime.Now.Ticks;
                rising = value;
            }
        }

        public bool Decline {
            get {
                return decline;
            }

            set {
                start_p = progress;
                eventStart = DateTime.Now.Ticks;
                decline = value;
            }
        }

        public float Start_p {
            get {
                return start_p;
            }
        }
    }
}
