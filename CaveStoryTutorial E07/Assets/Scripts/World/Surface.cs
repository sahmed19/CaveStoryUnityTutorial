using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour, IHittable {

    public GameObject surfaceHitParticles;

    public void OnHit(Vector3 position)
    {
        Instantiate(surfaceHitParticles, position, Quaternion.identity);
    }

}
