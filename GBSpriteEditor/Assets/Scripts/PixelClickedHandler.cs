using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PixelClickedHandler : MonoBehaviour
{
    #if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void UpdateSpriteDataTable(string spriteAsString);
    #endif

    private Button thisPixel;
    public SpriteEditorState editorState;
    public OpenSettings openSettings;

    private void Start() {
        thisPixel = GetComponent<Button>();
        if (thisPixel == null) {
            Debug.Log("Unable to get button reference!");
        }

        thisPixel.onClick.AddListener(ChangePixelColor);
    } 

    public void ChangePixelColor() {
        
        //ignore clicks when in settings
        if (!editorState.inSettings)
        {
            GetComponent<Image>().color = editorState.activePaletteColor;

            //update browser string
            UpdateSpriteDataTable(openSettings.GetSpriteAsHexString());
        }
    }

}
