using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour, IHittable {

    public string surfaceHitParticlesTag;

    public void OnHit(Vector3 position, Projectile projectile)
    {
        ObjectPoolManager.instance.SpawnFromPool(surfaceHitParticlesTag, position, Quaternion.identity);

    }

}
