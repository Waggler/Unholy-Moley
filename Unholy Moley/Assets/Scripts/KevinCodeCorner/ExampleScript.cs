using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    [Header("Health Settings")]// A header creates a grouping in Editor for the things under it.
    public int health = 0;
    public int maxHealth = 100;// a public variable is shown in the Editor and is accessible by other scripts
    private int lives;// a private variable is NOT shown in the Editor and is NOT accessible by other scripts

    [Header("Shield Settings")]
    public int shield = 0;
    public int maxShield = 0;

    [Header("Movement Settings")]
    [SerializeField] private float currentSpeed;// a SerialeField private variable is shown in the Editor BUT IS NOT accessible by other scripts
    private float maxSpeed;
    private float jumpHeight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }
    void LateUpdate()
    {

    }
}