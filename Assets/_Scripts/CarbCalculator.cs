using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbCalculator : MonoBehaviour
{
    public static int CarbCount(FoodAmountType type, int amount, float carbPerGram, float carbPerSession)
    {
        int multiplier = 1;
        bool returnGram = true;
        Debug.Log("CarbCalculator.CarbCount():"+type);
        switch (type)
        {
            case FoodAmountType.Gram:
                //
                break;
            case FoodAmountType.Kilogram:
                multiplier = 1000;
                break;
            case FoodAmountType.Mililitre:
                //
                break;
            case FoodAmountType.Litre:
                multiplier = 1000;
                break;
            case FoodAmountType.Porsiyon:
                returnGram = false;
                break;
            case FoodAmountType.Adet:
                returnGram = false;
                break;
            case FoodAmountType.Bardak:
                returnGram = false;
                break;
            default:
                break;
        }
        
        if(returnGram)return (int)(amount * multiplier * carbPerGram);
        else return (int)(amount * carbPerSession);
    }
}
