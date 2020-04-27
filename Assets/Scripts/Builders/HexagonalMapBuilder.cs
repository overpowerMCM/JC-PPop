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

                    int index = y * _mapdata.MapHeight + x;
                    int id = _mapdata.tiles[index];
                    var tile = _tiledata.SingleOrDefault(t => t.id == id);

                    mapTile.Setup( Resources.Load<Texture>(string.Format("Textures/{0}", tile.Name.ToLower())), tile.weight, tile.walkable, string.Format("Hex_{0}_{1}_{2}", mapTile.X, mapTile.Y, tile.Name));
                    
                    mapTiles.Add(mapTile);
                }
            }

            foreach( MapTile mapTile in mapTiles )
            { 
                mapTile.Neighbours = mapTile.Walkable ? GetNeighbours(mapTiles, mapTile) : null;
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

        private void ValidateAndAddNeighbourg(int x, int y, List<MapTile> tiles, List<MapTile> neighbours )
        {
            MapTile _neighbourdCandidate = tiles[y * _mapdata.MapHeight + x];
            if (_neighbourdCandidate.Walkable)
                neighbours.Add(_neighbourdCandidate);
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
                ValidateAndAddNeighbourg(xn, tile.Y, tiles, neighbours);
            }
            if (xp < _mapdata.MapWidth)//
            {
                ValidateAndAddNeighbourg(xp, tile.Y, tiles, neighbours);
            }
            if (yn >= 0)//
            {
                ValidateAndAddNeighbourg(tile.X, yn, tiles, neighbours);
            }
            if (yp < _mapdata.MapHeight)//
            {
                ValidateAndAddNeighbourg(tile.X, yp, tiles, neighbours);
            }

            // even rows
            if (tile.Y % 2 == 0)
            {
                if (xn >= 0 && yp < _mapdata.MapHeight)
                {
                    ValidateAndAddNeighbourg(xn, yp, tiles, neighbours);
                }
                if (xn >= 0 && yn >= 0)
                {
                    ValidateAndAddNeighbourg(xn, yn, tiles, neighbours);
                }
            }
            else // odd rows
            {
                if (xp < _mapdata.MapWidth && yn >= 0)
                {
                    ValidateAndAddNeighbourg(xp, yn, tiles, neighbours);
                }
                if (xp < _mapdata.MapWidth && yp < _mapdata.MapHeight)
                {
                    //neighbours.Add(tiles[yp * _mapdata.MapHeight + xp]);
                    ValidateAndAddNeighbourg(xp, yp, tiles, neighbours);
                }
            }
            /*
            neighbours.Sort(
                (a, b) => 
                {
                    if(a.Weight == b.Weight) return 0;
                    if (a.Weight > b.Weight) return -1;
                    else return 1;

                } );
            */
            return neighbours;
        }


    }
}
