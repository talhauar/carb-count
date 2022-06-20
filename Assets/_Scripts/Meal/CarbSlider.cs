using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CarbSlider : MonoBehaviour
{
    [SerializeField] private Image sliderImage;
    [SerializeField] private TMPro.TMP_Text carbText;
    
    private int _carbEaten = 0;
    private float _fillAmount = 0; 
    private void OnEnable()
    {
        CountCarbEaten();

        carbText.text = _carbEaten.ToString() + " gr / " + UserInfosData.Data.carbPerDay.ToString()+" gr";
        sliderImage.fillAmount = 0;
        _fillAmount = _carbEaten / (float) UserInfosData.Data.carbPerDay;
    }

    private void Update()
    {
        if (sliderImage.fillAmount >= _fillAmount) return;
        sliderImage.fillAmount += _fillAmount * Time.deltaTime;
        if (sliderImage.fillAmount > _fillAmount) sliderImage.fillAmount = _fillAmount;
    }

    private void CountCarbEaten()
    {
        _carbEaten = 0;
        foreach (var meal in MealData.Data.FoodHistory)
        {
            _carbEaten += CarbCalculator.CarbCount(meal.amountType, meal.amount, meal.food.CarbPerGram, meal.food.CarbPerSession);
        }
    }
}
