using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using UnityEngine;
using Utilities;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "ScriptableObjects/Singletons/DatabaseConnection")]
public class DatabaseManager : ScriptableSingleton<DatabaseManager>
{
    public enum ConnectionType
    {
        Connected,
        NotConnected,
        None
    }

    public enum SendType
    {
        MainThread,
        Async
    }

    private static DatabaseConnectionCallbackProvider callbackProvider;

    private MongoClient _client;

    private IMongoDatabase _database;

    [NonSerialized] public ConnectionType CurrentDBConnectionType = ConnectionType.None;

    public bool IsConnected { get { return CurrentDBConnectionType == ConnectionType.Connected; } }

    public IMongoDatabase Database
    {
        get => _database;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        Instance.CreateCallbackProvider();
    }

    private void CreateCallbackProvider()
    {
        callbackProvider = new GameObject().AddComponent<DatabaseConnectionCallbackProvider>();
        DontDestroyOnLoad(callbackProvider.gameObject);
    }

    public void ConnectDatabase(string dbName)
    {
        try
        {
            if (InternetManager.Instance.CurrentNetworkType == NetworkType.NotReachable)
                return;

            _client = new MongoClient("mongodb+srv://CarbCount:mongodb321.@cluster0.s7hv463.mongodb.net/?retryWrites=true&w=majority");
            _database = _client.GetDatabase(dbName);
            CurrentDBConnectionType = ConnectionType.Connected;
            AccountManager.Instance.CheckUser();
            AllFoodsManager.DownloadAllFoods();
        }
        catch (Exception)
        {
            CurrentDBConnectionType = ConnectionType.NotConnected;
            throw;
        }
        
    }

    public void Disconnect()
    {
        CurrentDBConnectionType = ConnectionType.NotConnected;
    }

    public ConnectionType GetCurrentType()
    {
        return CurrentDBConnectionType;
    }

    public bool InsertRecord<T>(string table, T record, SendType type = SendType.Async)
    {
        if (!IsConnected) return false;

        var collection = _database.GetCollection<T>(table);

        if(type == SendType.Async)
        {
            collection.InsertOneAsync(record);
            
        }
        else
        {
            collection.InsertOne(record);
        }       
        return true;
    }

    public List<T> LoadRecords<T>(string table)
    {
        var collection = _database.GetCollection<T>(table);
        return collection.Find(new BsonDocument()).ToList();
    }

    public UserData LoadRecord(string table, string userID)
    {
        var collection = _database.GetCollection<UserData>(table);
        var filter = Builders<UserData>.Filter.Eq(x => x.Id, userID);
        return collection.Find(filter).FirstOrDefault();
    }

    public bool IsUserExistInDB(string table, string userID)
    {
        var collection = _database.GetCollection<UserData>(table);
        var filter = Builders<UserData>.Filter.Eq(x => x.Id, userID);
        var user = collection.Find(filter).FirstOrDefault();

        if (user != null)
            return true;

        return false;
    }
}

public class DatabaseConnectionCallbackProvider : MonoBehaviour
{
    DatabaseManager instance;

    private void Awake()
    {
        instance = DatabaseManager.Instance;
    }
}

