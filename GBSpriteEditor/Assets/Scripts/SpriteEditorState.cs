using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ACTIVE_TOOL {
    PENCIL
}

public class SpriteEditorState : MonoBehaviour
{


    public Image palette0;
    public Image palette1;
    public Image palette2;
    public Image palette3;
    public InputField spriteData;
    public Color activePaletteColor;
    public ACTIVE_TOOL activeTool;
    public bool inSettings = false;
    
    void Start()
    {
        if (palette0 == null || palette1 == null || palette2 == null || palette3 == null)
        {
            Debug.Log("Make sure to set palette images in SpriteEditorState object!  At least 1 palette is not set.");
        }
    }

    void Update()
    {
    }
}
