using System.Collections;
using UnityEngine;
using Softweather.Enemy;
using Softweather.Core;

namespace Softweather.Player
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float range = 100f;
        [SerializeField] private float timeBetweenShots = 0.5f;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private GameObject enemyHitEffect;
        [SerializeField] private GameController myGameController;
        [SerializeField] private SoundFXController mySoundFXController;

        private bool canShoot = true;

        private void Start()
        {
            canShoot = true;
        }

        public void Fire()
        {
            if (canShoot && !myGameController.IsPlayerDead)
            {
                StartCoroutine(Shoot());
            }
        }

        private IEnumerator Shoot()
        {
            canShoot = false;
            PlayMuzzleFlash();
            ProcessRaycast();
            mySoundFXController.PlayFireSound();
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
                if (hit.transform.TryGetComponent(out DamageTaker damageTaker))
                {
                    damageTaker.DealDamage();
                    CreateHitImpact(hit, enemyHitEffect);
                }
                else
                {
                    CreateHitImpact(hit, hitEffect);
                }

                if (hit.transform.TryGetComponent(out ScoreAdder scoreAdder))
                {
                    scoreAdder.AddScore();
                }
            }
            else
            {
                return;
            }
        }

        private void CreateHitImpact(RaycastHit hit, GameObject hitEffect)
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.1f);
        }
    }
}