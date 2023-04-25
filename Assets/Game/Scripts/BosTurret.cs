using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosTurret : MonoBehaviour
{

    [SerializeField] private GameObject HitPoint;
    [SerializeField] private GameObject BulletPrefab;
    [HideInInspector] public bool turretShooted;


    public void turretShootButton()
    {
        if(!turretShooted)
        {
            Instantiate(BulletPrefab, HitPoint.transform.position, transform.rotation * Quaternion.Euler(0, 180, 0));
            turretShooted = true;
        }
        
    }

}
