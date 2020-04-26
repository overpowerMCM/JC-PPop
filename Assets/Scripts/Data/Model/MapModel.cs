using PPop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPop.Model
{
    [Serializable]
    public class MapModel
    {
        public EMapType mapType;
        public int MapWidth;
        public int MapHeight;

        public float TileHeight;
        public float TileWidth;

        public int[] tiles;
    }
}
