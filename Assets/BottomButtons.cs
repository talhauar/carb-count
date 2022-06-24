using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities;

public class BottomButtons : AutoSingleton<BottomButtons> 
{
    public List<Image> buttonsImages;
    
    public List<TMP_Text> buttonTexts;
    public List<GameObject> objectsToActivateOnClick;
    public TMP_Text topPageName;
    public GameObject AddFoodButton;
    public GameObject RefreshFoodButton;

    public Color clickedColor;

    private Color defaultColor;
    private int currentIndex = -1;
    public bool ButtonsLocked { get; set; } = false;

    private void Awake()
    {
        defaultColor = buttonsImages[0].color;
        if (UserInfosData.Data.InfosValid()) OnClick(0);
        else 
        { 
            OnClick(2);
            ButtonsLocked = true;
        }
    }

    public void OpenMainPage()
    {
        OnClick(0);
    }


    public void OnClick(int buttonIndex)
    {
        if (currentIndex == buttonIndex) return;
        if (ButtonsLocked) return;
        ReserColorToDefaultAll();
        currentIndex = buttonIndex;
        topPageName.text = buttonTexts[currentIndex].text;
        buttonsImages[currentIndex].color = clickedColor;
        buttonTexts[currentIndex].color = clickedColor;
        objectsToActivateOnClick[currentIndex].SetActive(true);
        AddFoodButton.SetActive(buttonIndex == 1);
        RefreshFoodButton.SetActive(buttonIndex == 1);
        
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
