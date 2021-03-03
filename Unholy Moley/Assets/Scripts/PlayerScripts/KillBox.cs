using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    public GameObject Gun;

    public bool hasGun;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killbox"))
        {
            SceneManager.LoadScene("Lose Screen");
        }
        if (other.CompareTag("GunPickup"))
        {
            Gun.SetActive(true);
            hasGun = true;
        }
    }
}
