using UnityEngine;

namespace PPop.Input
{
    [RequireComponent(typeof(MeshCollider))]
    public class InteractiveTile : Interactive
    {
        protected virtual void Awake()
        {
            RaycastStrategy = new MeshRaycastStrategy(transform);
        }
    }
}
