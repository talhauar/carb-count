using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealsHistoryRect : MonoBehaviour
{
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private GameObject foodElementPrefab;

    private void OnEnable()
    {
        foreach (Transform child in contentRect)
        {
            Destroy(child.gameObject);
        }
        
        UpdateFoodHistory();
    }

    public void UpdateFoodHistory()
    {
        foreach (FoodMeal meal in MealData.Data.FoodHistory)
        {
            Instantiate(foodElementPrefab, contentRect).GetComponent<FoodElement>().SetupMeal(meal);
        }
    }
}
