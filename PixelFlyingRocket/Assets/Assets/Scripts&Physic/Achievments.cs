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
    public Animator animator;
    public GameObject Ach1;
    private bool onetime;
    void Start()
    {
        AchievmentsList = PlayerPrefsX.GetBoolArray("Achievements");
    }
    void OnApplicationQuit()
    {
        PlayerPrefsX.SetBoolArray("Achievements", AchievmentsList);
    }
    // Update is called once per frame
    void Update()
    {
        if (AchievmentsList[0] == true)
        {
            AchText[0].color = Color.green;
            achFalse[0].gameObject.SetActive(false);
            achTrue[0].gameObject.SetActive(true);
            Ach1.gameObject.SetActive(false);

            if (!onetime)
            {
                animator.SetTrigger("isReached");
                onetime = true;
            }

        }
        if (AchievmentsList[0] == false)
        {
            AchText[0].color = Color.red;
            achFalse[0].gameObject.SetActive(true);
            achTrue[0].gameObject.SetActive(false);
        }
        if (AchievmentsList[1] == true)
        {
            AchText[1].color = Color.green;
            achFalse[1].gameObject.SetActive(false);
            achTrue[1].gameObject.SetActive(true);
        }
        else
        {
            AchText[1].color = Color.red;
            achFalse[1].gameObject.SetActive(true);
            achTrue[1].gameObject.SetActive(false);
        }
        if (AchievmentsList[2] == true)
        {
            AchText[2].color = Color.green;
            achFalse[2].gameObject.SetActive(false);
            achTrue[2].gameObject.SetActive(true);
        }
        else
        {
            AchText[2].color = Color.red;
            achFalse[2].gameObject.SetActive(true);
            achTrue[2].gameObject.SetActive(false);
        }
        if (AchievmentsList[3] == true)
        {
            AchText[3].color = Color.green;
            achFalse[3].gameObject.SetActive(false);
            achTrue[3].gameObject.SetActive(true);
        }
        else
        {
            AchText[3].color = Color.red;
            achFalse[3].gameObject.SetActive(true);
            achTrue[3].gameObject.SetActive(false);
        }
        if (AchievmentsList[4] == true)
        {
            AchText[4].color = Color.green;
            achFalse[4].gameObject.SetActive(false);
            achTrue[4].gameObject.SetActive(true);
        }
        else
        {
            AchText[4].color = Color.red;
            achFalse[4].gameObject.SetActive(true);
            achTrue[4].gameObject.SetActive(false);
        }
    }

}
