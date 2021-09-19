using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sprite2BPP : MonoBehaviour
{

    /**
    * NOTE:  Cannot serialize getters/setters using the JsonUtility, basic public fields
    * work though.
    */

    public GameObject row0_px0;
    public GameObject row1_px0;
    public GameObject row2_px0;
    public GameObject row3_px0;
    public GameObject row4_px0;
    public GameObject row5_px0;
    public GameObject row6_px0;
    public GameObject row7_px0;

    public Sprite2BPP() {
        //init sprite
        //flags = new bool[8];
        //spriteRowsAsHex = new string[8];
        //new List<string>(spriteRowsAsHex).ForEach( spriteRowAsHex => spriteRowAsHex = "FFFFFFFF" );
    }

    public string getSprite2BPPAsJson() {
        return JsonUtility.ToJson(this);
    }
}
