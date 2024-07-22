using System;
using Player;
using ScriptableObjects;
using UnityEngine;

namespace Nodes
{
    public class StartNode : MonoBehaviour, INode
    {
        [SerializeField] private PrefabOptions prefabOptions;
        
        private void Awake()
        {
            var player = Instantiate(prefabOptions.playerPrefab, transform);
            player.GetComponent<PlayerMovement>().SetStartPoint(transform.position);
        }
    }
}