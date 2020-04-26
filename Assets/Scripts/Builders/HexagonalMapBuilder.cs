using PPop.Input;
using PPop.Interfaces;
using PPop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PPop.Builders
{
    public class HexagonalMapBuilder : IBuilder
    {
        TileModel[] _tiledata;
        MapModel _mapdata;

        void GetData(IDataProvider dataProvider)
        {
            _tiledata = dataProvider.GetTiles();

            _mapdata = dataProvider.GetMap();
        }

        public List<MapTile> Create(IDataProvider dataProvider, Transform parent = null)
        {
            List<MapTile> mapTiles = new List<MapTile>();

            GetData(dataProvider);

            UnityEngine.Object prefab = UnityEngine.Resources.Load("Models/Hexagon_Model") ;
            Vector3 _topLeftCorner = CalculateTopLeftCorner();

            IHeuristic h = new HexEuclidian();
            for (int y = 0; y < _mapdata.MapHeight; y++)
            {
                for (int x = 0; x < _mapdata.MapWidth; x++)
                {
                    GameObject instance = (GameObject)UnityEngine.Object.Instantiate(prefab);
                    MapTile mapTile = instance.AddComponent<MapTile>();
                    if( null != parent)
                        mapTile.transform.SetParent(parent, false);
                    mapTile.X = x;
                    mapTile.Y = y;

                    mapTile.Heuristic = h;

                    // set positions
                    mapTile.Position = CalculatePosition(_topLeftCorner,mapTile);

                    mapTiles.Add(mapTile);
                }
            }

            foreach( MapTile mapTile in mapTiles )
            { 
                int index = mapTile.Y * _mapdata.MapHeight + mapTile.X;
                int id = _mapdata.tiles[index];
                var tile = _tiledata.SingleOrDefault(t => t.id == id);

                mapTile.Setup( Resources.Load<Texture>(string.Format("Textures/{0}", tile.Name.ToLower())), tile.walkable?tile.weight:(_mapdata.MapWidth*_mapdata.MapHeight*10), tile.walkable, GetNeighbours( mapTiles, mapTile ));

                mapTile.name = string.Format("Hex_{0}_{1}_{2}", mapTile.X, mapTile.Y, tile.Name);

            }

            return mapTiles;
        }

        private Vector3 CalculateTopLeftCorner()
        {
            float _x = 0;
            if ((_mapdata.MapHeight / 2) % 2 != 0)
                _x = _mapdata.TileWidth * .5f;

            return new Vector3 ( 
                -_mapdata.TileWidth * (_mapdata.MapWidth/2) - _x,
                0f, 
                _mapdata.TileHeight * (_mapdata.MapHeight/2) * 0.75f );

        }

        private Vector3 CalculatePosition(Vector3 _topLeftCorner, MapTile mapTile)
        {
            float _x = 0;
            if (mapTile.Y % 2 != 0)
                _x = _mapdata.TileWidth * .5f;

            return new Vector3(
                _topLeftCorner.x + mapTile.X * _mapdata.TileWidth + _x,
                0f,
                _topLeftCorner.z - mapTile.Y * _mapdata.TileHeight * 0.75f );

        }

        public List<MapTile> GetNeighbours(List<MapTile> tiles, MapTile tile)
        {
            List<MapTile> neighbours = new List<MapTile>();

            int xp = tile.X + 1;
            int xn = tile.X - 1;
            int yp = tile.Y + 1;
            int yn = tile.Y - 1;

            // common
            if (xn >= 0)//
            {
                neighbours.Add(tiles[tile.Y * _mapdata.MapHeight + xn]);
            }
            if (xp < _mapdata.MapWidth)//
            {
                neighbours.Add(tiles[tile.Y * _mapdata.MapHeight + xp]);
            }
            if (yn >= 0)//
            {
                neighbours.Add(tiles[yn * _mapdata.MapHeight + tile.X]);
            }
            if (yp < _mapdata.MapHeight)//
            {
                neighbours.Add(tiles[yp * _mapdata.MapHeight + tile.X]);
            }

            // even rows
            if (tile.Y % 2 == 0)
            {
                if (xn >= 0 && yp < _mapdata.MapHeight)
                {
                    neighbours.Add(tiles[yp * _mapdata.MapHeight + xn]);
                }
                if (xn >= 0 && yn >= 0)
                {
                    neighbours.Add(tiles[yn * _mapdata.MapHeight + xn]);
                }
            }
            else // odd rows
            {
                if (xp < _mapdata.MapWidth && yn >= 0)
                {
                    neighbours.Add(tiles[yn * _mapdata.MapHeight + xp]);
                }
                if (xp < _mapdata.MapWidth && yp < _mapdata.MapHeight)
                {
                    neighbours.Add(tiles[yp * _mapdata.MapHeight + xp]);
                }
            }
           
            return neighbours;
        }


    }
}
