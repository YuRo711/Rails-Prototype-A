using Nodes;
using UnityEngine;
using Utils;

namespace ScriptableObjects
{
    /// <summary>
    /// Stores object prefabs for building a level
    /// </summary>
    [CreateAssetMenu(menuName = "PrefabOptions")]
    public class PrefabOptions : ScriptableObject
    {
        #region Fields

        public GameObject linePrefab;
        public SerializablePair<NodeType, GameObject>[] nodePrefabs;

        #endregion
    }
}