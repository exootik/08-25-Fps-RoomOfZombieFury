using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [Header("Shotgun config")]
    public int pelletCount = 8;
    public float spreadAngle = 10f;

    public override void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Fire1"))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TryReload();
        }
    }
    public override void Shoot()
    {
        Camera cam = Camera.main;
        Vector3 origin = cam.transform.position;
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        for (int i = 0; i < pelletCount; i++)
        {
            Ray ray = cam.ScreenPointToRay(screenCenter);

            Vector3 dir = ray.direction;

            float yaw = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            dir = Quaternion.AngleAxis(yaw, cam.transform.up) * dir;

            // On tourne autour de l’axe "droite" de la caméra (pour l’écart vertical)
            float pitch = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            dir = Quaternion.AngleAxis(pitch, cam.transform.right) * dir;

            RaycastHit hit;
            Vector3 target;

            if (Physics.Raycast(ray.origin, dir, out hit, gunData.shootingRange, gunData.targetLayerMask))
            {
                target = hit.point;
            }
            else
            {
                target = ray.origin + dir * gunData.shootingRange;
            }
            StartCoroutine(BulletFire(target, hit));
        }
    }

    private IEnumerator BulletFire(Vector3 target, RaycastHit hit)
    {
        GameObject bulletTrail = Instantiate(gunData.bulletTrailPrefab, gunMuzzle.position, Quaternion.identity);

        while (bulletTrail != null && Vector3.Distance(bulletTrail.transform.position, target) > 0.1f)
        {   
            bulletTrail.transform.position = Vector3.MoveTowards(bulletTrail.transform.position, target, Time.deltaTime * gunData.bulletSpeed);
            yield return null;
        }
        Destroy(bulletTrail);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                BulletHitFX(hit);
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                ZombieAI enemy = hit.collider.GetComponent<ZombieAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(gunData.shootDamage);

                    ParticleSystem blood = Instantiate(zombieBloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(blood.gameObject, blood.main.duration);
                }
            }
        }
    }

    private void BulletHitFX(RaycastHit hit)
    {
        Vector3 hitPosition = hit.point + hit.normal * 0.01f;

        GameObject bulletHole = Instantiate(bulletHolePrefab, hitPosition, Quaternion.LookRotation(hit.normal));
        //GameObject bulletParticle = Instantiate(bulletHitParticlePrefab, hit.point, Quaternion.LookRotation(hit.normal));

        bulletHole.transform.parent = hit.collider.transform;
        //bulletParticle.transform.parent = hit.collider.transform;

        Destroy(bulletHole, 5f);
        //Destroy(bulletParticle, 5f);
    }
}
