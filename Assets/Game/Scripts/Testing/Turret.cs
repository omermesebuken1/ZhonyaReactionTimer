using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject TurretUst;
    [SerializeField] private GameObject TurretAlt;
    [SerializeField] private GameObject HitPoint;

    private float timer;
    [SerializeField] private float bulletCooldown;

    [SerializeField] private bool fire;

    private Vector3 lookPosition;
    private Vector3 laserPosition;

    public bool playerIsDead;

    public bool turretShooted;

    private void Update()
    {

        TurretLook();
        //TurretUst.transform.LookAt(Target.transform);

        //TurretUst.transform.rotation *= Quaternion.Euler(rotationOptimazer,180,0);
        TurretShot();
    }


    private void TurretLook() // bakma
    {

        lookPosition = Target.transform.position;
        lookPosition.y = TurretUst.transform.position.y;
        laserPosition = lookPosition;
        laserPosition.y = HitPoint.transform.position.y;
        //drawLineToTarget();
        TurretUst.transform.LookAt(lookPosition);
        TurretUst.transform.rotation *= Quaternion.Euler(0, 180, 0);

    }

    private void drawLineToTarget()
    {
        GetComponent<LineRenderer>().SetPosition(0, HitPoint.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, laserPosition);
        GetComponent<LineRenderer>().startWidth = 0.09f;
    }

    private void TurretShot() // ateş etme
    {

        if (timer >= bulletCooldown && fire && !playerIsDead)
        {

            //Instantiate(turretSound);
            Instantiate(BulletPrefab, HitPoint.transform.position, TurretUst.transform.rotation * Quaternion.Euler(0, 180, 0));
            timer = 0;


        }
        else
        {
            timer += Time.deltaTime;
        }



    }

    public void turretShootButton()
    {
        if(!turretShooted)
        {
            Instantiate(BulletPrefab, HitPoint.transform.position, TurretUst.transform.rotation * Quaternion.Euler(0, 180, 0));
            turretShooted = true;
        }
        
    }
}
