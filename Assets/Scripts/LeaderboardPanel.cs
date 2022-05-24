using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardPanel : MonoBehaviour
{
    public List<TMP_Text> leaderboardPlaces;

    private void Start()
    {
        Leaderboard.Reset();
    }

    private void LateUpdate()
    {
        List<string> places = Leaderboard.GetPlaces();

        for(int i=0; i<leaderboardPlaces.Count; i++)
        {
            if(i<places.Count)
            {
                leaderboardPlaces[i].text = places[i];
            }
            else
            {
                leaderboardPlaces[i].text = "";
            }
        }
    }
}
