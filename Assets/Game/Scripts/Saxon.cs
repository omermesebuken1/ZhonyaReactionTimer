using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Saxon : MonoBehaviour
{

    [Header("Canvas Objects")]


    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject zhonyaButtonGameObject;
    [SerializeField] private GameObject reviveButtonGameObject;
    [SerializeField] private GameObject fireButtonGameObject;

    [Header("Sounds")]


    [SerializeField] private AudioClip zhonyaSound;

    [Header("Scripts")]


    [SerializeField] private BosTurret bosTurret;

    [Header("Vectors")]

    [SerializeField] private Vector3 startPos;


    [HideInInspector] public bool zhonyaBool;
    [HideInInspector] public bool missed;
    private bool isDead;
    private float zhonyaTimer;
    private float ScoreTimer;
    private float Score = 100;
    private float highScore;
    private bool scoreBool;

    private int AdCounter;


    private void Start()
    {
        bosTurret.turretShooted = true;
        zhonyaButtonGameObject.SetActive(true);
        reviveButtonGameObject.SetActive(false);

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetFloat("Score", 100);
        }

        highScoreText.text = "";
        scoreText.text = "";
        fireButtonGameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }

    private void Update()
    {
        ZhonyaStop();
        ZhonyaFunc();

        ScoreFunc();

        ifMissed();

        if (!isDead)
        {
            transform.position = startPos;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

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
            scoreText.text = Score.ToString("F3");
            CheckHighScore();
            scoreBool = false;
            ScoreTimer = 0;
            Score = 100;
            reviveButtonGameObject.SetActive(true);

        }

    }

    private void ifMissed()
    {
        if (missed)
        {

            scoreText.text = "Missed";
            scoreBool = false;
            ScoreTimer = 0;
            zhonyaButtonGameObject.SetActive(true);

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
        ShowInterAd();


    }

    IEnumerator Wait(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

    }


    public void ZhonyaButton()
    {
        
        fireButtonGameObject.GetComponent<Image>().color = new Color(1, 1, 1);
        highScoreText.text = "";
        scoreText.text = "";
        zhonyaButtonGameObject.SetActive(false);

        if (!zhonyaBool)
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
        if (zhonyaBool)
        {
            zhonyaTimer += Time.deltaTime;
        }

        if (zhonyaTimer > 2.5f)
        {
            scoreBool = true;
            zhonyaBool = false;
            zhonyaTimer = 0;
        }

        if (bosTurret.turretShooted)
        {
            fireButtonGameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    private void ScoreFunc()
    {
        if (scoreBool)
        {
            ScoreTimer += Time.deltaTime;
        }


    }

    private void CheckHighScore()
    {

           
            if (PlayerPrefs.HasKey("Score"))
            {

                highScore = PlayerPrefs.GetFloat("Score");

                if (Score < highScore)
                {
                    highScore = Score;
                    PlayerPrefs.SetFloat("Score", highScore);
                    highScoreText.text = "New High Score!";
                    KTGameCenter.SharedCenter().SubmitFloatScore(highScore, 3, "Reaction Time");
                    
                }


            }


        



    }

    private void ShowInterAd()
    {
        AdCounter++;

        if(AdCounter > 2)
        {
            FindObjectOfType<Interstitial>().interstitialplay();
            AdCounter = 0;
        }
    }

}
