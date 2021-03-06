using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Arm;
    public GameObject FlashLight;
    public GameObject Arm2;
    public GameObject Vial;
    public Animator Anim;

    public bool hasGun;
    public bool isSafe = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killbox"))
        {
            SceneManager.LoadScene("Lose Screen");
        }
        if (other.CompareTag("GunPickup"))
        {
            Arm.GetComponent<SkinnedMeshRenderer>().enabled = false;
            FlashLight.GetComponent<MeshRenderer>().enabled = false;
            Arm2.GetComponent<SkinnedMeshRenderer>().enabled = false;
            Vial.GetComponent<MeshRenderer>().enabled = false;
            Gun.SetActive(true);
            hasGun = true;
            Anim.SetBool("GunHeld", true);
        }
        if (other.CompareTag("SafeZone") && isSafe == false)
        {
            isSafe = true;
        }
        if (other.CompareTag("DangerZone") && isSafe == true)
        {
            isSafe = false;
        }
    }
}