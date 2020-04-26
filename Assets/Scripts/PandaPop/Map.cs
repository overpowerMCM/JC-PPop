using PPop.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PPop
{
    public class Map : MonoBehaviour
    {
        [SerializeField]
        GameObject _tileContainer;
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
                    clickableTile.OnClick.AddListener((t) => Debug.Log(string.Format("Clicked: {0},{1}", t.X, t.Y)));
                }
            }


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