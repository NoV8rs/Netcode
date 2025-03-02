using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public TextMeshProUGUI homePointText;
    public TextMeshProUGUI awayPointText;
    public int homePoint;
    public int awayPoint;
    
    public void AddHomePoint()
    {
        homePoint++;
        homePointText.text = homePoint.ToString();
    }
    
    public void AddAwayPoint()
    {
        awayPoint++;
        awayPointText.text = awayPoint.ToString();
    }
    
    public void ResetPoints()
    {
        homePoint = 0;
        awayPoint = 0;
        homePointText.text = homePoint.ToString();
        awayPointText.text = awayPoint.ToString();
    }
}
