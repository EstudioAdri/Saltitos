using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] Transform projectileTarget, projectileSpawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (projectileSpawnPoint == null)
            {
                Debug.Log("No bullet spawnpoint set!");
                return;
            }
            
            Projectile projectileInstance = Instantiate(projectile);
            projectileInstance.transform.position = projectileSpawnPoint.transform.position;
            projectileInstance.Target = projectileTarget;
        }
    }
}
