using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Vector2 direction;
    [SerializeField] float shootTime;
    [SerializeField] float timeBetweenStages;
    [SerializeField] float projectileSpeed;
    [SerializeField] bool isShooting;


    void Start()
    {
        StartCoroutine(CO_Shoot());
        InvokeRepeating("ToggleIsShooting", timeBetweenStages , timeBetweenStages);
    }

    IEnumerator CO_Shoot()
    {
        if (isShooting)
        {
            Projectile proj =Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            // Debug.Log((-transform.right) * projectileSpeed , gameObject);
            
            proj.velocity = (-transform.right) * projectileSpeed;
            
        }


        yield return new WaitForSeconds(shootTime);

        StartCoroutine(CO_Shoot());

    }

    void ToggleIsShooting() => isShooting = !isShooting;
}
