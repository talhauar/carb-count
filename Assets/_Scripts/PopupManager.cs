using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using DG.Tweening;
using System;
using UnityEngine.UI;
using System.Text;

public class PopupManager : AutoSingleton<PopupManager>
{
    [SerializeField] CanvasGroup group;
    [SerializeField] TMPro.TMP_Text header;
    [SerializeField] TMPro.TMP_Text bodyText;
    [SerializeField] TMPro.TMP_Text buttonText;
    [SerializeField] Button button;

    public void CreatePopup(string headerStr, string bodyStr, string buttonStr, Action onButton)
    {
        group.DOComplete();
        group.DOFade(1, .3f);
        group.blocksRaycasts = true;
        group.interactable = true;
        Debug.Log(bodyStr);
        header.text = headerStr;
        bodyText.text = bodyStr;
        buttonText.text = buttonStr;

        button.onClick.AddListener(() => { if (onButton != null) { onButton(); } ClosePopUp(); } );

        Debug.Log(bodyText.text);
    }

    private void ClosePopUp()
    {
        group.DOComplete();
        group.blocksRaycasts = false;
        group.interactable = false;
        button.onClick.RemoveAllListeners();
        group.DOFade(0, .3f);
    }
}
