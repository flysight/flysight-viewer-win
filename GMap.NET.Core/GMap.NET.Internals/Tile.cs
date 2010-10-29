﻿
namespace GMap.NET.Internals
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// represent tile
    /// </summary>
    internal class Tile
    {
        Point pos;
        int zoom;
        public readonly List<PureImage> Overlays = new List<PureImage>(1);

        public Tile(int zoom, Point pos)
        {
            this.Zoom = zoom;
            this.Pos = pos;
        }

        public void Clear()
        {
            lock (Overlays)
            {
                foreach (PureImage i in Overlays)
                {
                    i.Dispose();
                }

                Overlays.Clear();
            }
        }

        public int Zoom
        {
            get
            {
                return zoom;
            }
            private set
            {
                zoom = value;
            }
        }

        public Point Pos
        {
            get
            {
                return pos;
            }
            private set
            {
                pos = value;
            }
        }
    }
}
