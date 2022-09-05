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
        [SerializeField] private Button soundButton;
        [SerializeField] private TMP_Text bestScoreText;
        [SerializeField] private Image soundOnIcon;
        [SerializeField] private Image soundOffIcon;

        private ScoreKeeper myScoreKeeper;
        private bool muted = false;

        private void Awake()
        {
            myScoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        private void Start()
        {
            ManageSettings();
            UpdateButtonIcon();
            DisplayBestScore();

            AudioListener.pause = muted;
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayButtonClick);
            exitButton.onClick.AddListener(OnExitButtonClick);
            soundButton.onClick.AddListener(OnSoundButtonClick);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClick);
            exitButton.onClick.RemoveListener(OnExitButtonClick);
            soundButton.onClick.RemoveListener(OnSoundButtonClick);
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

        private void OnSoundButtonClick()
        {
            if (!muted)
            {
                muted = true;
                AudioListener.pause = true;
            }
            else
            {
                muted = false;
                AudioListener.pause = false;
            }

            SaveSettings();
            UpdateButtonIcon();
        }

        private void DisplayBestScore()
        {
            bestScoreText.text = "Hight score: " + myScoreKeeper.BestScore;
        }

        private void ManageSettings()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsValues.IsMuted))
            {
                PlayerPrefs.SetInt(PlayerPrefsValues.IsMuted, 0);
                LoadSettings();
            }
            else
            {
                LoadSettings();
            }
        }

        private void LoadSettings()
        {
            muted = PlayerPrefs.GetInt(PlayerPrefsValues.IsMuted) == 1;
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetInt(PlayerPrefsValues.IsMuted, muted ? 1 : 0);
        }

        private void UpdateButtonIcon()
        {
            if (!muted)
            {
                soundOnIcon.enabled = true;
                soundOffIcon.enabled = false;
            }
            else
            {
                soundOnIcon.enabled = false;
                soundOffIcon.enabled = true;
            }
        }
    }
}