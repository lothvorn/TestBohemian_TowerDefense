using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _gameEndText;

        public event Action RestartClicked;
        

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
        
        }

        private void OnRestartClicked()
        {
            RestartClicked?.Invoke();
        }

        public void SetScreenValues(int score, bool gameWon)
        {
            _scoreText.text = "Final score " + score;
            if (gameWon)
                _gameEndText.text = "Victory";
            else
            {
                _gameEndText.text = "Defeat";
            }

        }

    }
}