using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealsHistoryRect : Utilities.AutoSingleton<MealsHistoryRect> 
{
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private GameObject foodElementPrefab;

    private void OnEnable()
    {
        UpdateFoodHistory();
    }

    public void UpdateFoodHistory()
    {
        foreach (Transform child in contentRect)
        {
            Destroy(child.gameObject);
        }
        
        if (!AllFoodsManager.Instance.Ready) return;
        
        foreach (FoodMeal meal in MealData.Data.FoodHistory)
        {
            Instantiate(foodElementPrefab, contentRect).GetComponent<MealElement>().SetupMeal(meal);
        }
    }
}
