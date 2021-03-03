using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightStun : MonoBehaviour
{
    // Start is called before the first frame update

    //public float damage = 10f;
    public float range = 20f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public bool stunned = false;
    public float lightRadius;


    private float nextTimeToFire = 0f;
    public Camera fpsCam;

    public float LookPercentage;

    public GameObject Mole;

    RaycastHit hit;

    public float threshold = 0.80f;

    /*
    bool CanSeeMonster(GameObject target)
    {
        float heightOfPlayer = 1.5f;

        Vector3 startVec = transform.position;
        startVec.y += heightOfPlayer;
        Vector3 startVecFwd = transform.forward;
        startVecFwd.y += heightOfPlayer;

        RaycastHit hit;
        Vector3 rayDirection = target.transform.position - startVec;

        // If the ObjectToSee is close to this object and is in front of it, then return true
        if ((Vector3.Angle(rayDirection, startVecFwd)) < 110 &&
            (Vector3.Distance(startVec, target.transform.position) <= 20f))
        {
            Debug.Log("close");
            return true;
        }
        if ((Vector3.Angle(rayDirection, startVecFwd)) < 90 &&
            Physics.Raycast(startVec, rayDirection, out hit, 100f))
        { // Detect if player is within the field of view

            if (hit.collider.gameObject == target)
            {
                Debug.Log("Can see player");
                return true;
            }
            else
            {
                Debug.Log("Can not see player");
                return false;
            }
        }
        return false;
    }
    */
    // Update is called once per frame
    void Update()
    {
        //var ray = fpsCam.ScreenPointToRay(Input.mousePosition);

        //LookPercentage.ToString("F3");
        if (Input.GetKeyDown("r") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    /*
     * void Shoot(Ray ray)
    {

        var vector1 = ray.direction;
        var vector2 = Mole.transform.position - ray.origin;
        var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (lookPercentage > threshold)
            {
                target.GetStun(stunned);
            }

        }
    }
    */

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.SphereCast(fpsCam.transform.position, lightRadius, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.GetStun(stunned = true);
            }
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(fpsCam.transform.position, lightRadius);
    }
}
