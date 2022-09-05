using UnityEngine;
using TMPro;
using Softweather.Core;

namespace Softweather.UI
{
    [RequireComponent(typeof(GameController))]
    public class DisplayScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentScoreText;

        private GameController myGameController;

        private void Awake()
        {
            myGameController = GetComponent<GameController>();
        }

        private void OnEnable()
        {
            myGameController.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            myGameController.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int value)
        {
            currentScoreText.text = "Score: " + value.ToString();
        }
    }
}