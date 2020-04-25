using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPop.Interfaces
{
    public interface IBuilder
    {
        List<MapTile> Create(IDataProvider dataProvider);
        List<MapTile> GetNeighbours(MapTile tile);
    }
}
