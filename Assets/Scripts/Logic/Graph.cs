using System;
using System.Collections.Generic;
using System.Linq;
using Nodes;
using UnityEngine;
using Utils;

namespace Logic
{
    /// <summary>
    /// Paths graph abstraction 
    /// </summary>
    public class Graph
    {
        #region Fields
        
        // Which paths (coords to coords) are active
        public readonly Dictionary<Tuple<Vector3, Vector3>, bool> PathsActivity = new ();
        
        // Which point type is on each coords
        public readonly Dictionary<Vector3, NodeType> PointTypes = new ();
        
        // Junction coordinates
        public readonly Dictionary<JunctionNode, Vector3> JunctionCoords = new();

        #endregion

        
        /// <summary>
        /// Changes activity of paths coming from junction
        /// </summary>
        public void ChangeDirection(JunctionNode junctionNode)
        {
            var coords = JunctionCoords[junctionNode];
            var allPaths = PathsActivity
                .Where(pathActivity => pathActivity.Key.Item1 == coords);
            var activePath = allPaths
                .First(path => path.Value);
            var inactivePath = allPaths
                .First(path => !path.Value);

            PathsActivity[activePath.Key] = false;
            PathsActivity[inactivePath.Key] = true;
        }

        public void Clear()
        {
            PathsActivity.Clear();
            PointTypes.Clear();
            JunctionCoords.Clear();
        }
    }
}

