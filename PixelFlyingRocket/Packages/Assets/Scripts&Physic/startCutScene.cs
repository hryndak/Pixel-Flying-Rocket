using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startCutScene : MonoBehaviour
{
    public Animator camAnim;

    public bool cutScenePlayed;

    public Buttons ButtonsScript;

    public Achievments AchievmentsScript;

    public Text endText;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "lRocket")
        {
            Time.timeScale = 1f;
            if (Time.timeScale == 1f && !cutScenePlayed)
            {
                PlayWinSound();
                camAnim.SetBool("cutScene1", true);
                Invoke(nameof(stopCutScene), 5f);
                cutScenePlayed = true;
                AchievmentsScript.AchievmentsList[2] = true;
            }
        }
    }

    void stopCutScene()
    {
        camAnim.SetBool("cutScene1", false);
        endText.gameObject.SetActive(false);
    }

    void PlayWinSound()
    {
        ButtonsScript.WinSource.PlayOneShot(ButtonsScript.WinSound, 0.5f);
        endText.gameObject.SetActive(true);
    }

    public void CutSave()
    {
        PlayerPrefsX.SetBool("CutScene", cutScenePlayed);
    }

    public void CutLoad()
    {
        cutScenePlayed = PlayerPrefsX.GetBool("CutScene");
    }
}
