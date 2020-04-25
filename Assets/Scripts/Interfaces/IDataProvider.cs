using PPop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPop.Interfaces
{
    public interface IDataProvider
    {
        TileModel[] GetTiles();
        MapModel GetMap();
    }
}
