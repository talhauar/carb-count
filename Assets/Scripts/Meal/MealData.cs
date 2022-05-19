using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MealData : Saveable<MealData>
{
    public List<MealList> MealLists = new List<MealList>();
}

[Serializable]
public class MealList
{
    public string MealName;
    public List<Food> foodList = new List<Food>();

    public MealList(string mealName, Food food)
    {
        MealName = mealName;
        foodList.Add(food);
    }
}

/*
    //--> Öğlen
          - Et
          - Pilav
          - Yogurt
          
    Gibi doldurulcak diye düsündüm. tarih belirtmemize gerek yok zaten.
*/