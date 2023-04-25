using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private float InputX;
    private float InputY;

    private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private float timer_Q;
    private bool isSkillQ;

    [SerializeField] private float slowSpeed;
    [SerializeField] private float fastSpeed;

    private bool isDead;

    private void Start()
    {

        moveSpeed = slowSpeed;

    }

    private void Update()
    {
        ZhonyaStop();
        
        if(Input.GetKey(KeyCode.Space) != true && !isDead)
        {
            Walk();
            Skill();
        }

        Revive();

    }


    private void Walk()
    {
        InputX = Input.GetAxis("Horizontal");
        InputY = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(InputX, 0, InputY);
        movementDirection.Normalize();
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
        if (movementDirection != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("isWalking", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
        }

    }

    private void ZhonyaStop()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().enabled = false;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<Animator>().enabled = true;  
        }
    }

    private void OnCollisionEnter(Collision other) {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("isDead");
           
        }

    }

    private void Skill()
    {

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            timer_Q = 0;
            isSkillQ = true;
            moveSpeed = fastSpeed;
            GetComponent<Animator>().SetTrigger("Q");
            
        }

        if(isSkillQ)
        {
            timer_Q += Time.deltaTime;
        }

        if(timer_Q > 0.3f)
        {
            moveSpeed = slowSpeed;
            isSkillQ = false;
        }

    }

    private void Revive()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            isDead = false;
            FindAnyObjectByType<Turret>().playerIsDead = false;
            transform.position = new Vector3(0,0,-13);
            GetComponent<Animator>().SetTrigger("Revive");
        }
        
    }
}
