using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    public class PlayingGameView : MonoBehaviour
    {
        [SerializeField] private Button endGameButton;

        [Header("HUD")] 
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private TMP_Text scoreText;


        public event Action EndGameClicked;

        private void Awake ()
        {
            endGameButton.onClick.AddListener(OnEndGameClicked);
        }

        private void OnDestroy ()
        {
            endGameButton.onClick.RemoveListener(OnEndGameClicked);
        }

        public void SetGold (int gold)
        {
            goldText.text = $"Gold: {gold}";
        }

        public void SetLives (int lives)
        {
            livesText.text = $"Lives: {lives}";
        }

        public void SetScore (int score)
        {
            scoreText.text = $"Score: {score}";
        }
        

        private void OnEndGameClicked()
        {
            EndGameClicked?.Invoke();
        }
    }
}