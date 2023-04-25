using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interstitial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IronSource.Agent.loadInterstitial();
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
    }

    // Invoked when the interstitial ad closed and the user goes back to the application screen.

    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo) {

        IronSource.Agent.loadInterstitial();
    }

    public void interstitialplay() 
    {
        IronSource.Agent.showInterstitial();
    }
}
