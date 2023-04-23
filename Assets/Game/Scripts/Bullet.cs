using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    [SerializeField] private float bulletlife;

    private float bulletTimer;

    private Rigidbody rb;




    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed);

    }


    private void OnCollisionEnter(Collision other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            FindAnyObjectByType<Turret>().playerIsDead = true;
            Destroy(this.gameObject);
        }


    }

    private void Update()
    {

        bulletTimer += Time.deltaTime;


        if (bulletTimer > bulletlife)
        {
            Destroy(this.gameObject);
        }

    }
}
