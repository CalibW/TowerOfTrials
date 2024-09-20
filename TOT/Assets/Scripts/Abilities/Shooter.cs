using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //setting variables and linking game objects as well as the camera and other scripts to this script.
    public Camera cam;
    public GameObject projectile;
    public Transform LHFirePoint, RHFirePoint;
    public float projectileSpeed = 30;
    public float fireRate = 4;
    public float arcRange = 1;
    private Vector3 destination;
    private bool leftHand;
    public float ftime;
    [SerializeField] PlayerAttributes playeratm;
    [SerializeField] ManaBar mb;

    void Start()
    {
        // Sets the first shot to left hand
        leftHand = true;
    }

    // Update is called once per frame
    void Update()
    {
        ftime += Time.deltaTime;
        // Check mana and time to fire as well as if q is pressed
        if (Input.GetKey("q") && ftime >= fireRate && playeratm.Mana >= 10f)
        {
            ftime = 0;
            mb.loseShootMana(10);
            ShootProjectile();
        }
    }

    // function to shoot projectile
    void ShootProjectile()
    {
        // create ray from center of cam view
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hits any object in scene
        if (Physics.Raycast(ray, out hit))
            destination = hit.point; //if ray hits something aim at that point
        else
            destination = ray.GetPoint(1000); // if not hit aim far into distance
        //alternate firting between left and right hand by checking if the lefthadn is flase or true
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

    //function to instantiate the porjectile and fire the projectile.
    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2));
    }
}