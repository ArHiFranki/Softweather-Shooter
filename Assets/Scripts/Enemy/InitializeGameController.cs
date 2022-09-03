using Softweather.Core;
using UnityEngine;

namespace Softweather.Enemy
{
    public class InitializeGameController : MonoBehaviour
    {
        [SerializeField] private ScoreAdder headScoreAdder;
        [SerializeField] private ScoreAdder bodyScoreAdder;

        private GameController myGameController;

        public void InitGameController(GameController gameController)
        {
            myGameController = gameController;
            headScoreAdder.InitGameController(myGameController);
            bodyScoreAdder.InitGameController(myGameController);
        }
    }
}