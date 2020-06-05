using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace WorldSim
{
    class Town
    {
        public enum TownType
        {
            Village = 0,
            Town = 1,
            City = 2
        }

        public TownType Type;
        public uint Population;
        public Vector2 Position;

        public Town()
        {
            
        }

        public float DistanceTo(Vector2 target)
        {
            return Vector2.Distance(Position, target);
        }

        public string ToString()
        {
            return "Type=" + Type + " Pos=" + Position + " Pop=" + Population;
        }
    }
}
