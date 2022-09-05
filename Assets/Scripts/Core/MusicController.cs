using UnityEngine;

namespace Softweather.Core
{
    public class MusicController : MonoBehaviour
    {
        private static MusicController musicController;

        private void Awake()
        {
            if (musicController == null)
            {
                musicController = this;
                DontDestroyOnLoad(musicController);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}