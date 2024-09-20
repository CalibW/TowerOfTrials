using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public Transform LHFirePoint, RHFirePoint;
    public float projectileSpeed = 30;
    public float fireRate = 4;
    public float arcRange = 1;
    private Vector3 destination;
    private bool leftHand;
    public float ftime;
    public PlayerAttributes playeratm;
    public ManaBar mb;

    void Start()
    {
        // Initialize leftHand if needed
        leftHand = true;
    }

    // Update is called once per frame
    void Update()
    {
        ftime += Time.deltaTime;
        // Check mana and time to fire
        if (Input.GetKey("q") && ftime >= fireRate && playeratm.Mana >= 10f)
        {
            ftime = 0;
            mb.loseShootMana(10);  // Spend mana here
            ShootProjectile();
        }
        else if (Input.GetKey("q") && playeratm.Mana < 10f)
        {
            Debug.Log("Out of Mana to Shoot");
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        if (leftHand)
        {
            leftHand = false;
            InstantiateProjectile(LHFirePoint);
        }
        else
        {
            leftHand = true;
            InstantiateProjectile(RHFirePoint);
        }
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2));
    }
}