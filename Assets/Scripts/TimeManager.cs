using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public ColorSettings LavaColor, RockyColor, WaterColor, GreenColor, IcyColor;

    public Planet planet;
    public GeneratePlanet generatePlanet;

    public TextMeshProUGUI CurrentTimeMMSS, TotalElapsed, TotalRemaining, NextPhase;
    public TextMeshProUGUI CurrentPhase;
    
    //minutes
    public float FocusTimeCounter, BreakTimeCounter ,ElapsedCounter, RemainingCounter;
    public float MaxTotalTime;
    public float MaxFocusTime;
    public float MaxBreakTime;
    public int loopCount;
    public int currentLoop;
    public bool FocusPhase;
    public float IntervalTime; 
    public float IntervalCounter;
    public int CurrentInterval;

    public bool AllClrUpdated = false;
    void Start()
    {
        loopCount = 1;
        MaxBreakTime = 1f;
        MaxFocusTime = 1f;

        MaxTotalTime = ((MaxFocusTime + MaxBreakTime) * loopCount);
        FocusTimeCounter = 60 * MaxFocusTime;
        BreakTimeCounter = 60 * MaxBreakTime;

        ElapsedCounter = 0;
        RemainingCounter = MaxTotalTime * 60f;

        FocusPhase = true;

        IntervalTime = (MaxTotalTime * 60f) / 5f;
        CurrentInterval = 1;
        IntervalCounter = IntervalTime;
        generatePlanet.SetShape();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLoop<=loopCount)
        {
            if (FocusPhase)
            {
                FocusTimeCounter -= Time.deltaTime;
                CurrentTimeMMSS.text = FormatTimeToMMSS(FocusTimeCounter);
                if (FocusTimeCounter <= 0f)
                {
                    FocusPhase = false;
                    BreakTimeCounter = MaxBreakTime * 60f;
                    CurrentPhase.text = "IN BREAK MODE";
                    CurrentPhase.color = Color.green;
                }
            }
            else
            {
                BreakTimeCounter -= Time.deltaTime;
                CurrentTimeMMSS.text = FormatTimeToMMSS(BreakTimeCounter);
                if (BreakTimeCounter <= 0)
                {
                    if (currentLoop == loopCount)
                    {
                        //Over
                    }
                    else
                    {
                        FocusTimeCounter = MaxFocusTime * 60f;
                        FocusPhase = true;
                        currentLoop++;
                        CurrentPhase.text = "IN FOCUS MODE";
                        CurrentPhase.color = Color.yellow;
                    }
                }

            }

            ElapsedCounter += Time.deltaTime;
            TotalElapsed.text = FormatTimeToHHMMSS(ElapsedCounter);

            RemainingCounter -= Time.deltaTime;
            TotalRemaining.text = FormatTimeToHHMMSS(RemainingCounter);

            IntervalClr();
        }
        
    }

    public void IntervalClr()
    {
        if(AllClrUpdated == true)
        {

        }
        else
        {
            IntervalCounter -= Time.deltaTime;
            if (IntervalCounter <= 0f)
            {
                if (CurrentInterval == 1)
                {
                    planet.colorSettings = RockyColor;
                    CurrentInterval++;
                    IntervalCounter = IntervalTime;
                    generatePlanet.SetColor();
                    Debug.Log("Clr Change triggered - Rocky");
                }
                else if (CurrentInterval == 2)
                {
                    planet.colorSettings = WaterColor;
                    CurrentInterval++;
                    IntervalCounter = IntervalTime;
                    generatePlanet.SetColor();
                    Debug.Log("Clr Change triggered - Water");
                }
                else if (CurrentInterval == 3)
                {
                    planet.colorSettings = IcyColor;
                    CurrentInterval++;
                    IntervalCounter = IntervalTime;
                    generatePlanet.SetColor();
                    Debug.Log("Clr Change triggered - Icy");
                }
                else if (CurrentInterval == 4)
                {
                    planet.colorSettings = GreenColor;
                    CurrentInterval++;
                    IntervalCounter = IntervalTime;
                    generatePlanet.SetColor();
                    Debug.Log("Clr Change triggered - Green");
                    IntervalCounter = 0f;
                    AllClrUpdated = true;
                }
            }
            NextPhase.text = FormatTimeToHHMMSS(IntervalCounter);
        }
        

        

    }

    private string FormatTimeToMMSS(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60);
        int secs = Mathf.FloorToInt(seconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, secs);
    }

    private string FormatTimeToHHMMSS(float seconds)
    {
        int hours = Mathf.FloorToInt(seconds / 3600);
        int minutes = Mathf.FloorToInt((seconds % 3600) / 60);
        int secs = Mathf.FloorToInt(seconds % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, secs);
    }
}
