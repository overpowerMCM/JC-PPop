using PPop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPop.Model
{
    [Serializable]
    public struct MapModel
    {
        public EMapType mapType;
        public int width;
        public int height;
        public int[] tiles;
    }
}
