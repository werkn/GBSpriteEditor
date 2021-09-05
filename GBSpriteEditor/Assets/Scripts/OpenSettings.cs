using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettings : MonoBehaviour
{
    public GameObject panel;
    public SpriteEditorState editorState;
    public GameObject[] pixelsRow0;
    public GameObject[] pixelsRow1;
    public GameObject[] pixelsRow2;
    public GameObject[] pixelsRow3;
    public GameObject[] pixelsRow4;
    public GameObject[] pixelsRow5;
    public GameObject[] pixelsRow6;
    public GameObject[] pixelsRow7;

    private uint GetPixelPaletteIndex(Color pixelColor) 
    {
        if (pixelColor == editorState.palette0.color) { return 0; }
        else if (pixelColor == editorState.palette1.color) { return 1; }
        else if (pixelColor == editorState.palette2.color) { return 2; }
        else if (pixelColor == editorState.palette3.color) { return 3; }

        return 0;
    }

    public string GetSpriteRowAsHexString(GameObject[] pixelRow)
    {
        //we could just use 0 here but using binary notation
        //so precision required / masking is explicit
        uint byte0 = 0b00_00_00_00;
        uint byte1 = 0b00_00_00_00;

        if (pixelRow.Length == 8)
        {
            for (int i = 0; i < 8; i++)
            {
                //convert the active color to its paletteIndex (0,1,2,3)
                uint paletteIndex = GetPixelPaletteIndex(pixelRow[i].GetComponent<Image>().color);

                //bit shift and capture first part of color to first byte
                //Debug.Log($"paletteIndex: {paletteIndex} pI & 0b10 { System.Convert.ToString(paletteIndex >> 1, 2) } ");
                byte0 += ((paletteIndex >> 1) << (7 - i));
                //Debug.Log($"byte0: {System.Convert.ToString(byte0, 2)}");

                //bit shift and capture second part of color to second byte
                //Debug.Log($"paletteIndex: {paletteIndex} pI & 0b01 { System.Convert.ToString(paletteIndex & 0b01, 2) } ");
                byte1 += ((paletteIndex & 0b01) << (7 - i));
            }

            //Debug.Log($"Byte0: {byte0:X2}");
            //Debug.Log($"Byte1: {byte1:X2}");
        }

        //keep precision of 4 digits, hex (max value is FFFF per row)
        return $"{byte0:X2} {byte1:X2}";
    }

    /** 
     * We currently just handle sprites as GameObjects, we probably should just grab
     * these as an array of GetComponentInChildren but meh, works for now.
     */
    public string GetSpriteAsHexString()
    {
        string res = $"{GetSpriteRowAsHexString(pixelsRow0)} ";
        res += $"{GetSpriteRowAsHexString(pixelsRow1)} ";
        res += $"{GetSpriteRowAsHexString(pixelsRow2)} ";
        res += $"{GetSpriteRowAsHexString(pixelsRow3)} ";
        res += $"{GetSpriteRowAsHexString(pixelsRow4)} ";
        res += $"{GetSpriteRowAsHexString(pixelsRow5)} ";
        res += $"{GetSpriteRowAsHexString(pixelsRow6)} ";
        res += $"{GetSpriteRowAsHexString(pixelsRow7)}";

        return res;
    }

    public void OpenSettingsMenu()
    {

        //convert sprite to string
        string spriteAsHex = GetSpriteAsHexString();

        //update text in settings
        editorState.spriteData.text = spriteAsHex;

        if (panel != null)
        {
            Animator animator = panel.GetComponent<Animator>();
            if (animator != null)
            {
                bool openSettings = animator.GetBool("OpenSettings");
                animator.SetBool("OpenSettings", !openSettings);
                editorState.inSettings = !openSettings;
            }
        }
    }

}
