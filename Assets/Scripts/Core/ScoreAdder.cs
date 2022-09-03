using UnityEngine;

namespace Softweather.Core
{
    public class ScoreAdder : MonoBehaviour
    {
        [SerializeField] private int score;
        [SerializeField] private GameController myGameController;

        public void AddScore()
        {
            myGameController.UpdateScore(score);
        }
    }
}