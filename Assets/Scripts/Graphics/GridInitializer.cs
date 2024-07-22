using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<Node> _nodes;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes level grid and its objects
        /// </summary>
        private void InitializeGrid()
        {
            foreach (var nodeData in levelData.nodeCoordinates)
            {
                // Find the prefab of the node type
                var prefab = prefabOptions.nodePrefabs
                    .First(pair => pair.left == nodeData.right)
                    .right;
                var coords = (Vector2)nodeData.left * levelData.CellSize;
                
                CreateNode(prefab, coords);
            }
        }


        /// <summary>
        /// Creates a node from prefab on coordinates
        /// </summary>
        private void CreateNode(GameObject nodePrefab, Vector3 coords)
        {
            var nodeObject = Instantiate(nodePrefab, coords, Quaternion.identity, transform);

            var node = nodeObject.GetComponent<Node>();
            _nodes.Add(node);
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