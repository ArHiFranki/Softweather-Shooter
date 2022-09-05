using UnityEngine;

namespace Softweather.Core
{
    public class ScoreAdder : MonoBehaviour
    {
        [SerializeField] private int score;
        private GameController myGameController;

        public void AddScore()
        {
            myGameController.UpdateScore(score);
        }

        public void InitGameController(GameController gameController)
        {
            myGameController = gameController;
        }
    }
}