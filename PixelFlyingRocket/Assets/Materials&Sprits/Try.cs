using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Try : MonoBehaviour
{
    private MoveUp moveUpScript;
    public GameObject[] Rockets;

    void Start()
    {
        moveUpScript = GetComponentInChildren<MoveUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveUpScript.isPlaying && Rockets[0].gameObject.activeInHierarchy)
        {
            print("work1");
        }
        if(moveUpScript.isPlaying && Rockets[1].gameObject.activeInHierarchy)
        {
            print("work2");
        }
    }
}
