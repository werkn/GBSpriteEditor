using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteClickedHandler : MonoBehaviour
{

    private Button thisPaletteButton;
    public SpriteEditorState editorState;

    // Start is called before the first frame update
    void Start()
    {
        thisPaletteButton = GetComponent<Button>();
        if (thisPaletteButton == null) {
            Debug.Log("Unable to get button reference!");
        }

        thisPaletteButton.onClick.AddListener(ChangePaletteColor);
    }
  
    public void ChangePaletteColor() {
       editorState.activePaletteColor = GetComponent<Image>().color;
    }
}

