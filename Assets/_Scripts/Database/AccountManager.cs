using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using UnityEngine;
using Utilities;
using System.Linq;

[CreateAssetMenu(menuName = "ScriptableObjects/Singletons/AccountManager")]
public class AccountManager : ScriptableSingleton<AccountManager>
{
    private static AccountManagerCallbackProvider callbackProvider;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        Instance.CreateCallbackProvider();
    }

    private void CreateCallbackProvider()
    {
        callbackProvider = new GameObject().AddComponent<AccountManagerCallbackProvider>();
        DontDestroyOnLoad(callbackProvider.gameObject);
    }

    public void CheckUser() 
    {
        if (IsUserExists())
        {
            if (IsUserHaveLocalData())
            {
                return;
            }
            MatchDataFromBackend();
            return;
        }
            
        if (IsUserHaveLocalData())
        {
            SetDataInDatabase();
            return;
        }

        if (UserData.Data.Username.Equals(""))
            CreateUser();           
    }

    private bool IsUserHaveLocalData()
    {
        if (UserData.Data.Username.Equals(""))
            return false;

        return true;     
    }  

    private void CreateUser()
    {
        UserData.Data.Id = SystemInfo.deviceUniqueIdentifier;
        UserData.Data.Username = SystemInfo.deviceName + "#" + RandomUserTag();
        UserData.Data.DeviceModel = SystemInfo.deviceModel;

        Debug.Log("Creating New User: " + UserData.Data.Username);
        DatabaseManager.Instance.InsertRecord(TableNameManager.Instance.UserDataTable, UserData.Data);
        UserData.Data.Save();
    }

    private static string RandomUserTag()
    {
        System.Random random = new System.Random();
        int length = 6;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private bool IsUserExists()
    {
        return DatabaseManager.Instance.IsUserExistInDB(TableNameManager.Instance.UserDataTable, SystemInfo.deviceUniqueIdentifier);
    }

    private static void MatchDataFromBackend()
    {
        UserData record = DatabaseManager.Instance.LoadRecord(TableNameManager.Instance.UserDataTable, SystemInfo.deviceUniqueIdentifier);
           
        Debug.Log("User in database. but not found local data. Matching data.");
        UserData.Data.Id = record.Id;
        UserData.Data.Username = record.Id;
        UserData.Data.DeviceModel = record.DeviceModel;       
    }

    private static void SetDataInDatabase()
    {
        Debug.Log("Set User User: " + UserData.Data.Username);
        DatabaseManager.Instance.InsertRecord(TableNameManager.Instance.UserDataTable, UserData.Data);
        UserData.Data.Save();
    }
}

public class AccountManagerCallbackProvider : MonoBehaviour
{
    AccountManager instance;

    private void Awake()
    {
        instance = AccountManager.Instance;
    }
}

public class UserData : Saveable<UserData>
{
    [BsonId]
    public string Id { get; set; }
    public string Username { get; set; } = "";
    public string DeviceModel { get; set; } = "";
}

