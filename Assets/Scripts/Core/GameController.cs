using UnityEngine;
using UnityEngine.Events;
using Softweather.Player;

namespace Softweather.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerHealth myPlayerHealth;

        private int currentScore;
        private int bestScore;
        private bool isPlayerDead;
        private ScoreKeeper myScoreKeeper;

        public event UnityAction<int> ScoreChanged;

        public bool IsPlayerDead => isPlayerDead;

        private void Awake()
        {
            myScoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        private void Start()
        {
            isPlayerDead = false;
            currentScore = 0;
            bestScore = myScoreKeeper.BestScore;
            ScoreChanged?.Invoke(currentScore);
        }

        private void OnEnable()
        {
            myPlayerHealth.PlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            myPlayerHealth.PlayerDied -= OnPlayerDied;
        }

        public void UpdateScore(int value)
        {
            currentScore += value;
            ScoreChanged?.Invoke(currentScore);
        }

        private void OnPlayerDied()
        {
            isPlayerDead = true;
            TryUpdateBestScore();
        }

        private void TryUpdateBestScore()
        {
            if (currentScore > bestScore || bestScore == 0)
            {
                myScoreKeeper.UpdateBestScore(currentScore);
            }
        }
    }
}