using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSentry
{
    public class Sentry
    {
        public ushort Id { get; set; }
        public byte Width { get; set; }
        public byte Height { get; set; }

        public Sentry() { }

        public Sentry(ushort id, byte width, byte height)
        {
            Id = id;
            Width = width;
            Height = height;
        }
    }
}
