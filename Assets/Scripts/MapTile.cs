using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinding;
using PPop.Interfaces;
using UnityEngine;

namespace PPop
{
    [RequireComponent( typeof( MeshRenderer ) )]
    public class MapTile : MonoBehaviour, IAStarNode
    {
        Transform _cachedtransform;
        IHeuristic _heuristic;
        float _weight = 0f;
        MeshRenderer _renderer;

        public IHeuristic Heuristic { get => _heuristic??(_heuristic = new Heuristics.Euclidian()); set => _heuristic = value; }
        public float Weight { get => _weight; }
        public Vector3 Position
        {
            get => _cachedtransform.position;
            set => _cachedtransform.position = value;
        }
        public IEnumerable<IAStarNode> Neighbours { get; private set; }

        void Awake()
        {
            _cachedtransform = transform;
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Setup(Texture tex, float weight, List<MapTile> neighbours)
        {
            Neighbours = neighbours;
            _weight = weight;
            _renderer.material.mainTexture = tex;
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
