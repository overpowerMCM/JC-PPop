using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinding;
using PPop.Interfaces;
using UnityEngine;

namespace PPop
{
    [Serializable]
    [RequireComponent( typeof( MeshRenderer ) )]
    public class MapTile : MonoBehaviour, IAStarNode
    {
        Transform _cachedtransform;
        IHeuristic _heuristic;
        [SerializeField]float _weight = 0f;
        [SerializeField] int _x, _y;
        MeshRenderer _renderer;

        [SerializeField]List<MapTile> _neighbours;
        
        public IHeuristic Heuristic { get => _heuristic??(_heuristic = new Heuristics.Euclidian()); set => _heuristic = value; }
        public float Weight { get => _weight; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public Vector3 Position
        {
            get => _cachedtransform.localPosition;
            set => _cachedtransform.localPosition = value;
        }
        public IEnumerable<IAStarNode> Neighbours { get=> _neighbours; private set=> _neighbours = (List<MapTile>)value; }

        void Awake()
        {
            _cachedtransform = transform;
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Setup(Texture tex, float weight, List<MapTile> neighbours)
        {
            Neighbours = neighbours;
            _weight = weight;
            _renderer.material = Resources.Load<Material>("Materials/Tile");
            _renderer.material.SetTexture("_MainTex", tex);
            SetTintColor(Color.white);
        }

        public void SetTintColor( Color c )
        {
            _renderer.material.SetColor("_ColorTint", c);
        }

        public float CostTo(IAStarNode neighbour)
        {
            return Heuristic.GetHeuristic( this, (MapTile)neighbour, true);
        }

        public float EstimatedCostTo(IAStarNode target)
        {
            return Heuristic.GetHeuristic( this, (MapTile)target );
        }
    }
}
