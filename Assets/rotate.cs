using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 5.0f, 0, Space.Self);
        transform.Translate(Vector3.up * (float)System.Math.Sin((double)Time.frameCount)*20.0f, Space.Self);
    }
}
