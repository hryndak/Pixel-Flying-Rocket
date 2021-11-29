using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float x = Random.Range(1f,-1f);
            float y = Random.Range(1f,-1f);

            transform.position = new Vector2(x,y);
        }
    }
}
