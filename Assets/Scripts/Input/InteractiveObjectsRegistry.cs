using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPop.Input
{
    public class InteractiveObjectsRegistry
    {
        private static InteractiveObjectsRegistry _instance = null;
        List<Interactive> _interactiveObjects;

        public static InteractiveObjectsRegistry Instance { get => _instance ?? (_instance = new InteractiveObjectsRegistry()); }
        public List<Interactive> InteractiveObjects { get => _interactiveObjects; }

        public void Register( Interactive interactive )
        {
            if (null == _interactiveObjects)
                _interactiveObjects = new List<Interactive>();

            if (!_interactiveObjects.Contains(interactive))
                _interactiveObjects.Add(interactive);
        }

        public void Unregister(Interactive interactive)
        {
            if ( null != _interactiveObjects )
                _interactiveObjects.Remove(interactive);
        }
    }
}
