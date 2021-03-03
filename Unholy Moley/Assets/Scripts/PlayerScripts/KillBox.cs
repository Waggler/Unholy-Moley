using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    public GameObject Gun;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killbox"))
        {
            SceneManager.LoadScene("Lose Screen");
            gameObject.SetActive(false);
        }
        if (other.CompareTag("GunPickup"))
        {
            Gun.SetActive(true);
        }
    }
}
