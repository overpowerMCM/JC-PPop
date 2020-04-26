using PPop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PPop.Input
{
    public class RaycastStrategy : IRaycastStrategy
    {
        public virtual bool IsValid(Camera currentEventCamera, Vector3 eventPosition)
        {
            return false;
        }
    }
}
