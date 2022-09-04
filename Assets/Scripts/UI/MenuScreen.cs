using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Softweather.Core;

namespace Softweather.UI
{
    public class MenuScreen : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TMP_Text bestScoreText;

        private ScoreKeeper myScoreKeeper;

        private void Awake()
        {
            myScoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        private void Start()
        {
            DisplayBestScore();
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayButtonClick);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClick);
            exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnPlayButtonClick()
        {
            SceneManager.LoadScene(ScenesNames.GameScene);
            Time.timeScale = 1;
        }

        private void OnExitButtonClick()
        {
            Debug.Log("Application Quit");
            Application.Quit();
        }

        private void DisplayBestScore()
        {
            bestScoreText.text = "Hight score: " + myScoreKeeper.BestScore;
        }
    }
}