using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndingScript : MonoBehaviour , IPlayerTriggerable
{
    public GameObject ending1;
    public GameObject ending2;
    public GameObject ending3;
    public GameObject ending4;
    public TextSequencer textSequencer;


    public void OnPlayerTriggered(PlayerController player)
    {
        Debug.Log("Ending");
        textSequencer.StartTextSequence();
        // int max_val = DayTracker.Instance.choose_counter.Max();
        // int index = DayTracker.Instance.choose_counter.IndexOf(max_val);

        // switch (index)
        // {
        //     case 0:
        //         ending1.SetActive(true);
        //         break;
        //     case 1:
        //         ending2.SetActive(true);
        //         break;
        //     case 2:
        //         ending3.SetActive(true);
        //         break;
        //     case 3:
        //         ending4.SetActive(true);
        //         break;
        // }
    }
}
