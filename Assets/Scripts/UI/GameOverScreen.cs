using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Softweather.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button exitButton;

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnRestartButtonClick);
            menuButton.onClick.AddListener(OnMenuButtonClick);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnRestartButtonClick);
            menuButton.onClick.RemoveListener(OnMenuButtonClick);
            exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnRestartButtonClick()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            Time.timeScale = 1;
        }

        private void OnMenuButtonClick()
        {
            SceneManager.LoadScene(ScenesNames.MenuScene);
            Time.timeScale = 1;
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}