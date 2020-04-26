using PPop.Interfaces;
using UnityEngine;

namespace PPop.Input
{
    public class Interactive : MonoBehaviour
    {
        private IRaycastStrategy _raycastStrategy;

        protected Transform _catchedTransform;

        public Transform CatchedTransform { get => _catchedTransform ?? (_catchedTransform = transform); }
        public IRaycastStrategy RaycastStrategy { get => _raycastStrategy ?? (_raycastStrategy = new RaycastStrategy()); set => _raycastStrategy = value; }

        private void OnEnable()
        {
            InteractiveObjectsRegistry.Instance.Register(this);
        }

        private void OnDisable()
        {
            InteractiveObjectsRegistry.Instance.Unregister(this);
        }
    }
}
