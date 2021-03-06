using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkVial : MonoBehaviour
{
    // Speed Boost Stats
    [Header("Stat Increase")]
    public float boostAmount = 1.5f;
    public float duration = 3f;
    public float vialCount = 0f;

    public bool equipGun;

    public GameObject killBox;
    public Animator animator;
    public GameObject SerumArm;
    public GameObject SerumArmMesh;
    public GameObject VialMesh;


    [Header("Audio")]
    public AudioClip DrinkSound;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        equipGun = killBox.GetComponent<KillBox>().hasGun;
        if (vialCount > 0)
        {
            SerumArm.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Q) && vialCount > 0 && equipGun == false)
        {
            audioSource.PlayOneShot(DrinkSound);
            animator.SetFloat("Serums", 1f);
            //animator.SetBool("Chugging", true);
            animator.SetTrigger("Chuggin");
            StartCoroutine(AnimShow());
            StartCoroutine(Boost());
            vialCount -= 1;
            

        }
        animator.SetFloat("Serums", 0f);
        //animator.SetBool("Chugging", false);
    }
    IEnumerator Boost()
    {
        //Instantiate(vialCloud, transform.position, transform.rotation);

        PlayerMovement stats = GetComponent<PlayerMovement>();// Get the Player's Speed
        stats.speed *= boostAmount;// Boost speed by boost amount

        //GetComponent<MeshRenderer>().enabled = false;// Turns off Collider and Mesh Render
        //GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);// Waits for Duration

        stats.speed /= boostAmount;// Revert boosted speed
        SerumArmMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
        VialMesh.GetComponent<MeshRenderer>().enabled = true;
        SerumArm.gameObject.SetActive(false);

        //Destroy(gameObject);// Destroy Speedboost vial
    }
    IEnumerator AnimShow()
    {
        yield return new WaitForSeconds(1);
        VialMesh.GetComponent<MeshRenderer>().enabled = false;
        SerumArmMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vial") && vialCount < 1)
        {
            vialCount += 1;
            Destroy(other.gameObject);
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