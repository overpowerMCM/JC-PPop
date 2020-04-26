using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PPop
{
    public class Map : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Builders.HexagonalMapBuilder builder = new Builders.HexagonalMapBuilder();
            List<MapTile> mapTiles =  builder.Create( new Data.DefaultDataProvider() );

            MapTile from = mapTiles.Find(t => t.X == 7 && t.Y == 3);
            MapTile to = mapTiles.Find(t => t.X == 6 && t.Y == 7);

            var path = PathFinding.AStar.GetPath(from, to);

            Debug.Log("tiles: " + path.Count);

            for( int i = 0; i < path.Count; i++ )
            {
                ((MapTile)path[i]).SetTintColor((i == 0 || i == path.Count - 1) ? Color.green : Color.red);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}