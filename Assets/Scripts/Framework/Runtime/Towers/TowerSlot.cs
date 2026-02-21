using System;
using TMPro;
using UnityEngine;

namespace Framework.Runtime.Towers
{
    public class TowerSlot : MonoBehaviour
    {
        [SerializeField] private Transform towerAnchor;
        [SerializeField] private int towerCost = 5;
        [SerializeField] private TMP_Text towerCostText;
        private TowerView _currentTowerView;
        public bool IsOccupied => _currentTowerView != null;
        public Transform Anchor => towerAnchor;
        public TowerView CurrentTowerView => _currentTowerView;
        public int TowerCost => towerCost;

        public event Action<TowerSlot> Clicked;


        private void Start()
        {
            towerCostText.text = string.Empty;
        }

        private void OnMouseDown()
        {
            Clicked?.Invoke(this);
        }

        private void OnMouseEnter()
        {
            towerCostText.text ="Tower Cost: " + towerCost;
        }

        private void OnMouseExit()
        {
            towerCostText.text = "";
        }

        public void AssignTower(TowerView towerTransform)
        {
            _currentTowerView = towerTransform;
        }

        public void Clear()
        {
            _currentTowerView = null;
        }
    }
}
