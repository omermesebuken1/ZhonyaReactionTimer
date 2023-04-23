using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject target;
    private void Update() 
    {

        transform.position = target.transform.position + offset;

    }
}
