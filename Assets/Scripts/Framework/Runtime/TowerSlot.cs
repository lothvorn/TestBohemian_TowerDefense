using UnityEngine;

namespace Framework.Runtime
{
    public class TowerSlot : MonoBehaviour
    {
        [SerializeField] private Transform towerAnchor;

        private Transform currentTowerTransform;

        public bool IsOccupied => currentTowerTransform != null;

        public Transform Anchor => towerAnchor;

        public Transform CurrentTowerTransform => currentTowerTransform;

        public void AssignTower(Transform towerTransform)
        {
            currentTowerTransform = towerTransform;
        }

        public void Clear()
        {
            currentTowerTransform = null;
        }
    }
}
