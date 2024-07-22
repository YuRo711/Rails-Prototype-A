using Nodes;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace ScriptableObjects
{
    /// <summary>
    /// Data of a level (can be created in editor)
    /// </summary>
    [CreateAssetMenu(menuName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        #region Fields

        public SerializablePair<Vector2Int, NodeType>[] nodeCoordinates;
        
        // A pair of node indexes and is the path active
        public SerializablePair<SerializablePair<int, int>, bool>[] nodeConnectionsActivity;
        
        public float cellSize = 1f;

        #endregion
    }
}