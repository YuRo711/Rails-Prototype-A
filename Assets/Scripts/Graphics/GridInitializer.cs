using System;
using System.Collections.Generic;
using System.Linq;
using Logic;
using Nodes;
using ScriptableObjects;
using UnityEngine;

namespace Graphics
{
    public class GridInitializer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LevelData levelData;
        [SerializeField] private PrefabOptions prefabOptions;
        [SerializeField] private Graph graph;

        private readonly List<GameObject> _nodes = new();

        #endregion

        #region Methods

        /// <summary>
        /// Initializes level grid and its objects
        /// </summary>
        private void InitializeGrid()
        {
            CreateNodes();
            CreatePaths();
        }

        private void CreateNodes()
        {
            foreach (var nodeData in levelData.nodeCoordinates)
            {
                // Find the prefab of the node type
                var prefab = prefabOptions.nodePrefabs
                    .First(prefabPair => prefabPair.left.Equals(nodeData.right))
                    .right;
                var coords = (Vector2)nodeData.left * levelData.cellSize;
                
                CreateNode(prefab, coords);
            }
        }

        private void CreatePaths()
        {
            
            foreach (var pathNodes in levelData.nodeConnectionsActivity)
            {
                var isActive = pathNodes.right;
                var nodeIndexes = pathNodes.left;
                
                CreatePath(_nodes[nodeIndexes.left], _nodes[nodeIndexes.right], isActive);
            }
        }

        /// <summary>
        /// Creates a node from prefab on coordinates
        /// </summary>
        private void CreateNode(GameObject nodePrefab, Vector3 coords)
        {
            var nodeObject = Instantiate(nodePrefab, coords, Quaternion.identity, transform);
            _nodes.Add(nodeObject);
        }

        /// <summary>
        /// Creates a line between nodes
        /// </summary>
        private void CreatePath(GameObject start, GameObject finish, bool isActive)
        {
            var lineObject = Instantiate(prefabOptions.linePrefab, start.transform);
            var line = lineObject.GetComponent<Line>();
            
            line.Connect(start.transform.position, finish.transform.position);
            line.SetPathActive(isActive);
        }

        #endregion

        #region MB Callbacks

        private void Awake()
        {
            InitializeGrid();
        }

        #endregion
    }
}