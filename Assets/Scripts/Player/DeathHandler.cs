using UnityEngine;

namespace Softweather.Player
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] private Canvas gameOverCanvas;
        [SerializeField] private Canvas gunReticleCanvas;

        private void Start()
        {
            gameOverCanvas.gameObject.SetActive(false);
        }

        public void HandleDeath()
        {
            gameOverCanvas.gameObject.SetActive(true);
            gunReticleCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}