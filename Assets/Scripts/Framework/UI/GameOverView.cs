using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Framework.Runtime.GameStates
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        public event Action RestartClicked;
        public event Action ExitClicked;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnRestartClicked()
        {
            RestartClicked?.Invoke();
        }

        private void OnExitClicked()
        {
            ExitClicked?.Invoke();
        }
    }
}