using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsUnity : MonoBehaviour
{
    public static AnalyticsUnity analytics;
    public DataAnalytics data;

    private void Start()
    {
        analytics = this;
        data = new DataAnalytics();
    }


    //This function to storing custom event to Unity Analytics
    public void StoreAnalytics()
    {
        Analytics.CustomEvent("Gameplay", new Dictionary<string, object> {
              { "Total Catcher Time", data.totalTime },
              { "Total Being Catcher", data.catcher }
        });
    }


    // To define how much the player being a catcher and store the first time he being
    public void BeingCatch(int time)
    {
        data.catcher++;
        data.startCatch = time;
    }

    // To calcuate how much time he being a catcher and reset the first time to zero
    // This function is called when the player is not being a catcher anymore
    public void EndCatch(int time)
    {
        if (data.startCatch != 0)
        {
            data.endCatch = time;
            data.totalTime += (data.endCatch - data.startCatch);
            data.startCatch = 0;
            data.endCatch = 0;
        }
    }

}

[System.Serializable]
public class DataAnalytics
{
    //Data analytics for storing to Unity Analytics
    //But, there is internal error to my Unity, so my Unity don't validate to integrate analytics when play button pressed.
    public int catcher = 0;
    public int startCatch = 0;
    public int endCatch = 0;
    public int totalTime = 0;

    // To defining the first initialize when this class is created
    public DataAnalytics()
    {
        catcher = 0;
        startCatch = 0;
        endCatch = 0;
        totalTime = 0;
    }
}