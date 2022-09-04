using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Softweather.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnRestartButtonClick);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnRestartButtonClick);
            exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnRestartButtonClick()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            Time.timeScale = 1;
        }

        private void OnExitButtonClick()
        {
            Debug.Log("Application Quit");
            Application.Quit();
        }
    }
}