using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject Gun;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killbox"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("GunPickup"))
        {
            Gun.SetActive(true);
        }
    }
}
