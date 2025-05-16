using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUiLogoChange : MonoBehaviour,IControllerDedection
{
    Sprite gameObjectImage;
    RectTransform ObjectTransform;
    [SerializeField] List<ButtonSpriteData> buttonSprites = new List<ButtonSpriteData>();

    
    private void OnEnable()
    {
        ControllerChecker.isKB += isKB;
        ControllerChecker.isPS += isPS;
        ControllerChecker.isXbox += isXbox;
    }
    private void OnDisable()
    {
        ControllerChecker.isKB -= isKB;
        ControllerChecker.isPS -= isPS;
        ControllerChecker.isXbox -= isXbox;
    }

    public void isKB()
    {
        GetComponent<Image>().sprite = buttonSprites[0].sprite;
        GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSprites[0].size.x, buttonSprites[0].size.y);
    }

    public void isPS()
    {
        GetComponent<Image>().sprite = buttonSprites[1].sprite;
        GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSprites[1].size.x, buttonSprites[1].size.y);
    }

    public void isXbox()
    {
        GetComponent<Image>().sprite = buttonSprites[2].sprite;
        GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSprites[2].size.x, buttonSprites[2].size.y);
    }




}
[System.Serializable]
public class ButtonSpriteData
{
    public Sprite sprite;
    public Vector2 size; // size.x = width, size.y = height
}
