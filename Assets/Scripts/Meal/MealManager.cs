using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MealManager : AutoSingleton<MealManager>
{
    public void AddMeal(string mealName, Food food)
    {
        foreach (var meal in MealData.Data.MealLists)
        {
            if (mealName == meal.MealName)
            {
                meal.foodList.Add(food);
                return;
            }
        }
        MealData.Data.MealLists.Add(new MealList(mealName, food));
    }

    public void DeleteMeal()
    {
        
    }
}
