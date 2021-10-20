using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public virtual void Use(GameObject player)
    {
    }

    void OnTriggerEnter(Collider collider)
    {
            Use(collider.gameObject);
            Destroy(gameObject);
    }
}
