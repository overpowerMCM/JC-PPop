﻿using PPop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPop.Model
{
    [Serializable]
    public class TileModel
    {
        public int id;
        public string Name;
        public float weight;
        public bool walkable;
    }
}
