using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 50f;
    public bool stun = false;

    private float stunTimer;

    private IEnumerator coRoutine;
    private void Update()
    {
        stunTimer = GetComponent<MoleScript>().stunDuration;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    public void GetStun(bool stunned)
    {
        coRoutine = Stunned(stunned);
        StartCoroutine(coRoutine);
    }
    void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator Stunned(bool stunned)
    {
        Debug.Log("Run?");
        stun = stunned;
        yield return new WaitForSeconds(stunTimer);// Waits for Duration
        stun = false;
    }
}

