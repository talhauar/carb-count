using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AllMealsData : Saveable<AllMealsData>
{
    public List<Food> AllFoods = new List<Food>();
}
