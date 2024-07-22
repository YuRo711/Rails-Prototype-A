using System;
using System.Collections.Generic;
using Nodes;
using UnityEngine;
using Utils;

namespace Logic
{
    public class Graph
    {
        public Dictionary<Tuple<Vector3, Vector3>, bool> PathsActivity = new ();
        public Dictionary<Vector3, NodeType> PointTypes = new ();
    }
}

