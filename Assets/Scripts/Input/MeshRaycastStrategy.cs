using UnityEngine;

namespace PPop.Input
{
    public class MeshRaycastStrategy : RaycastStrategy
    {
        Collider _collider;

        public MeshRaycastStrategy(Transform component)
        {
            _collider = component.GetComponent<MeshCollider>();
        }

        public override bool IsValid(Camera currentEventCamera, Vector3 eventPosition)
        {
            RaycastHit info = new RaycastHit();
            return _collider.Raycast(currentEventCamera.ScreenPointToRay(eventPosition), out info, float.MaxValue);
        }

    }
}
