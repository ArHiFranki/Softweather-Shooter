using System.Collections;
using UnityEngine;

namespace Softweather.Player
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float range = 100f;
        [SerializeField] private float bodyDamage = 20f;
        [SerializeField] private float headDamage = 50f;
        [SerializeField] private float timeBetweenShots = 0.5f;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private GameObject hitEffect;

        private bool canShoot = true;

        private void Start()
        {
            canShoot = true;
        }

        public void Fire()
        {
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }

        private IEnumerator Shoot()
        {
            canShoot = false;
            PlayMuzzleFlash();
            ProcessRaycast();
            yield return new WaitForSeconds(timeBetweenShots);
            canShoot = true;
        }

        private void PlayMuzzleFlash()
        {
            muzzleFlash.Play();
        }

        private void ProcessRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
            {
                CreateHitImpact(hit);

                //if (hit.transform.TryGetComponent(out EnemyHealth target))
                //{
                //    target.TakeDamage(damage);
                //}
                Debug.Log("hit: " + hit.transform.name);
            }
            else
            {
                return;
            }
        }

        private void CreateHitImpact(RaycastHit hit)
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.1f);
        }
    }
}