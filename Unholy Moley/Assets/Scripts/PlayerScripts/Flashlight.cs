﻿using System.Collections;
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
            lightActive = !lightActive;

            if (lightActive)
            {
                FlashlightOn();
                aSource.PlayOneShot(on);
            }

            if (!lightActive)
            {
                FlashlightOff();
                aSource.PlayOneShot(off);
            }
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
