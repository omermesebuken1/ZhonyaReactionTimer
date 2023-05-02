using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializeads : MonoBehaviour
{

    public string appKey;

    // Start is called before the first frame update

    private void Awake()
    {
        IronSource.Agent.init(appKey);
    }

    void Start()
    {
        Loadbanner();
        IronSource.Agent.loadInterstitial();

        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
        IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void Loadbanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo) 
    {
        IronSource.Agent.loadInterstitial();
    }

    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo) 
    {
        //ready
    }

    public void interstitialplay() 
    {  
        
        if (IronSource.Agent.isInterstitialReady()) 
        {
			IronSource.Agent.showInterstitial ();
		} 
       

    }
}
