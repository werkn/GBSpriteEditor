using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public enum ACTIVE_TOOL {
    PENCIL
}

public class SpriteEditorState : MonoBehaviour
{
    //expose browser to Unity, allow interop (look in Assets/Plugins/browser_interop.js for code
    //this method sets up browser form controls/buttons and UI elements needed for interop
    #if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void InitBrowserState(string palette0Color, string palette1Color, string palette2Color, string palette3Color);
    #endif

    public Image palette0;
    public Image palette1;
    public Image palette2;
    public Image palette3;
    public GameObject pixelPanel;
    public InputField spriteData;
    public Color activePaletteColor;
    public ACTIVE_TOOL activeTool;
    public bool inSettings = false;
    
    public void ResetSprite()
    {
        Image[] imageRefs = pixelPanel.GetComponentsInChildren<Image>();
        
        //iterate over collection and swap old color indexes with new ones
        foreach (Image imageRef in imageRefs)
        {
         
            if (imageRef.color == palette0.color) { imageRef.color = palette2.color; }
            else if (imageRef.color == palette1.color) { imageRef.color = palette2.color; }
            else if (imageRef.color == palette2.color) { imageRef.color = palette2.color; }
            else if (imageRef.color == palette3.color) { imageRef.color = palette2.color; }
        }
    }

    //update the pixel panel when palette is updated in browser
    public void UpdatePixelPanel(string newPalette0, string newPalette1, string newPalette2, string newPalette3)
    {
        Image[] imageRefs = pixelPanel.GetComponentsInChildren<Image>();

        Color newPalette0Color, newPalette1Color, newPalette2Color, newPalette3Color; 
        ColorUtility.TryParseHtmlString(newPalette0, out newPalette0Color);
        ColorUtility.TryParseHtmlString(newPalette1, out newPalette1Color);
        ColorUtility.TryParseHtmlString(newPalette2, out newPalette2Color);
        ColorUtility.TryParseHtmlString(newPalette3, out newPalette3Color);

        //iterate over collection and swap old color indexes with new ones
        foreach (Image imageRef in imageRefs)
        {
            if (imageRef.color == palette0.color) { imageRef.color = newPalette0Color; }
            else if (imageRef.color == palette1.color) { imageRef.color = newPalette1Color; }
            else if (imageRef.color == palette2.color) { imageRef.color = newPalette2Color; }
            else if (imageRef.color == palette3.color) { imageRef.color = newPalette3Color; }
        }

        //make sure to swap out active color
        if (palette0.color == activePaletteColor) { activePaletteColor = newPalette0Color; }
        else if (palette1.color == activePaletteColor) { activePaletteColor = newPalette1Color; }
        else if (palette2.color == activePaletteColor) { activePaletteColor = newPalette2Color; }
        else if (palette3.color == activePaletteColor) { activePaletteColor = newPalette3Color; }
        else { activePaletteColor = newPalette0Color; }
    }

    //Syncs with browser controls for palette colors, ie: we can change browser color input and it will 
    //have the change reflected in the unity webgl app
    public void SyncBrowserPalette(string paletteColorsCSV)
    {
        string[] paletteColors = paletteColorsCSV.Split(',');

        Debug.Log(paletteColorsCSV);
        Debug.Log(paletteColors.ToString());

        //swap current palette(s) with new palette(s)
        UpdatePixelPanel(paletteColors[0], paletteColors[1], paletteColors[2], paletteColors[3]);

        //now swap out indexes
        Color tmpColor = Color.black;
        ColorUtility.TryParseHtmlString(paletteColors[0], out tmpColor);
        palette0.color = tmpColor;
        ColorUtility.TryParseHtmlString(paletteColors[1], out tmpColor);
        palette1.color = tmpColor;
        ColorUtility.TryParseHtmlString(paletteColors[2], out tmpColor);
        palette2.color = tmpColor;
        ColorUtility.TryParseHtmlString(paletteColors[3], out tmpColor);
        palette3.color = tmpColor;
    }

    void Start()
    {
        if (palette0 == null || palette1 == null || palette2 == null || palette3 == null)
        {
            Debug.Log("Make sure to set palette images in SpriteEditorState object!  At least 1 palette is not set.");
        }
        else
        {
            
            #if UNITY_WEBGL
                InitBrowserState("#"+ColorUtility.ToHtmlStringRGB(palette0.color),
                    "#" + ColorUtility.ToHtmlStringRGB(palette1.color),
                    "#" + ColorUtility.ToHtmlStringRGB(palette2.color),
                    "#" + ColorUtility.ToHtmlStringRGB(palette3.color));
            #endif


            //reset all pixels in pixel panel and reset active palette index
            ResetSprite();

            //reset active palette color
            activePaletteColor = palette3.color;
        }

    }

    void Update()
    {
    }
}
