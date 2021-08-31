using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelClickedHandler : MonoBehaviour
{
    private Button thisPixel;
    public SpriteEditorState editorState;

    private void Start() {
        thisPixel = GetComponent<Button>();
        if (thisPixel == null) {
            Debug.Log("Unable to get button reference!");
        }

        thisPixel.onClick.AddListener(ChangePixelColor);
    } 

    public void ChangePixelColor() {
        GetComponent<Image>().color = editorState.activePaletteColor;
    }

}
