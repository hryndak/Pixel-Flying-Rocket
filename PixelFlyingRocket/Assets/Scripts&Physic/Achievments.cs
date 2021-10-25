using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievments : MonoBehaviour
{
    public bool[] AchievmentsList = new bool[6];
    public Text[] AchText;
    public GameObject[] achFalse;
    public GameObject[] achTrue;
    void Start()
    {
        achFalse[0].gameObject.SetActive(true);
        achTrue[0].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (AchievmentsList[0] == true)
        {
            AchText[0].color = Color.green;
            achFalse[0].gameObject.SetActive(false);
            achTrue[0].gameObject.SetActive(true);
        }

        if (AchievmentsList[1] == true)
        {
            AchText[1].color = Color.green;
            achFalse[1].gameObject.SetActive(false);
            achTrue[1].gameObject.SetActive(true);
        }
    }
}
