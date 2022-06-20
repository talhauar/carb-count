using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MealElement : MonoBehaviour
{
    [SerializeField] private RawImage foodImage;
    [SerializeField] private TMP_Text foodName;
    [SerializeField] private TMP_Text carbs;
    [SerializeField] private TMP_Text foodAmountText;

    public Food _food;
    private FoodMeal _meal;

    public void SetupMeal(FoodMeal meal)
    {
        _meal = meal;
        _food = meal.food;
        foodName.text = _food.FoodName;
        foodImage.texture = _food.GetTexture();
        foodAmountText.text = meal.amount.ToString() + " " + _meal.GetFoodUnitText();
        carbs.text = CarbCalculator.CarbCount(meal.amountType, meal.amount, _food.CarbPerGram, _food.CarbPerSession).ToString() + " g";
    }
    public void RemoveFromHistory()
    {
        MealData.Data.FoodHistory.Remove(_meal);
        MealData.Data.Save();
        GameObject.Destroy(gameObject);
    }
}
