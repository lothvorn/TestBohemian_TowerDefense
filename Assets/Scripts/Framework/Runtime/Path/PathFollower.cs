using System;
using Core;
using Framework.Runtime.Interfaces;
using UnityEngine;

namespace Framework.Runtime.Path
{
    public class PathFollower : MonoBehaviour, IGoalReachedSignal
    {
        private IPathProvider _pathProvider;
        private float _moveSpeed;
        private int _currentTargetIndex;

        public event Action GoalReached;

        public void Initialize(
            IPathProvider pathProvider, 
            float moveSpeed)
        {
            _pathProvider = pathProvider;
            
            _moveSpeed = moveSpeed;
            _currentTargetIndex = 0;
            
            transform.position = _pathProvider.GetPoint(0);
        }

        private void Update()
        {
            MoveTowardsCurrentTarget();
        }

        private void MoveTowardsCurrentTarget()
        {
            Vector3 targetPosition = _pathProvider.GetPoint(_currentTargetIndex);
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            transform.position = newPosition;
            
            if ((transform.position - targetPosition).sqrMagnitude <= 0.0001f)
            {
                AdvanceToNextPoint();
            }
        }

        private void AdvanceToNextPoint()
        {
            _currentTargetIndex++;

            if (_currentTargetIndex >= _pathProvider.PointCount)
            {
                enabled = false;
                GoalReached?.Invoke();
            }
        }

        
    }
}