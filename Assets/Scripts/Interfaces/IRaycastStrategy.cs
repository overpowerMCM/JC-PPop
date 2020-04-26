using UnityEngine;

namespace PPop.Interfaces
{
    public interface IRaycastStrategy
    {
        bool IsValid(Camera currentEventCamera, Vector3 eventPosition);
    }
}
