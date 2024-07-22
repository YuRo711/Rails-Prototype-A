using System;
using System.Collections.Generic;
using System.Linq;
using Logic;
using Nodes;
using Player;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Graphics
{
    public class GridInitializer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LevelData levelData;
        [SerializeField] private PrefabOptions prefabOptions;
        
        private readonly List<GameObject> _nodes = new();
        private readonly Graph _graph = new();

        #endregion

        #region Methods

        /// <summary>
        /// Initializes level grid and its objects
        /// </summary>
        private void InitializeGrid()
        {
            CreateNodes();
            CreatePaths();
            FindObjectOfType<PlayerMovement>().Graph = _graph;
        }

        private void CreateNodes()
        {
            foreach (var nodeData in levelData.nodeCoordinates)
            {
                var coords = (Vector2)nodeData.left * levelData.cellSize;
                CreateNode(nodeData.right, coords);
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
        /// Creates a node from type prefab on coordinates
        /// </summary>
        private void CreateNode(NodeType nodeType, Vector3 coords)
        {
            // Find the prefab of the node type
            var prefab = prefabOptions.nodePrefabs
                .First(prefabPair => prefabPair.left.Equals(nodeType))
                .right;
            
            var nodeObject = Instantiate(prefab, coords, Quaternion.identity, transform);
            _nodes.Add(nodeObject);
            _graph.PointTypes[nodeObject.transform.position] = nodeType;
        }

        /// <summary>
        /// Creates a line between nodes
        /// </summary>
        private void CreatePath(GameObject start, GameObject finish, bool isActive)
        {
            var lineObject = Instantiate(prefabOptions.linePrefab, start.transform);
            var line = lineObject.GetComponent<Line>();
            var startCoords = start.transform.position;
            var finishCoords = finish.transform.position;

            var pathCoords = Tuple.Create(startCoords, finishCoords);
            _graph.PathsActivity[pathCoords] = isActive;
            
            line.Connect(startCoords, finishCoords);
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