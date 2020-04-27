using PPop.Interfaces;
using PPop.Model;
using PPop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPop.Data
{
    public class DefaultDataProvider : IDataProvider
    {
        public MapModel GetMap()
        {
            return new MapModel()
            {
                mapType = Enums.EMapType.HEXAGONAL,
                MapWidth = 8,
                MapHeight = 8,
                TileHeight = 1,
                TileWidth = 1,
                tiles = new int[] 
                {
                    3,3,1,5,1,4,4,1,
                    3,3,3,2,1,2,4,2,
                    3,2,3,1,1,4,3,1,
                    3,2,5,2,4,2,3,3,
                    5,5,5,2,4,2,1,3,
                    2,2,3,1,5,5,1,5,
                    1,2,4,2,2,1,5,5,
                    4,4,4,2,1,2,1,5
                }
            };
        }

        public TileModel[] GetTiles()
        {
            return new TileModel[]
            {
                new TileModel(){ id = 1, Name = "Grass", weight = 1, walkable = true},
                new TileModel(){ id = 2, Name = "Forest", weight = 3, walkable = true},
                new TileModel(){ id = 3, Name = "Desert", weight = 5, walkable = true},
                new TileModel(){ id = 4, Name = "Mountain", weight = 10, walkable = true},
                new TileModel(){ id = 5, Name = "Water", weight = 0, walkable = false},
            };
        }
    }
}
