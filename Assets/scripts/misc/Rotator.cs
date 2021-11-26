using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField, Range(0, 90)]
    private float rotateAmount = 1f;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(transform.up, rotateAmount * Time.deltaTime);
    }
}
