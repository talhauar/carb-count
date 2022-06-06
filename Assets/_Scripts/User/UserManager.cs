using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class UserManager : AutoSingleton<UserManager>
{
    public void SetUserData(UserInfosData userData)
    {
        UserInfosData.Data.Name = userData.Name;
        UserInfosData.Data.Surname = userData.Surname;
        UserInfosData.Data.Age = userData.Age;
        UserInfosData.Data.Weight = userData.Weight;
        UserInfosData.Data.Height = userData.Height;
        UserInfosData.Data.Save();
    }

    public void DeleteUserData()
    {
        UserInfosData.DeleteSave();
    }
}
