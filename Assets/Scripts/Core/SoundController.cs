using UnityEngine;

namespace Softweather.Core
{
    public class SoundController : MonoBehaviour
    {
        private static SoundController soundController;

        private void Awake()
        {
            if (soundController == null)
            {
                soundController = this;
                DontDestroyOnLoad(soundController);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}