using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PPop.Heuristics
{
    public class Euclidian : PPop.Interfaces.IHeuristic
    {
        public float GetHeuristic(MapTile from, MapTile to)
        {
            return Vector3.Distance(from.Position, to.Position);
        }
    }
}
