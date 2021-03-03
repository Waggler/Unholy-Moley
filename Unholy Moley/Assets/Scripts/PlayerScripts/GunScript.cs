using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public bool equipGun;
    private float nextTimeToFire = 0f;
    public Camera fpsCam;

    public GameObject killBox;
    void Update()
    {
        equipGun = killBox.GetComponent<KillBox>().hasGun;
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && equipGun == true)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}
