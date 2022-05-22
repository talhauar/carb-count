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

    private int currentIndex = -1;

    private void Awake()
    {
        OnClick(0);
    }


    public void OnClick(int buttonIndex)
    {
        if (currentIndex == buttonIndex) return;
        WhiteAll();
        currentIndex = buttonIndex;
        topPageName.text = buttonTexts[currentIndex].text;
        buttonsImages[currentIndex].color = clickedColor;
        buttonTexts[currentIndex].color = clickedColor;
        objectsToActivateOnClick[currentIndex].SetActive(true);
    }

    private void WhiteAll()
    {
        foreach (GameObject panel in objectsToActivateOnClick)
        {
            panel.SetActive(false);
        }
        foreach (var button in buttonsImages)
        {
            button.color = Color.white;
        }
        foreach (var text in buttonTexts)
        {
            text.color = Color.white;
        }
    }
}
