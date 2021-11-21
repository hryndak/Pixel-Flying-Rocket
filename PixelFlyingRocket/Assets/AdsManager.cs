using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    public Text MoneyText;
    public Buttons ButtonsScript;
    public Achievments AchievementsScript;
    public Button RewardButton;
    public Button PowerUpButton;
    public int MoneyToReward;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("4217183");
        Advertisement.AddListener(this);
    }
    void Update()
    {
        MoneyText.text = MoneyToReward.ToString();
        RewardControler();
        if (ButtonsScript.HavePowerUp)
        {
            PowerUpButton.gameObject.SetActive(false);
        }
        else
        {
            PowerUpButton.gameObject.SetActive(true);
        }

        if (AchievementsScript.AchievmentsList[4] == true)
        {
            PowerUpButton.gameObject.SetActive(false);
            ButtonsScript.HavePowerUp = true;
        }
    }

    // Update is called once per frame
    public void PlayPowerUpAd()
    {
        if (Advertisement.IsReady("Power_Up"))
        {
            Advertisement.Show("Power_Up");
        }
        else
        {
            Debug.Log("Rewarded ad not ready | Power_Up");
        }
    }
    public void RewardedAd()
    {
        if (Advertisement.IsReady("Money_Reward"))
        {
            Advertisement.Show("Money_Reward");
        }
        else
        {
            Debug.Log("Rewarded ad not ready| Money_Reward");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log("AD is ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error! " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Video started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "Power_Up" && showResult == ShowResult.Finished)
        {
            ButtonsScript.HavePowerUp = true;
            PowerUpButton.gameObject.SetActive(false);
            AchievementsScript.AchievmentsList[4] = true;
        }
        if (placementId == "Money_Reward" && showResult == ShowResult.Finished)
        {
            ButtonsScript.Money += MoneyToReward;
        }
    }

    public void RewardControler()
    {
        if (ButtonsScript.CurrentRocket == 0)
        {
            MoneyToReward = 1200;
        }
        if (ButtonsScript.CurrentRocket == 1)
        {
            MoneyToReward = 3500;
        }
        if (ButtonsScript.CurrentRocket == 2)
        {
            MoneyToReward = 5500;
        }
        if (ButtonsScript.CurrentRocket == 3)
        {
            MoneyToReward = 10000;
        }
        if (ButtonsScript.CurrentRocket == 4)
        {
            MoneyToReward = 30000;
        }
        if (ButtonsScript.CurrentRocket == 5)
        {
            MoneyToReward = 50000;
        }

    }
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
