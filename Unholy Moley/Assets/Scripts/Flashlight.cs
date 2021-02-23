﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject sLight;
    public AudioSource aSource;
    public AudioClip on;
    public AudioClip off;

    public bool lightActive;


    // Flashlight ain't on at start
    void Start()
    {
        sLight.SetActive(false);
    }

    // Press F to turn on the flashlight
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lightActive = !lightActive;

            if (lightActive)
            {
                FlashlightOn();
            }

            if (!lightActive)
            {
                FlashlightOff();
            }
        }
    }

    // Turns flashlight on/off and plays button clip
    void FlashlightOn()
    {
        sLight.SetActive(true);
        aSource.PlayOneShot(on);
    }

    void FlashlightOff()
    {
        sLight.SetActive(false);
        aSource.PlayOneShot(off);

    }
}
