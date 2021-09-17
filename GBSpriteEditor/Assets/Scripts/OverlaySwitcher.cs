using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlaySwitcher : MonoBehaviour
{
    public Sprite[] overlays;
    private int currentIndex = 0;

    public void NextOverlay()
    {
        GetComponent<Image>().sprite = overlays[(currentIndex++) % overlays.Length];
    }

}
