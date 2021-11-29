using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject[] RocketList;
    public int CurrentRocket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ComparePrefs();
        DecomaprePrefs();
    }

    private void ComparePrefs()
    {
        if(RocketList[0].gameObject.activeInHierarchy)
        {
            CurrentRocket = 0;
        }
        if(RocketList[1].gameObject.activeInHierarchy)
        {
            CurrentRocket = 1;
        }
        if(RocketList[2].gameObject.activeInHierarchy)
        {
            CurrentRocket = 2;
        }
        if(RocketList[3].gameObject.activeInHierarchy)
        {
            CurrentRocket = 3;
        }
        if(RocketList[4].gameObject.activeInHierarchy)
        {
            CurrentRocket = 4;
        }
        if(RocketList[5].gameObject.activeInHierarchy)
        {
            CurrentRocket = 5;
        }

    }
    private void DecomaprePrefs()
    {
        if(CurrentRocket == 0)
        {
            RocketList[0].gameObject.SetActive(true);
        }
        if(CurrentRocket == 1)
        {
            RocketList[1].gameObject.SetActive(true);
        }
        if(CurrentRocket == 2)
        {
            RocketList[2].gameObject.SetActive(true);
        }
        if(CurrentRocket == 3)
        {
            RocketList[3].gameObject.SetActive(true);
        }
        if(CurrentRocket == 4)
        {
            RocketList[4].gameObject.SetActive(true);
        }
        if(CurrentRocket == 5)
        {
            RocketList[5].gameObject.SetActive(true);
        }
    }
}
