using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Collider collider;

    private void Start()
    {
        collider = gameObject.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.other.GetComponent<PlayerController>();

        if (player != null)
        {
            player?.Die();
        }
    }
}
