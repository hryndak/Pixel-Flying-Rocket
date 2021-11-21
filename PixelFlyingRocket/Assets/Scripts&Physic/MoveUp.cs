using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MoveUp : MonoBehaviour
{
    private Achievments AchievmentsScript;
    private Buttons ButtonsScript;
    private Rigidbody2D Rb;


    public bool EngineisOn;
    private bool HaveFuel;
    private float Fuel;
    private GameObject BlueFire;
    private GameObject BlueSmoke;

    private GameObject ObjectWithScript;

    public Vector2 StartTransfrom;
    public float SetFuel;
    public bool isPaused;
    public bool isPlaying;
    public float BurnRate;
    public float FuelToAdd;

    //public int[] EnginePowerChanger;

    public Text[] PowerText;
    public Text[] BurnRateText;
    public Text[] MoneyText;

    private GameObject Canvas;

    void Start()
    {
        ObjectWithScript = GameObject.Find("RocketHolder");
        ButtonsScript = ObjectWithScript.GetComponent<Buttons>();
        BlueFire = GameObject.Find("BlueFire");
        BlueSmoke = GameObject.Find("BlueSmoke");
        StartTransfrom = transform.position;
        BlueFire.gameObject.SetActive(false);
        BlueSmoke.gameObject.SetActive(false);
    }
    void Update()
    {
        EnigneSwitch();
        FuelFuncion();
        SmokeFuncion();
        //UIFunc();
        AchievmentsScript = ObjectWithScript.GetComponent<Achievments>();
    }

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void EnigneSwitch()
    {
        Rb.mass = 1f;
        switch (EngineisOn)
        {
            case true:
                Rb.AddForce(new Vector2(0f, ButtonsScript.Power * Time.deltaTime), ForceMode2D.Force);
                break;
            case false:
                Rb.AddForce(new Vector2(0f, 0f * Time.deltaTime), ForceMode2D.Force);
                break;
        }
    }
    public void SmokeFuncion()
    {
        if (EngineisOn && isPlaying)
        {
            BlueFire.gameObject.SetActive(true);
            BlueSmoke.gameObject.SetActive(true);
        }
        else
        {
            BlueSmoke.gameObject.SetActive(false);
            BlueFire.gameObject.SetActive(false);
        }
    }
    private void FuelFuncion()
    {
        if (Input.GetMouseButtonDown(0) && HaveFuel && isPlaying)
        {
            EngineisOn = true;
        }
        if (Input.GetMouseButtonUp(0) && HaveFuel && isPlaying)
        {
            EngineisOn = false;
        }

        if (EngineisOn)
        {
            FuelToAdd -= BurnRate * Time.deltaTime;
        }
        Fuel = Mathf.RoundToInt(FuelToAdd);

        if (FuelToAdd <= 0 && isPlaying)
        {
            if (Rb.velocity.y > 0 && Rb.velocity.y < 20 && isPlaying)
            {
                isPaused = true;
            }

            HaveFuel = false;
        }

        if (!HaveFuel)
        {
            EngineisOn = false;
        }
        if (!isPlaying)
        {
            transform.position = StartTransfrom;
        }

        if (FuelToAdd > 0)
        {
            HaveFuel = true;
        }
        if (FuelToAdd <= 0)
        {
            HaveFuel = false;
        }
    }
    /*public void PowerChanger()
    {
        if (ButtonsScript.EngineUnlocked[0])
        {
            Power += EnginePowerChanger[0];
        }
        if (ButtonsScript.EngineUnlocked[1])
        {
            Power += EnginePowerChanger[1];
        }
        if (ButtonsScript.EngineUnlocked[2])
        {
            Power += EnginePowerChanger[2];
        }
        if (ButtonsScript.EngineUnlocked[3])
        {
            Power += EnginePowerChanger[3];
        }
        if (ButtonsScript.EngineUnlocked[4])
        {
            Power += EnginePowerChanger[4];
        }
        if (ButtonsScript.EngineUnlocked[5])
        {
            Power += EnginePowerChanger[5];
        }
    }
    */
    private void UIFunc()
    {

        PowerText[0].text = "Power: " + ButtonsScript.Power.ToString();
        PowerText[1].text = "Power: " + ButtonsScript.Power.ToString();

        BurnRateText[0].text = "Burn Rate: " + BurnRate.ToString();
        BurnRateText[1].text = "Burn Rate: " + BurnRate.ToString();

        MoneyText[0].text = ButtonsScript.Money.ToString();
        MoneyText[1].text = ButtonsScript.Money.ToString();
        MoneyText[2].text = ButtonsScript.Money.ToString();
    }
    private void Achievments()
    {
        if (ButtonsScript.Money >= 1000000)
        {
            AchievmentsScript.AchievmentsList[1] = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Sattelite"))
        {
            AchievmentsScript.animator.SetTrigger("isReached");
            AchievmentsScript.AchievmentsList[0] = true;
        }

        if (col.gameObject.CompareTag("ISS"))
        {
            AchievmentsScript.AchievmentsList[3] = true;
        }

        if (col.gameObject.CompareTag("StartTrig"))
        {
            ButtonsScript.changeSound = true;
        }
    }
}
