using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class UserManager : AutoSingleton<UserManager>
{
    public void SetUserData(UserData userData)
    {
        UserData.Data.Name = userData.Name;
        UserData.Data.Surname = userData.Surname;
        UserData.Data.Age = userData.Age;
        UserData.Data.Weight = userData.Weight;
        UserData.Data.Height = userData.Height;
        UserData.Data.Save();
    }

    public void DeleteUserData()
    {
        UserData.DeleteSave();
    }
}
