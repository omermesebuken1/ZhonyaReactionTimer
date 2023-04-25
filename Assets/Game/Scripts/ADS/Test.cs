using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IronSource.Agent.setMetaData("is_test_suite", "enable"); 
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
private void SdkInitializationCompletedEvent(){
    
    //Launch test suite
    IronSource.Agent.launchTestSuite();
    IronSource.Agent.validateIntegration();
}



}
