using Nodes;
using UnityEngine;
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
        public readonly float CellSize = 1f;

        #endregion
    }
}