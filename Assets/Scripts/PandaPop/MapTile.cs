using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinding;
using PPop.Input;
using PPop.Interfaces;
using UnityEngine;

namespace PPop
{
    [Serializable]
    [RequireComponent( typeof( MeshRenderer ) )]
    public class MapTile : InteractiveTile, IAStarNode
    {
        IHeuristic _heuristic;
        [SerializeField]float _weight = 0f;
        [SerializeField] int _x, _y;
        [SerializeField] bool _walkable;
        MeshRenderer _renderer;

        [SerializeField]List<MapTile> _neighbours;
        
        public IHeuristic Heuristic { get => _heuristic??(_heuristic = new Heuristics.Euclidian()); set => _heuristic = value; }
        public float Weight { get => _weight; }
        public bool Walkable { get => _walkable; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        public Vector3 Position
        {
            get => CatchedTransform.localPosition;
            set => CatchedTransform.localPosition = value;
        }
        public IEnumerable<IAStarNode> Neighbours { get=> _neighbours; private set=> _neighbours = (List<MapTile>)value; }

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Setup(Texture tex, float weight, bool walkable, List<MapTile> neighbours)
        {
            Neighbours = neighbours;
            _weight = weight;
            _walkable = Walkable;
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
