using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameWelcome : MonoBehaviour
{
    [SerializeField] string preText;
    [SerializeField] string postText;
    [SerializeField] TMPro.TMP_Text Text;


    private void OnEnable()
    {
        Text.text = preText + UserInfosData.Data.Name + postText;
    }
}
