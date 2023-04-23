using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zhonya : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Material[] metarials;

    private void Start() {

        GetComponent<SkinnedMeshRenderer>().material = metarials[0];
    }

    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            boxCollider.enabled = false;
            GetComponent<SkinnedMeshRenderer>().material = metarials[1];
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            boxCollider.enabled = true;
            GetComponent<SkinnedMeshRenderer>().material = metarials[0];
        }

        
    }
}
