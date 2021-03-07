using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    [Header("LightSetting")]
    public GameObject sLight;
    public bool lightActive;

    [Header("Audio")]
    public AudioSource aSource;
    public AudioClip on;
    public AudioClip off;

    public bool equipGun;

    public GameObject killBox;
    public Animator animator;

    

    // Flashlight ain't on at start
    void Start()
    {
        sLight.SetActive(false);
        
    }

    // Press F to turn on the flashlight
    void Update()
    {
        equipGun = killBox.GetComponent<KillBox>().hasGun;
        if (equipGun == true && PauseScript.gamePaused == false)
        {
            lightActive = false;
            FlashlightOn();
        }
        else if (Input.GetKeyDown(KeyCode.F) && PauseScript.gamePaused == false)
        {
            FlashlightOn();
            aSource.PlayOneShot(on);
            lightActive = true;
            animator.SetTrigger("PressButton");
            animator.SetBool("FlashLightOn", true);

        }
        else if (Input.GetKeyUp(KeyCode.F) && PauseScript.gamePaused == false )
        {
            FlashlightOff();
            aSource.PlayOneShot(off);
            lightActive = false;
            animator.SetTrigger("PressButton");
            animator.SetBool("FlashLightOn", false);
        }

        if (Input.GetKey(KeyCode.P))
        {
            lightActive = false;
            FlashlightOff();
        }

    }

    // Turns flashlight on/off and plays button clip
    void FlashlightOn()
    {
        sLight.SetActive(true);
    }

    void FlashlightOff()
    {
        sLight.SetActive(false);

    }
}
