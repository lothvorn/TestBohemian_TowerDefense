using System;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    public class PlayingGameView : MonoBehaviour
    {
        [SerializeField] private Button endGameButton;

        public event Action EndGameClicked;

        private void Awake()
        {
            endGameButton.onClick.AddListener(OnEndGameClicked);
        }

        private void OnDestroy()
        {
            endGameButton.onClick.RemoveListener(OnEndGameClicked);
        }

        private void OnEndGameClicked()
        {
            EndGameClicked?.Invoke();
        }
    }
}
