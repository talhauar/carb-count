using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllFoodsRect : Utilities.AutoSingleton<AllFoodsRect>
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

        foreach (Food food in AllFoodsManager.Instance.AllFoods)
        {
            Instantiate(foodElementPrefab, contentRect).GetComponent<FoodElement>().SetupFood(food);
        }
    }

    public void Search(string searchText)
    {
        foreach (Transform child in contentRect)
        {
            if (child.GetComponent<FoodElement>()._food.FoodName.ToLower().Contains(searchText.ToLower()))
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
