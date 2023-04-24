using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VayneBot : MonoBehaviour
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

    private float walkTimer;
    private float walkRandomCooldown;

    private bool goto0;

    private void Start()
    {

        moveSpeed = slowSpeed;
        walkRandomCooldown = Random.Range(0,1.5f);
        ChangeDirection();
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

        walkTimer += Time.deltaTime;

        if(walkTimer > walkRandomCooldown)
        {
            StopWalk();
            ChangeDirection();
            walkRandomCooldown = Random.Range(0,1.5f);
            walkTimer = 0;

        }


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

    private void StopWalk()
    {
        int stopPosibility = Random.Range(1,10);
        int stopRand = Random.Range(1,10);

        if(stopRand > stopPosibility)
        {
            InputX = 0;
            InputY = 0;
            float waitTime = Random.Range(0,0.4f);
            Wait(waitTime);
        }

    }

        private void ChangeDirection()
        {
            InputX = Random.Range(-1,2);
            InputY = Random.Range(-1,2);

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

        
        if (other.gameObject.CompareTag("Wall"))
        {
            goto0 = true;
            InputX = -InputX;
            InputY = -InputY;
            walkTimer = 0;
            print("wall");
           
        }


    }

    

    private void GotoMerkez()
    {
        if(goto0)
        {
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(0,0,20),moveSpeed*Time.deltaTime);
        }

        if(transform.position.x < 1 && transform.position.y < 1)
        {
            goto0 = false;
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Wall"))
        {
            InputX = -InputX;
            InputY = -InputY;
            walkTimer = 0;
            print("wall");
           
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

    IEnumerator Wait(float waitTime){

        yield return new WaitForSeconds(waitTime);

    }



}
