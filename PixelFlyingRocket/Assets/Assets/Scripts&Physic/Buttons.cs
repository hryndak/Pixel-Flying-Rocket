using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [Header("Scripts")]
    public CraneScript CraneScript;
    public MoveUp MoveUpScript;
    public Achievments AchievementsScript;
    private GameObject ObjectWithCraneScript;
    private GameObject ObjectWithScript;
    private GameObject ObjectWithScript2;
    private GameObject ObjectWithScript3;
    public CinemachineVirtualCamera vcam;
    public int CurrentRocket;

    public bool[] EngineUnlocked = new bool[6];
    public int[] EnginePowerChanger;
    public int Money;
    private int percentage;
    public int Power;

    [Header("Save")]



    [Header("UI")]
    public GameObject NoFuelPanel;
    public GameObject UtilitiesPanel;
    public GameObject AchievementsPanel;
    public GameObject ShopPage1;
    public GameObject ShopPage2;
    public GameObject ShopButton;
    public GameObject PlayButton;
    public GameObject RestartButton;
    public GameObject UtilitiesButton;
    public GameObject SettingsButton;
    public GameObject Smoke;
    public GameObject Ach1;
    public GameObject FasterButton;

    public TextMeshProUGUI HeightText;
    public TextMeshProUGUI FuelText;

    public GameObject[] RocketList;

    public GameObject[] BBPanel1;
    public GameObject[] TruePanel1;

    public GameObject[] BBPanel2;
    public GameObject[] TruePanel2;

    public Text[] PowerText;
    public Text[] BurnRateText;
    public Text[] MoneyText;

    public bool onetime;
    public bool onetime2;
    public bool HavePowerUp;

    void Start()
    {
        AchievementsPanel.gameObject.SetActive(false);
        NoFuelPanel.gameObject.SetActive(false);
        ShopPage1.gameObject.SetActive(false);
        ShopPage2.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        UtilitiesPanel.gameObject.SetActive(false);

        UtilitiesButton.gameObject.SetActive(true);
        ShopButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(true);
        Smoke.gameObject.SetActive(true);

        ObjectWithCraneScript = GameObject.FindWithTag("Crane");
        CraneScript = ObjectWithCraneScript.GetComponent<CraneScript>();
        Load();
    }
    void Update()
    {
        MoveUpScript = GetComponentInChildren<MoveUp>();
        AchievementsScript = GetComponentInChildren<Achievments>();
        CraneScript.CraneMove();
        PlayFuncion();
        SlowMotionFuncion();
        CameraFovChanger();

        DecomaprePrefs();
        ChangeColor();
        PercentCounter();
        LookAt();
        UIFunc();
        MoneyAch();
        BuyButtonsManager();
        AchievementsLoad();


        if (!onetime)
        {
            AddPower();
            onetime = true;
        }

        if (!onetime2)
        {
            ComparePrefs();
            onetime = true;
        }

    }
    void OnApplicationQuit()
    {
        Save();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("Rocket", CurrentRocket);
        PlayerPrefsX.SetBoolArray("Engine", EngineUnlocked);
        PlayerPrefsX.SetBoolArray("Achievements", AchievementsScript.AchievmentsList);
        PlayerPrefs.SetInt("Power", Power);
        PlayerPrefsX.SetBool("PowerUp", HavePowerUp);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        Money = PlayerPrefs.GetInt("Money", Money);
        CurrentRocket = PlayerPrefs.GetInt("Rocket", CurrentRocket);
        EngineUnlocked = PlayerPrefsX.GetBoolArray("Engine", false, 6);
        AchievementsScript.AchievmentsList = PlayerPrefsX.GetBoolArray("Achievements");
        HavePowerUp = PlayerPrefsX.GetBool("PowerUp", HavePowerUp);
        Power = PlayerPrefs.GetInt("Power", Power);
    }
    private void ChangeColor()
    {
        if (percentage >= 0)
        {
            FuelText.color = Color.red;
        }
        if (percentage >= 20)
        {
            FuelText.color = Color.yellow;
        }
        if (percentage >= 50)
        {
            FuelText.color = Color.green;
        }
    }
    public void PercentCounter()
    {
        percentage = (int)Mathf.Round((float)(MoveUpScript.SetFuel * MoveUpScript.FuelToAdd) / MoveUpScript.SetFuel);
        FuelText.text = "Fuel: " + percentage.ToString();

        HeightText.text = "Height: " + Mathf.RoundToInt(MoveUpScript.transform.position.y).ToString();
    }
    public void UIFunc()
    {

        PowerText[0].text = "Power: " + Power.ToString();
        PowerText[1].text = "Power: " + Power.ToString();

        BurnRateText[0].text = "Burn Rate: " + MoveUpScript.BurnRate.ToString();
        BurnRateText[1].text = "Burn Rate: " + MoveUpScript.BurnRate.ToString();

        MoneyText[0].text = Money.ToString();
        MoneyText[1].text = Money.ToString();
        MoneyText[2].text = Money.ToString();
    }
    public void PlayFuncion()
    {
        if (MoveUpScript.isPlaying == true)
        {
            PlayButton.gameObject.SetActive(false);
        }
        if (MoveUpScript.isPlaying == false)
        {
            PlayButton.gameObject.SetActive(true);
        }
    }
    public void OnPlayButtonClick()
    {
        MoveUpScript.isPlaying = true;
        PlayButton.gameObject.SetActive(false);
        UtilitiesButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(false);
        SettingsButton.gameObject.SetActive(false);

        if (HavePowerUp)
        {
            FasterButton.gameObject.SetActive(true);
        }
        else
        {
            FasterButton.gameObject.SetActive(false);
        }

    }
    public void SlowMotionFuncion()
    {
        if (MoveUpScript.isPaused)
        {
            NoFuelPanel.gameObject.SetActive(true);
            RestartButton.gameObject.SetActive(true);
            Time.timeScale = 0.2f * Time.fixedDeltaTime;
            DoSlowMotion();
        }
    }
    public void OnShopButtonClick()
    {
        MoveUpScript.isPlaying = false;
        ShopPage1.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(false);
    }
    public void OnNextPageClick()
    {
        ShopPage1.gameObject.SetActive(false);
        ShopPage2.gameObject.SetActive(true);
    }
    public void OnBackPageClick()
    {
        ShopPage1.gameObject.SetActive(true);
        ShopPage2.gameObject.SetActive(false);
    }
    public void OnExitShopClick()
    {
        ShopPage1.gameObject.SetActive(false);
        ShopPage2.gameObject.SetActive(false);
        UtilitiesPanel.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(true);
        AchievementsPanel.gameObject.SetActive(false);
    }
    public void OnRestartButtonCilck()
    {

        MoveUpScript.isPaused = false;
        MoneyFuncion();
        MoveUpScript.FuelToAdd = MoveUpScript.SetFuel;
        MoveUpScript.isPlaying = false;
        MoveUpScript.transform.position = MoveUpScript.StartTransfrom;
        MoveUpScript.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        CraneScript.transform.position = CraneScript.StartPos;
        NoFuelPanel.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        UtilitiesButton.gameObject.SetActive(true);
        SettingsButton.gameObject.SetActive(true);
        FasterButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(true);
        ShopButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = .02f;
        Save();

    }
    public void OnUtilitesButton()
    {
        UtilitiesPanel.gameObject.SetActive(true);

    }
    public void DoSlowMotion()
    {
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = 0.05f * .02f;
    }
    public void CameraFovChanger()
    {
        float b, c;
        b = MoveUpScript.GetComponent<Rigidbody2D>().velocity.y * 6;
        c = Mathf.Clamp(b, 90, 115);
        vcam.m_Lens.FieldOfView = c;
    }
    private void ComparePrefs()
    {
        if (RocketList[0].gameObject.activeInHierarchy)
        {
            CurrentRocket = 0;
        }
        if (RocketList[1].gameObject.activeInHierarchy)
        {
            CurrentRocket = 1;
        }
        if (RocketList[2].gameObject.activeInHierarchy)
        {
            CurrentRocket = 2;
        }
        if (RocketList[3].gameObject.activeInHierarchy)
        {
            CurrentRocket = 3;
        }
        if (RocketList[4].gameObject.activeInHierarchy)
        {
            CurrentRocket = 4;
        }
        if (RocketList[5].gameObject.activeInHierarchy)
        {
            CurrentRocket = 5;
        }
    }
    private void DecomaprePrefs()
    {
        if (CurrentRocket == 0)
        {
            RocketList[0].gameObject.SetActive(true);
            RocketList[1].gameObject.SetActive(false);
            RocketList[2].gameObject.SetActive(false);
            RocketList[3].gameObject.SetActive(false);
            RocketList[4].gameObject.SetActive(false);
            RocketList[5].gameObject.SetActive(false);

        }
        if (CurrentRocket == 1)
        {
            RocketList[0].gameObject.SetActive(false);
            RocketList[1].gameObject.SetActive(true);
            RocketList[2].gameObject.SetActive(false);
            RocketList[3].gameObject.SetActive(false);
            RocketList[4].gameObject.SetActive(false);
            RocketList[5].gameObject.SetActive(false);
        }
        if (CurrentRocket == 2)
        {
            RocketList[0].gameObject.SetActive(false);
            RocketList[1].gameObject.SetActive(false);
            RocketList[2].gameObject.SetActive(true);
            RocketList[3].gameObject.SetActive(false);
            RocketList[4].gameObject.SetActive(false);
            RocketList[5].gameObject.SetActive(false);
        }
        if (CurrentRocket == 3)
        {
            RocketList[0].gameObject.SetActive(false);
            RocketList[1].gameObject.SetActive(false);
            RocketList[2].gameObject.SetActive(false);
            RocketList[3].gameObject.SetActive(true);
            RocketList[4].gameObject.SetActive(false);
            RocketList[5].gameObject.SetActive(false);
        }
        if (CurrentRocket == 4)
        {
            RocketList[0].gameObject.SetActive(false);
            RocketList[1].gameObject.SetActive(false);
            RocketList[2].gameObject.SetActive(false);
            RocketList[3].gameObject.SetActive(false);
            RocketList[4].gameObject.SetActive(true);
            RocketList[5].gameObject.SetActive(false);
        }
        if (CurrentRocket == 5)
        {
            RocketList[0].gameObject.SetActive(false);
            RocketList[1].gameObject.SetActive(false);
            RocketList[2].gameObject.SetActive(false);
            RocketList[3].gameObject.SetActive(false);
            RocketList[4].gameObject.SetActive(false);
            RocketList[5].gameObject.SetActive(true);
        }
    }
    public void MoneyFuncion()
    {
        //Add money depending on the rocket you have
        if (RocketList[0].gameObject.activeInHierarchy)
        {
            Money += Mathf.RoundToInt(MoveUpScript.transform.position.y * 2);
        }
        if (RocketList[1].gameObject.activeInHierarchy)
        {
            Money += Mathf.RoundToInt(MoveUpScript.transform.position.y * 5);
        }
        if (RocketList[2].gameObject.activeInHierarchy)
        {
            Money += Mathf.RoundToInt(MoveUpScript.transform.position.y * 8);
        }
        if (RocketList[3].gameObject.activeInHierarchy)
        {
            Money += Mathf.RoundToInt(MoveUpScript.transform.position.y * 12);
        }
        if (RocketList[4].gameObject.activeInHierarchy)
        {
            Money += Mathf.RoundToInt(MoveUpScript.transform.position.y * 20);
        }
        if (RocketList[5].gameObject.activeInHierarchy)
        {
            Money += Mathf.RoundToInt(MoveUpScript.transform.position.y * 30);
        }
    }
    public void OnSettingsClick()
    {
        AchievementsPanel.gameObject.SetActive(true);
    }
    public void OnFasterButtonClick()
    {
        //Change world time
        Time.timeScale = 8f;
    }
    public void RocketBuy1()
    {
        if (Money >= 10000)
        {
            Power += 1000;
            Money -= 10000;
            //Set the right rocket
            CurrentRocket = 1;
            //Change button with .png
            BBPanel1[0].gameObject.SetActive(false);
            TruePanel1[0].gameObject.SetActive(true);
            Save();
        }
    }
    public void RocketBuy2()
    {
        if (Money >= 25000)
        {
            Money -= 25000;
            Power += 2000;
            //Set the right rocket
            CurrentRocket = 2;
            //Change button with .png
            BBPanel1[1].gameObject.SetActive(false);
            TruePanel1[1].gameObject.SetActive(true);
            Save();
        }

    }
    public void RocketBuy3()
    {
        if (Money >= 100000)
        {
            Money -= 100000;
            Power += 4000;
            //Set the right rocket
            CurrentRocket = 3;
            //Change button with .png
            BBPanel1[2].gameObject.SetActive(false);
            TruePanel1[2].gameObject.SetActive(true);
            Save();
        }
    }
    public void RocketBuy4()
    {
        if (Money >= 500000)
        {
            Money -= 500000;
            Power += 6000;
            //Set the right rocket
            CurrentRocket = 4;
            //Change button with .png
            BBPanel1[3].gameObject.SetActive(false);
            TruePanel1[3].gameObject.SetActive(true);
            Save();
        }
    }
    public void RocketBuy5()
    {
        if (Money >= 1000000)
        {
            Money -= 1000000;
            Power += 10000;
            //Set the right rocket
            CurrentRocket = 5;
            //Change button with .png
            TruePanel1[4].gameObject.SetActive(true);
            Save();
        }
    }
    public void LookAt()
    {
        if (RocketList[0].gameObject.activeInHierarchy)
        {
            vcam.LookAt = RocketList[0].transform;
            vcam.Follow = RocketList[0].transform;
        }
        if (RocketList[1].gameObject.activeInHierarchy)
        {
            vcam.LookAt = RocketList[1].transform;
            vcam.Follow = RocketList[1].transform;
        }
        if (RocketList[2].gameObject.activeInHierarchy)
        {
            vcam.LookAt = RocketList[2].transform;
            vcam.Follow = RocketList[2].transform;
        }
        if (RocketList[3].gameObject.activeInHierarchy)
        {
            vcam.LookAt = RocketList[3].transform;
            vcam.Follow = RocketList[3].transform;
        }
        if (RocketList[4].gameObject.activeInHierarchy)
        {
            vcam.LookAt = RocketList[4].transform;
            vcam.Follow = RocketList[4].transform;
        }
        if (RocketList[5].gameObject.activeInHierarchy)
        {
            vcam.LookAt = RocketList[5].transform;
            vcam.Follow = RocketList[5].transform;
        }
    }
    public void AddPower()
    {
        if (EngineUnlocked[0])
        {
            Power += EnginePowerChanger[0];
        }
        if (EngineUnlocked[1])
        {
            Power += EnginePowerChanger[1];
        }
        if (EngineUnlocked[2])
        {
            Power += EnginePowerChanger[2];
        }
        if (EngineUnlocked[3])
        {
            Power += EnginePowerChanger[3];
        }
        if (EngineUnlocked[4])
        {
            Power += EnginePowerChanger[4];
        }
        if (EngineUnlocked[5])
        {
            Power += EnginePowerChanger[5];
        }
    }
    public void EngineBuy1()
    {
        if (Money >= 2000)
        {
            Money -= 2000;
            //Set the right engine
            EngineUnlocked[0] = true;
            EngineUnlocked[1] = false;
            EngineUnlocked[2] = false;
            EngineUnlocked[3] = false;
            EngineUnlocked[4] = false;
            EngineUnlocked[5] = false;
            Power += EnginePowerChanger[0];
            //Change button with .png
            TruePanel2[0].gameObject.SetActive(true);
            Save();
        }
    }
    public void EngineBuy2()
    {
        if (Money >= 15000)
        {
            Money -= 15000;
            //Set the right engine
            EngineUnlocked[0] = false;
            EngineUnlocked[1] = true;
            EngineUnlocked[2] = false;
            EngineUnlocked[3] = false;
            EngineUnlocked[4] = false;
            EngineUnlocked[5] = false;
            Power += EnginePowerChanger[1];
            //Change button with .png
            TruePanel2[1].gameObject.SetActive(true);
            Save();
        }
    }
    public void EngineBuy3()
    {
        if (Money >= 50000)
        {
            Money -= 50000;
            //Set the right engine
            EngineUnlocked[0] = false;
            EngineUnlocked[1] = false;
            EngineUnlocked[2] = true;
            EngineUnlocked[3] = false;
            EngineUnlocked[4] = false;
            EngineUnlocked[5] = false;
            Power += EnginePowerChanger[2];
            //Change button with .png
            TruePanel2[2].gameObject.SetActive(true);
            Save();
        }
    }
    public void EngineBuy4()
    {
        if (Money >= 80000)
        {
            Money -= 80000;
            //Set the right engine
            EngineUnlocked[0] = false;
            EngineUnlocked[1] = false;
            EngineUnlocked[2] = false;
            EngineUnlocked[3] = true;
            EngineUnlocked[4] = false;
            EngineUnlocked[5] = false;
            Power += EnginePowerChanger[3];
            //Change button with .png
            TruePanel2[3].gameObject.SetActive(true);
            Save();
        }
    }
    public void EngineBuy5()
    {
        if (Money >= 120000)
        {
            Money -= 120000;
            //Set the right engine
            EngineUnlocked[0] = false;
            EngineUnlocked[1] = false;
            EngineUnlocked[2] = false;
            EngineUnlocked[3] = false;
            EngineUnlocked[4] = true;
            EngineUnlocked[5] = false;
            Power += EnginePowerChanger[4];
            //Change button with .png
            TruePanel2[4].gameObject.SetActive(true);
            Save();
        }
    }
    public void EngineBuy6()
    {
        if (Money >= 300000)
        {
            Money -= 300000;
            //Set the right engine
            EngineUnlocked[0] = false;
            EngineUnlocked[1] = false;
            EngineUnlocked[2] = false;
            EngineUnlocked[3] = false;
            EngineUnlocked[4] = false;
            EngineUnlocked[5] = true;
            Power += EnginePowerChanger[5];
            //Change button with .png
            TruePanel2[5].gameObject.SetActive(true);
            Save();
        }
    }
    public void BuyButtonsManager()
    {
        //Engine
        if (EngineUnlocked[0])
        {
            BBPanel2[0].gameObject.SetActive(false);
            BBPanel2[1].gameObject.SetActive(true);
            BBPanel2[2].gameObject.SetActive(true);
            BBPanel2[3].gameObject.SetActive(true);
            BBPanel2[4].gameObject.SetActive(true);
            BBPanel2[5].gameObject.SetActive(true);

            TruePanel2[0].gameObject.SetActive(true);
            TruePanel2[1].gameObject.SetActive(false);
            TruePanel2[2].gameObject.SetActive(false);
            TruePanel2[3].gameObject.SetActive(false);
            TruePanel2[4].gameObject.SetActive(false);
            TruePanel2[5].gameObject.SetActive(false);
        }
        if (EngineUnlocked[1])
        {
            BBPanel2[0].gameObject.SetActive(false);
            BBPanel2[1].gameObject.SetActive(false);
            BBPanel2[2].gameObject.SetActive(true);
            BBPanel2[3].gameObject.SetActive(true);
            BBPanel2[4].gameObject.SetActive(true);
            BBPanel2[5].gameObject.SetActive(true);

            TruePanel2[0].gameObject.SetActive(true);
            TruePanel2[1].gameObject.SetActive(true);
            TruePanel2[2].gameObject.SetActive(false);
            TruePanel2[3].gameObject.SetActive(false);
            TruePanel2[4].gameObject.SetActive(false);
            TruePanel2[5].gameObject.SetActive(false);
        }
        if (EngineUnlocked[2])
        {
            BBPanel2[0].gameObject.SetActive(false);
            BBPanel2[1].gameObject.SetActive(false);
            BBPanel2[2].gameObject.SetActive(false);
            BBPanel2[3].gameObject.SetActive(true);
            BBPanel2[4].gameObject.SetActive(true);
            BBPanel2[5].gameObject.SetActive(true);

            TruePanel2[0].gameObject.SetActive(true);
            TruePanel2[1].gameObject.SetActive(true);
            TruePanel2[2].gameObject.SetActive(true);
            TruePanel2[3].gameObject.SetActive(false);
            TruePanel2[4].gameObject.SetActive(false);
            TruePanel2[5].gameObject.SetActive(false);
        }
        if (EngineUnlocked[3])
        {
            BBPanel2[0].gameObject.SetActive(false);
            BBPanel2[1].gameObject.SetActive(false);
            BBPanel2[2].gameObject.SetActive(false);
            BBPanel2[3].gameObject.SetActive(false);
            BBPanel2[4].gameObject.SetActive(true);
            BBPanel2[5].gameObject.SetActive(true);

            TruePanel2[0].gameObject.SetActive(true);
            TruePanel2[1].gameObject.SetActive(true);
            TruePanel2[2].gameObject.SetActive(true);
            TruePanel2[3].gameObject.SetActive(true);
            TruePanel2[4].gameObject.SetActive(false);
            TruePanel2[5].gameObject.SetActive(false);
        }
        if (EngineUnlocked[4])
        {
            BBPanel2[0].gameObject.SetActive(false);
            BBPanel2[1].gameObject.SetActive(false);
            BBPanel2[2].gameObject.SetActive(false);
            BBPanel2[3].gameObject.SetActive(false);
            BBPanel2[4].gameObject.SetActive(false);
            BBPanel2[5].gameObject.SetActive(true);

            TruePanel2[0].gameObject.SetActive(true);
            TruePanel2[1].gameObject.SetActive(true);
            TruePanel2[2].gameObject.SetActive(true);
            TruePanel2[3].gameObject.SetActive(true);
            TruePanel2[4].gameObject.SetActive(true);
            TruePanel2[5].gameObject.SetActive(false);
        }
        if (EngineUnlocked[5])
        {
            BBPanel2[0].gameObject.SetActive(false);
            BBPanel2[1].gameObject.SetActive(false);
            BBPanel2[2].gameObject.SetActive(false);
            BBPanel2[3].gameObject.SetActive(false);
            BBPanel2[4].gameObject.SetActive(false);
            BBPanel2[5].gameObject.SetActive(false);

            TruePanel2[0].gameObject.SetActive(true);
            TruePanel2[1].gameObject.SetActive(true);
            TruePanel2[2].gameObject.SetActive(true);
            TruePanel2[3].gameObject.SetActive(true);
            TruePanel2[4].gameObject.SetActive(true);
            TruePanel2[5].gameObject.SetActive(true);
        }

        //Rocket
        if (CurrentRocket == 0)
        {
            BBPanel1[0].gameObject.SetActive(true);
            BBPanel1[1].gameObject.SetActive(true);
            BBPanel1[2].gameObject.SetActive(true);
            BBPanel1[3].gameObject.SetActive(true);
            BBPanel1[4].gameObject.SetActive(true);


            TruePanel1[0].gameObject.SetActive(false);
            TruePanel1[1].gameObject.SetActive(false);
            TruePanel1[2].gameObject.SetActive(false);
            TruePanel1[3].gameObject.SetActive(false);
            TruePanel1[4].gameObject.SetActive(false);

        }
        if (CurrentRocket == 1)
        {
            BBPanel1[0].gameObject.SetActive(false);
            BBPanel1[1].gameObject.SetActive(true);
            BBPanel1[2].gameObject.SetActive(true);
            BBPanel1[3].gameObject.SetActive(true);
            BBPanel1[4].gameObject.SetActive(true);


            TruePanel1[0].gameObject.SetActive(true);
            TruePanel1[1].gameObject.SetActive(false);
            TruePanel1[2].gameObject.SetActive(false);
            TruePanel1[3].gameObject.SetActive(false);
            TruePanel1[4].gameObject.SetActive(false);

        }
        if (CurrentRocket == 2)
        {
            BBPanel1[0].gameObject.SetActive(false);
            BBPanel1[1].gameObject.SetActive(false);
            BBPanel1[2].gameObject.SetActive(true);
            BBPanel1[3].gameObject.SetActive(true);
            BBPanel1[4].gameObject.SetActive(true);


            TruePanel1[0].gameObject.SetActive(true);
            TruePanel1[1].gameObject.SetActive(true);
            TruePanel1[2].gameObject.SetActive(false);
            TruePanel1[3].gameObject.SetActive(false);
            TruePanel1[4].gameObject.SetActive(false);

        }
        if (CurrentRocket == 3)
        {
            BBPanel1[0].gameObject.SetActive(false);
            BBPanel1[1].gameObject.SetActive(false);
            BBPanel1[2].gameObject.SetActive(false);
            BBPanel1[3].gameObject.SetActive(true);
            BBPanel1[4].gameObject.SetActive(true);


            TruePanel1[0].gameObject.SetActive(true);
            TruePanel1[1].gameObject.SetActive(true);
            TruePanel1[2].gameObject.SetActive(true);
            TruePanel1[3].gameObject.SetActive(false);
            TruePanel1[4].gameObject.SetActive(false);

        }
        if (CurrentRocket == 4)
        {
            BBPanel1[0].gameObject.SetActive(false);
            BBPanel1[1].gameObject.SetActive(false);
            BBPanel1[2].gameObject.SetActive(false);
            BBPanel1[3].gameObject.SetActive(false);
            BBPanel1[4].gameObject.SetActive(true);


            TruePanel1[0].gameObject.SetActive(true);
            TruePanel1[1].gameObject.SetActive(true);
            TruePanel1[2].gameObject.SetActive(true);
            TruePanel1[3].gameObject.SetActive(true);
            TruePanel1[4].gameObject.SetActive(false);

        }
        if (CurrentRocket == 5)
        {
            BBPanel1[0].gameObject.SetActive(false);
            BBPanel1[1].gameObject.SetActive(false);
            BBPanel1[2].gameObject.SetActive(false);
            BBPanel1[3].gameObject.SetActive(false);
            BBPanel1[4].gameObject.SetActive(false);


            TruePanel1[0].gameObject.SetActive(true);
            TruePanel1[1].gameObject.SetActive(true);
            TruePanel1[2].gameObject.SetActive(true);
            TruePanel1[3].gameObject.SetActive(true);
            TruePanel1[4].gameObject.SetActive(true);

        }
        if (CurrentRocket == 6)
        {
            BBPanel1[0].gameObject.SetActive(false);
            BBPanel1[1].gameObject.SetActive(false);
            BBPanel1[2].gameObject.SetActive(false);
            BBPanel1[3].gameObject.SetActive(false);
            BBPanel1[4].gameObject.SetActive(false);


            TruePanel1[0].gameObject.SetActive(true);
            TruePanel1[1].gameObject.SetActive(true);
            TruePanel1[2].gameObject.SetActive(true);
            TruePanel1[3].gameObject.SetActive(true);
            TruePanel1[4].gameObject.SetActive(true);

        }
    }
    public void AchievementsLoad()
    {
        if (!Ach1.activeSelf)
        {
            AchievementsScript.AchievmentsList[0] = true;
        }

    }
    public void MoneyAch()
    {
        if (Money >= 1000000)
        {
            AchievementsScript.AchievmentsList[1] = true;
        }
        else if (Money <= 1000000)
        {
            AchievementsScript.AchievmentsList[1] = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StartTrig"))
        {
            Smoke.gameObject.SetActive(false);
        }
        if (col.gameObject.CompareTag("Sattelite"))
        {
            print("Sattelite");
        }
    }
}
