using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PPop.Input
{
    [RequireComponent(typeof(MapTile))]
    public class ClickableTile : MonoBehaviour, IPointerClickHandler
    {
        MapTile _tile;
        [Serializable]
        public class ClickableTileEvent : UnityEvent<MapTile>
        {
        }

        // Event delegates triggered on click.
        [SerializeField]
        protected ClickableTileEvent _onClick = new ClickableTileEvent();

        void Awake()
        {
            _tile = GetComponent<MapTile>();
        }

        public ClickableTileEvent OnClick
        {
            get { return _onClick; }

            set { _onClick = value; }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }

        private void Press()
        {
            if (!isActiveAndEnabled)
                return;

            if (_onClick != null)
                _onClick.Invoke(_tile);
        }
    }
}
