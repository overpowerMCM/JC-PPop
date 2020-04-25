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
                width = 4,
                height = 4,
                tiles = new int[] 
                {
                    1,1,1,2,
                    1,3,3,4,
                    2,3,5,4,
                    2,3,5,2
                }
            };
        }

        public TileModel[] GetTiles()
        {
            return new TileModel[]
            {
                new TileModel(){ id = 1, Name = "Grass", weight = 1},
                new TileModel(){ id = 2, Name = "Forest", weight = 3},
                new TileModel(){ id = 3, Name = "Desert", weight = 5},
                new TileModel(){ id = 4, Name = "Mountain", weight = 10},
                new TileModel(){ id = 5, Name = "Water", weight = 256*256},
            };
        }
    }
}
