using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUnlock : MonoBehaviour
{
    // Gives control back to the player
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}