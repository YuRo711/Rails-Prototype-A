using System;
using System.Linq;
using Logic;
using Nodes;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields

        public Graph Graph;
        
        [SerializeField] private float speed;
        [SerializeField] private GameEvent winEvent;

        private Vector3 _lastPoint;
        private Vector3 _nextPoint;
        private float _progressToNext;

        #endregion
        
        #region Unity Methods
        
        /// <summary>
        /// Sets the start point
        /// </summary>
        public void SetStartPoint(Vector3 coords)
        {
            _lastPoint = coords;
        }
        
        /// <summary>
        /// Sets the next point (first one that has a path from the last)
        /// </summary>
        public void GetNextPoint()
        {
            _nextPoint = Graph.PathsActivity
                .First(pathActivity =>
                    pathActivity.Key.Item1 == _lastPoint && pathActivity.Value)
                .Key.Item2;
            Debug.Log(_nextPoint);
        }
        
        /// <summary>
        /// Moves the player along the path if user is pressing Space
        /// </summary>
        private void Move()
        {
            if (!Input.GetKey(KeyCode.Space))
                return;
            
            // we don't move in a direction, we calculate what the next position will be
            // it's more "expensive" but insignificant in a small game such as ours
            // makes programming it way easier, though
            transform.position = Vector3.Lerp(_lastPoint, _nextPoint, _progressToNext);
            _progressToNext += 0.001f * speed;
            
            // If we've reached the next point, we set a new one
            if (_progressToNext >= 1)
                UpdatePoint();
        }
        
        /// <summary>
        /// Called when the player has reached a new point
        /// </summary>
        private void UpdatePoint()
        {
            // Raises level win event if the point is a finish
            if (Graph.PointTypes[_nextPoint] == NodeType.Finish)
            {
                winEvent.Raise();
                return;
            }

            _lastPoint = _nextPoint;
            GetNextPoint();
        }

        #endregion
        
        #region MB Callbacks

        private void Update()
        {
            if (Graph == null) 
                return;
            if (_nextPoint == default)
                GetNextPoint();
            
            Move();
        }

        #endregion
    }
}