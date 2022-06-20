using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BottomButtons : MonoBehaviour
{
    public List<Image> buttonsImages;
    
    public List<TMP_Text> buttonTexts;
    public List<GameObject> objectsToActivateOnClick;
    public TMP_Text topPageName;

    public Color clickedColor;

    private Color defaultColor;
    private int currentIndex = -1;

    private void Awake()
    {
        defaultColor = buttonsImages[0].color;
        OnClick(0);
    }


    public void OnClick(int buttonIndex)
    {
        if (currentIndex == buttonIndex) return;
        ReserColorToDefaultAll();
        currentIndex = buttonIndex;
        topPageName.text = buttonTexts[currentIndex].text;
        buttonsImages[currentIndex].color = clickedColor;
        buttonTexts[currentIndex].color = clickedColor;
        objectsToActivateOnClick[currentIndex].SetActive(true);
    }

    private void ReserColorToDefaultAll()
    {
        foreach (GameObject panel in objectsToActivateOnClick)
        {
            panel.SetActive(false);
        }
        foreach (var button in buttonsImages)
        {
            button.color = defaultColor;
        }
        foreach (var text in buttonTexts)
        {
            text.color = defaultColor;
        }
    }
}
