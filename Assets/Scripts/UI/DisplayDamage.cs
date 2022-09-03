using System.Collections;
using UnityEngine;

namespace Softweather.UI
{
    public class DisplayDamage : MonoBehaviour
    {
        [SerializeField] private Canvas impactCanvas;
        [SerializeField] private float impactTime = 0.3f;

        private void Start()
        {
            impactCanvas.enabled = false;
        }

        public void ShowDamageImpact()
        {
            StartCoroutine(ShowSplatter());
        }

        private IEnumerator ShowSplatter()
        {
            impactCanvas.enabled = true;
            yield return new WaitForSeconds(impactTime);
            impactCanvas.enabled = false;
        }
    }
}