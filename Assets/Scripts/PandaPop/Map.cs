using PathFinding;
using PPop.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PPop
{
    public class Map : MonoBehaviour
    {
        [SerializeField]
        GameObject _tileContainer;

        MapTile from, to;

        // Start is called before the first frame update
        void Start()
        {
            Builders.HexagonalMapBuilder builder = new Builders.HexagonalMapBuilder();
            List<MapTile> mapTiles =  builder.Create( new Data.DefaultDataProvider(), _tileContainer.transform );

            foreach (MapTile tile in mapTiles)
            {
                if (tile.Walkable)
                {
                    ClickableTile clickableTile = tile.gameObject.AddComponent<ClickableTile>();
                    clickableTile.OnClick.AddListener(OnTileClicked);
                }
            }

            /*
            MapTile from = mapTiles.Find(t => t.X == 7 && t.Y == 3);
            MapTile to = mapTiles.Find(t => t.X == 6 && t.Y == 7);

            var path = PathFinding.AStar.GetPath(from, to);

            Debug.Log("tiles: " + path.Count);

            for( int i = 0; i < path.Count; i++ )
            {
                ((MapTile)path[i]).SetTintColor((i == 0 || i == path.Count - 1) ? Color.green : Color.red);
            }*/
        }

        IList<IAStarNode> _currentPath;

        void OnTileClicked( MapTile source )
        {
            if (null == from)
            {
                from = source;
                from.SetTintColor(Color.yellow);
            }
            else if (null == _currentPath && !from.Equals(source))
            {               
                _currentPath = PathFinding.AStar.GetPath(from, source);
                float totalweight = 0f;
                for (int i = 0; i < _currentPath.Count; i++)
                {
                    MapTile tile = ((MapTile)_currentPath[i]);
                    totalweight += tile.Weight;
                    tile.SetTintColor((i == 0 || i == _currentPath.Count - 1) ? Color.green : Color.red);
                }
                Debug.Log(string.Format("Tiles: {0}, weight: {1}", _currentPath.Count, totalweight)); 
            }
            else
            {
                Reset();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (UnityEngine.Input.GetMouseButtonUp(1) && null != from )
            {
                Reset();
            }
        }

        private void Reset()
        {
            if (null != from)
            {
                from.SetTintColor(Color.white);
                from = null;
            }
            if (null != _currentPath)
            {
                foreach (var e in _currentPath )
                {
                    ((MapTile)e).SetTintColor(Color.white);
                }
                _currentPath = null;
            }
        }
    }
}