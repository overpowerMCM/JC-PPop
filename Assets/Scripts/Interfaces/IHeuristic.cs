using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinding;

namespace PPop.Interfaces
{
    public interface IHeuristic
    {
        float GetHeuristic(MapTile from, MapTile to, bool areNeighbours  = false);
    }
}
