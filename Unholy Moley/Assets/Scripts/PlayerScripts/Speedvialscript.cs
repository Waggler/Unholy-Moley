using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedvialscript : MonoBehaviour
{
    // Speed Boost Stats
    [Header("Stat Increase")]
    public float boostAmount = 1.5f;
    public float duration = 3f;
    public float vialCount = 0f;

    /* Particle Effect Variable
     * [Header("Outside Objects")]
    public GameObject vialCloud;
    */

    // Checks whether the player comes in contact
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //vialCount += 1;
            Destroy(gameObject);
            //StartCoroutine (Boost(other));
        }
    }

    // I am SPEED

    /*Boost on Touch
     * IEnumerator Boost(Collider player)
    {
        //Instantiate(vialCloud, transform.position, transform.rotation);

        PlayerMovement stats = player.GetComponent<PlayerMovement>();// Get the Player's Speed
        stats.speed *= boostAmount;// Boost speed by boost amount

        GetComponent<MeshRenderer>().enabled = false;// Turns off Collider and Mesh Render
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);// Waits for Duration

        stats.speed /= boostAmount;// Revert boosted speed

        Destroy(gameObject);// Destroy Speedboost vial
    }*/

}
