using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject RetryButton;
    [SerializeField] private GameObject NoAdsButton;
    [SerializeField] private GameObject settings_ALL;
    [SerializeField] private GameObject taptoexitscreen;
    [SerializeField] private GameObject PP;
    [SerializeField] private GameObject TOU;
    
    private void Start() {

        NoAdsControl();
        
    }

    private void Update() {
        
        NoAdsButtonStatus();

    }

   

  
    public void ReloadLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Settings_Func()
    {
        if (PlayerPrefs.HasKey("Vibration"))
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                //Haptic.GetComponent<iOSHapticController>().TriggerImpactMedium();
            }
        }

        if (FindObjectOfType<settingsAnim>().status == false)
        {
            settings_ALL.GetComponent<Animator>().SetTrigger("open");
            taptoexitscreen.SetActive(true);
           
        }
        if (FindObjectOfType<settingsAnim>().status == true)
        {
            settings_ALL.GetComponent<Animator>().SetTrigger("close");
            taptoexitscreen.SetActive(false);
            PP.SetActive(false);
            TOU.SetActive(false);
            
        }
    }

    public void TapToExit()
    {
        if (PlayerPrefs.HasKey("Vibration"))
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                //Haptic.GetComponent<iOSHapticController>().TriggerImpactMedium();
            }
        }

        if (FindObjectOfType<settingsAnim>().status == true)
        {
            settings_ALL.GetComponent<Animator>().SetTrigger("close");
            taptoexitscreen.SetActive(false);
        }

        PP.SetActive(false);
        TOU.SetActive(false);
    }

    public void ShowPPandTOU()
    {
        PP.SetActive(true);
        TOU.SetActive(true);


    }

    public void privacy()
    {
        Application.OpenURL("https://omermesebuken.com/mobile-games/zhonya-reaction/privacy-policy");
    }

    public void Terms()
    {
        Application.OpenURL("https://omermesebuken.com/mobile-games/zhonya-reaction/terms-of-use");
    }

    public void NoAdsControl()
    {

        if (PlayerPrefs.HasKey("NoAds") == false)
        {

            PlayerPrefs.SetInt("NoAds", 0);

        }
    }

    public void openGameCenter()
    {
        KTGameCenter.SharedCenter().ShowLeaderboard("grp.ReactionTime");
    }

    private void NoAdsButtonStatus()
    {
        if(PlayerPrefs.HasKey("NoAds"))
        {
            if(PlayerPrefs.GetInt("NoAds") == 1)
            {
                NoAdsButton.SetActive(false);
            }

        }
    }

    public void NoAdsPurchased()
    {
        PlayerPrefs.SetInt("NoAds", 1);
    }

}



