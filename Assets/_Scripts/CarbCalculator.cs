using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbCalculator : MonoBehaviour
{
    [SerializeField] string successProfileHeader;
    [TextArea] [SerializeField] string successProfileBody;
    public static bool CalculateCarbNeeded(out int carbsNeededPerDay)
    {
        carbsNeededPerDay = -1;
        if (!UserInfosData.Data.InfosValid()) return false;

        if (!UserInfosData.Data.isMale)
        {
            float BMR = 655.1f + (9.563f * UserInfosData.Data.Weight) + (1.850f * UserInfosData.Data.Height) - (4.676f * UserInfosData.Data.Age);
            carbsNeededPerDay = (int)(BMR / 8);
            UserInfosData.Data.carbPerDay = carbsNeededPerDay;
            UserInfosData.Data.Save();
        }
        else
        {
            float BMR = 66.47f + (13.75f * UserInfosData.Data.Weight) + (5.003f * UserInfosData.Data.Height) - (6.755f * UserInfosData.Data.Age);
            carbsNeededPerDay = (int)(BMR / 8);
            UserInfosData.Data.carbPerDay = carbsNeededPerDay;
            UserInfosData.Data.Save();
        }
        return true;
    }

    public void OnCalculateCarb()
    {
        if (CalculateCarbNeeded(out int carbNeededPerDay))
        {
            BottomButtons.Instance.ButtonsLocked = false;
            string headerText = successProfileHeader;
            string bodyText = successProfileBody.Replace("*", UserInfosData.Data.carbPerDay.ToString());
            Debug.Log(bodyText);
            PopupManager.Instance.CreatePopup(headerText, bodyText, "Tamam", BottomButtons.Instance.OpenMainPage);
        }
        else
        {
            PopupManager.Instance.CreatePopup("Bilgiler Hatal? !", "Ki?isel bilgilerinizin do?ru oldu?una lütfen emin olun.", "Tamam", null);
        }
    }
    
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
