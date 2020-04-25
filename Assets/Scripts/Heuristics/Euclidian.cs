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
        public float GetHeuristic(MapTile from, MapTile to, bool areNeighbour = false)
        {
            if (areNeighbour)
                return to.Weight;
            return (Vector3.Distance(from.Position, to.Position) / 5f) * ((from.Weight+to.Weight) * 0.5f);
        }
    }
}
