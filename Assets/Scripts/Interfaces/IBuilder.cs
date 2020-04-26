using System.Collections.Generic;
using UnityEngine;

namespace PPop.Interfaces
{
    public interface IBuilder
    {
        List<MapTile> Create(IDataProvider dataProvider, Transform parent);
        List<MapTile> GetNeighbours(List<MapTile> tiles, MapTile tile);
    }
}
