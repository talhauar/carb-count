using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialDisplay : MonoBehaviour
{
    public Image fillImage;
    public TMP_Text valueText;

    public void SetFill(float current, float max)
    {
        fillImage.fillAmount = current / max;
        valueText.text = $"{current} / {max}";
    }
}
