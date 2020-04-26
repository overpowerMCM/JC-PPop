using System.Collections;
using System.Collections.Generic;
using PPop;
using UnityEngine;

public class HexEuclidian : PPop.Interfaces.IHeuristic
{
    public float GetHeuristic(MapTile from, MapTile to, bool areNeighbour = false)
    {
        if (areNeighbour)
            return to.Weight;

        var dx = to.X - from.X;
        var dy = to.Y - from.Y;
        float dist = 0f;

        if (Mathf.Sign(dx).Equals(Mathf.Sign(dy)))
        {    // this is (1); see first paragraph
            dist = Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy));
        }
        else
        {
            dist = Mathf.Abs(dx) + Mathf.Abs(dy);
        }

        return dist * (from.Weight + to.Weight) * 0.5f;
    }
}
