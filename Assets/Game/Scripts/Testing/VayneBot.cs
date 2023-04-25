using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VayneBot : MonoBehaviour
{


    public float InputX;
    public float InputY;
    private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private float timer_Q;
    private bool isSkillQ;

    [SerializeField] private float slowSpeed;
    [SerializeField] private float fastSpeed;

    private bool isDead;

    private float walkTimer;
    private float walkRandomCooldown;

    public bool goto0;

    private Vector3 movementDirection;

    [SerializeField] private float SphereRadius;

    [SerializeField] private float merkezKacmaPayi;
    private float merkezGidisKacmaPayi;

    [SerializeField] private float merkezGidisRand;

    [SerializeField] private LayerMask playerLayer;

    private float skillTimer;
    private float skillCooldownRand;

    public bool zhonyaBool;

    private float zhonyaTimer;

    [SerializeField] private AudioClip zhonyaSound;

    [SerializeField] private BosTurret bosTurret;


    [SerializeField] private bool isMove;

    private float ScoreTimer;
    private float Score = 100;
    [SerializeField] private TextMeshProUGUI scoreText;

    private bool scoreBool;

    [SerializeField] private TextMeshProUGUI highScoreText;

    public bool missed;

    [SerializeField] private GameObject zhonyaButtonGameObject;
    [SerializeField] private GameObject reviveButtonGameObject;

    [SerializeField] private GameObject fireButtonGameObject;

    [SerializeField] private Vector3 startPos;

    private void Start()
    {   
        bosTurret.turretShooted = true;
        zhonyaButtonGameObject.SetActive(true);
        reviveButtonGameObject.SetActive(false);
        //moveSpeed = slowSpeed;
        // walkRandomCooldown = Random.Range(0, 1.5f);
        // skillCooldownRand = Random.Range(3,5);
        // ChangeDirection();

        if(!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetFloat("Score",100);
        }

        highScoreText.text = "";
        scoreText.text = "";
        fireButtonGameObject.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
    }

    private void Update()
    {
        ZhonyaStop();
        ZhonyaFunc();
        // CheckOutOfBounds();
        
        // if (zhonyaBool != true && !isDead && isMove)
        // {
            
        //     GotoMerkez();
        //     Walk();
        //     Skill();
        // }

        // if(!isMove)
        // {
        //     transform.position = new Vector3(1,0,20);
        //     transform.LookAt(new Vector3(0,0,0));
        //     GetComponent<Animator>().SetBool("isWalking", false);
            
        // }

        ScoreFunc();
        
        ifMissed();

        if(!isDead)
        {
            transform.position = startPos;
            transform.rotation = Quaternion.Euler(0,180,0);
        }

    }


    private void Walk()
    {

        walkTimer += Time.deltaTime;

        if (walkTimer > walkRandomCooldown)
        {
            StopWalk();
            ChangeDirection();
            walkRandomCooldown = Random.Range(0, 1.5f);
            walkTimer = 0;

        }


        if(!goto0)
        {
            movementDirection = new Vector3(InputX, 0, InputY);
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


        

    }

    private void StopWalk()
    {
        int stopPosibility = Random.Range(1, 10);
        int stopRand = Random.Range(1, 10);

        if (stopRand > stopPosibility)
        {
            InputX = 0;
            InputY = 0;
            float waitTime = Random.Range(0, 0.3f);
            Wait(waitTime);
        }

    }

    private void ChangeDirection()
    {
        InputX = Random.Range(-1f, 1f);
        InputY = Random.Range(-0.2f, 0.2f);

    }

    private void ZhonyaStop()
    {
        if (zhonyaBool)
        {
            GetComponent<Animator>().enabled = false;
        }
        if (!zhonyaBool)
        {
            GetComponent<Animator>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("isDead");
            Score = ScoreTimer;
            scoreText.text = Score.ToString("F5");
            CheckHighScore();
            scoreBool = false;
            ScoreTimer = 0;
            reviveButtonGameObject.SetActive(true);

        }

    }

    private void ifMissed()
    {
        if(missed)
        {
            
            scoreText.text = "Missed";
            scoreBool = false;
            ScoreTimer = 0;
            zhonyaButtonGameObject.SetActive(true);

        }
    }

    private void CheckOutOfBounds()
    {
        if (!Physics.CheckSphere(new Vector3(0, 0, 20), SphereRadius, playerLayer))
        {
            goto0 = true;
        }

    }

    private void GotoMerkez()
    {
        if (goto0)
        {
            
            movementDirection = new Vector3(0 - transform.position.x, 0, 20 - transform.position.z);
            movementDirection.Normalize();
            merkezGidisKacmaPayi = Random.Range(-merkezGidisRand,merkezGidisRand);
            transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3(0 + merkezGidisKacmaPayi, 0, 20 + merkezGidisKacmaPayi),
                                moveSpeed * Time.deltaTime);

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

            walkTimer = 0;
            InputX = 0;
            InputY = 0;        
            
        }

        if (transform.position.x <  0 + merkezKacmaPayi &&
            transform.position.x >  0 - merkezKacmaPayi)
        {
            
            walkTimer = 0;
            goto0 = false;
            ChangeDirection();
        }
    }

    private void Skill()
    {

        skillTimer += Time.deltaTime;

        if (skillTimer > skillCooldownRand)
        {
            timer_Q = 0;
            isSkillQ = true;
            moveSpeed = fastSpeed;
            GetComponent<Animator>().SetTrigger("Q");
            skillCooldownRand = Random.Range(2, 4);
            skillTimer = 0;

        }

        if (isSkillQ)
        {
            timer_Q += Time.deltaTime;
        }

        if (timer_Q > 0.3f)
        {
            moveSpeed = slowSpeed;
            isSkillQ = false;
        }

    }

    public void Revive()
    {

            isDead = false;
            //FindAnyObjectByType<Turret>().playerIsDead = false;
            
            GetComponent<Animator>().SetTrigger("Revive");
            reviveButtonGameObject.SetActive(false);
            zhonyaButtonGameObject.SetActive(true);
            transform.position = startPos;
            

    }

    IEnumerator Wait(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

    }


    public void ZhonyaButton()
    {
        fireButtonGameObject.GetComponent<Image>().color = new Color(1,1,1);
        highScoreText.text = "";
        scoreText.text = "";
        zhonyaButtonGameObject.SetActive(false);

        if(!zhonyaBool)
        {
            
            missed = false;
            ScoreTimer = 0;
            zhonyaBool = true;
            bosTurret.turretShooted = false;
            GetComponent<AudioSource>().PlayOneShot(zhonyaSound);
        }
        
        

    }

    private void ZhonyaFunc()
    {
        if(zhonyaBool)
        {
            zhonyaTimer += Time.deltaTime;
        }

        if(zhonyaTimer > 2.5f)
        {
            scoreBool = true;
            zhonyaBool  = false;
            zhonyaTimer = 0;
        }

        if(bosTurret.turretShooted)
        {
            fireButtonGameObject.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
        }
    }

    private void ScoreFunc()
    {
        if(scoreBool)
        {
            ScoreTimer += Time.deltaTime;
        }


    }

    private void CheckHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("Score");
        if(Score < highScore)
        {
            highScore = Score;
            PlayerPrefs.SetFloat("Score",highScore);
            highScoreText.text = "New High Score!";
        }
    }

}
