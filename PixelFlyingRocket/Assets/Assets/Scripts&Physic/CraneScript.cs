using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneScript : MonoBehaviour
{
    public Vector3 StartPos;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CraneMove()
    {
        if(transform.position.x < -8f)
        {
            
        }
        else
        {
            transform.Translate(Vector3.left * 2 * Time.deltaTime);
        }
    }
}
