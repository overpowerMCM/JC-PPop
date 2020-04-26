using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PPop.Input
{
    public class InteractiveRaycaster : BaseRaycaster
    {
        List<Interactive> _results = new List<Interactive>();

        public override Camera eventCamera => Camera.main;

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            var assets = InteractiveObjectsRegistry.Instance.InteractiveObjects;
            if (assets.Count == 0)
                return;

            int displayIndex;
            var currentEventCamera = eventCamera; 

            displayIndex = currentEventCamera.targetDisplay;

            var eventPosition = eventData.position;

            Vector2 pos = currentEventCamera.ScreenToViewportPoint(eventPosition);

            // si salimos del viewport
            if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f)
                return;

            GetRaycastHitResults(currentEventCamera, eventPosition, assets);

            for (int i = 0; i < _results.Count; i++)
            {
                //NOTA: No se chequea por distancia de hit menores a 0, osea, detras de camara.
                var castResult = new RaycastResult
                {
                    gameObject = _results[i].gameObject,
                    module = this, // registramos este raycaster al modulo de EventSystem
                    distance = 0,
                    screenPosition = eventPosition,
                    index = resultAppendList.Count,
                    depth = 0,
                    sortingLayer = 0,
                    sortingOrder = 0
                };
                resultAppendList.Add(castResult);
            }
        }

        private void GetRaycastHitResults(Camera currentEventCamera, Vector3 eventPosition, List<Interactive> assets)
        {
            _results.Clear();

            for (int i = 0; i < assets.Count; i++)
            {
                Interactive asset = assets[i];

                if (asset.RaycastStrategy.IsValid(currentEventCamera, eventPosition))
                {
                    _results.Add(asset);
                }
            }

        }
    }
}
