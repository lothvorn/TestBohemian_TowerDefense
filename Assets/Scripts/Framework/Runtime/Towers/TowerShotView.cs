using UnityEngine;

namespace Framework.Runtime.Towers
{
    public class TowerShotView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _shotLineRenderer;
        [SerializeField] private float _shotVisualDurationSeconds = 0.05f;
        [SerializeField] private Transform _rayOrigin;

        private Vector3 _targetPosition;
        private float _remainingShotVisualSeconds;

        private void Start()
        {
            _shotLineRenderer.enabled = false;
        }

        private void Update()
        {
            if (!_shotLineRenderer.enabled)
                return;
            _shotLineRenderer.SetPosition(0, _rayOrigin.position);
            _shotLineRenderer.SetPosition(1, transform != null ? _targetPosition :  _rayOrigin.position );
        }


        private void LateUpdate()
        {
            if (!_shotLineRenderer.enabled)
                return;

            _remainingShotVisualSeconds -= Time.deltaTime;

            if (_remainingShotVisualSeconds <= 0f)
            {
                _shotLineRenderer.enabled = false;
                enabled = false;
            }

        }

        public void ShowShot(Transform targetTransform)
        {
            _targetPosition = targetTransform.position; 
            
            _shotLineRenderer.SetPosition(0, _rayOrigin.position);
            _shotLineRenderer.SetPosition(1, _targetPosition);
            
            enabled = true;
            _shotLineRenderer.enabled = true;
            _shotLineRenderer.positionCount = 2;
            _remainingShotVisualSeconds = _shotVisualDurationSeconds;
        }
    }
}
