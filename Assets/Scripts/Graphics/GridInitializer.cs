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
        [SerializeField] private GameEvent loadLevelEvent;
        
        private readonly List<GameObject> _nodes = new();
        private readonly List<GameObject> _edges = new();
        private Graph _graph;

        #endregion

        #region Methods

        /// <summary>
        /// Reloads current level
        /// </summary>
        public void Retry()
        {
            LoadLevel(levelData);
        }

        /// <summary>
        /// Loads next level
        /// </summary>
        public void NextLevel()
        {
            if (levelData.nextLevel == null) 
                return;
            
            LoadLevel(levelData.nextLevel);
        }

        /// <summary>
        /// Loads level from LevelData
        /// </summary>
        private void LoadLevel(LevelData level)
        {
            levelData = level;
            loadLevelEvent.Raise();
            ClearGrid();
            InitializeGrid();
        }

        /// <summary>
        /// Destroys all current nodes and edges
        /// </summary>
        private void ClearGrid()
        {
            foreach (var node in _nodes)
                Destroy(node);
            _nodes.Clear();

            foreach (var edge in _edges)
                Destroy(edge);
            _edges.Clear();

            Destroy(FindObjectOfType<PlayerMovement>().gameObject);
        }

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
            if (nodeType == NodeType.JunctionLeft || nodeType == NodeType.JunctionRight)
                _graph.JunctionCoords[nodeObject.GetComponent<JunctionNode>()] = coords;
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
            
            _edges.Add(lineObject);

            var pathCoords = Tuple.Create(startCoords, finishCoords);
            _graph.PathsActivity[pathCoords] = isActive;
            
            line.Connect(startCoords, finishCoords);
            line.SetPathActive(isActive);
            
        }

        private void SetGraph()
        {
            
            _graph = GraphManager.Instance.Graph;
        }

        #endregion

        #region MB Callbacks

        private void Awake()
        {
            SetGraph();
            InitializeGrid();
        }

        #endregion
    }
}