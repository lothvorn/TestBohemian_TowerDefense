using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Framework.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [FormerlySerializedAs("startButton")] [SerializeField] private Button _startButton;

        public event Action StartClicked;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            StartClicked?.Invoke();
        }
    }
}

