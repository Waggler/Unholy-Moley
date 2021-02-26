using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedvialscript : MonoBehaviour
{
    [Header("Stat Increase")]

    public float boostAmount = 1.5f;

    [Header("Outside Objects")]

    public GameObject vialCloud;

    // Checks whether the player comes in contact
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Boost(other);
        }
    }

    // I am SPEED
    void Boost(Collider player)
    {
        Instantiate(vialCloud, transform.position, transform.rotation);

        

        Destroy(gameObject);
    }

}
