using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zhonya : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Material[] metarials;

    [SerializeField] private Saxon saxon;

    private void Start() {

        GetComponent<SkinnedMeshRenderer>().material = metarials[0];
    }

    private void Update() {
        
        if(saxon.zhonyaBool)
        {
            boxCollider.enabled = false;
            GetComponent<SkinnedMeshRenderer>().material = metarials[1];
        }
        if(!saxon.zhonyaBool)
        {
            boxCollider.enabled = true;
            GetComponent<SkinnedMeshRenderer>().material = metarials[0];
        }

        
    }
}
