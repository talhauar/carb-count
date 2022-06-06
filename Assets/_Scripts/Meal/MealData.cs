using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MealData : Saveable<MealData>
{
    public List<FoodMeal> FoodHistory = new List<FoodMeal>();
}