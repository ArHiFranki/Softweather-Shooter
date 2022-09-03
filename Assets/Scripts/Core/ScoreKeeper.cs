using UnityEngine;

namespace Softweather.Core
{
    public class ScoreKeeper : MonoBehaviour
    {
        private int bestScore;
        private static ScoreKeeper instance;

        public int BestTime => bestScore;

        private void Awake()
        {
            ManageSingleton();
            PlayerPrefs.GetFloat(PlayerPrefsValues.BestScore, bestScore);
        }

        private void ManageSingleton()
        {
            if (instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void UpdateBestScore(int value)
        {
            bestScore = value;
            Mathf.Clamp(bestScore, 0, int.MaxValue);
            PlayerPrefs.SetFloat(PlayerPrefsValues.BestScore, bestScore);
        }
    }
}