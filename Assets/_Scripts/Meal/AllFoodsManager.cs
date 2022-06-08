using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEditor;
using MongoDB.Bson;

[CreateAssetMenu(menuName = "ScriptableObjects/Singletons/AllFoodsManager")]
public class AllFoodsManager : Utilities.ScriptableSingleton<AllFoodsManager>
{
    [SerializeField] private List<Food> UploadDataList = new List<Food>();

    public List<Food> AllFoods = new List<Food>();

    [NonSerialized] public bool Ready = false;

    public static void DownloadAllFoods()
    {
        List<FoodData> allFoodDatas = DatabaseManager.Instance.LoadRecords<FoodData>(TableNameManager.Instance.AllFoodsTable);

        Instance.AllFoods.Clear();
        foreach (FoodData foodData in allFoodDatas)
        {
            Food dowloadedFood = foodData.ToFood();
            Instance.AllFoods.Add(dowloadedFood);
            Debug.Log(JsonUtility.ToJson(dowloadedFood));
        }
        Instance.Ready = true;
        if (MealsHistoryRect.InstanceExists) MealsHistoryRect.Instance.UpdateFoodHistory();
    }


    [ContextMenu("Upload Foods")]
    public void UploadFoodDatas()
    {
        MongoClient _client = new MongoClient("mongodb+srv://CarbCount:mongodb321.@cluster0.s7hv463.mongodb.net/?retryWrites=true&w=majority");
        IMongoDatabase _database = _client.GetDatabase(TableNameManager.Instance.DatabaseTable);

        var collection = _database.GetCollection<FoodData>(TableNameManager.Instance.AllFoodsTable);

        collection.DeleteMany(new BsonDocument());

        foreach (Food food in UploadDataList)
        {
            FoodData foodData = food.ToFoodData();
            Debug.Log(foodData.Id);

            Debug.Log("Food Data: " + JsonUtility.ToJson(foodData));
            var foodsWithSameName = collection.Find(Builders<FoodData>.Filter.Eq(x => x.FoodName, foodData.FoodName));
            if (foodsWithSameName.CountDocuments() == 0)
            {
                Debug.Log("Uploaded New Food Data for : " + foodData.FoodName);
                collection.InsertOneAsync(foodData);
            }
            else
            {
                collection.FindOneAndReplace(Builders<FoodData>.Filter.Eq(x => x.FoodName, foodData.FoodName), foodData);
                Debug.Log("Replaced Food Data for : " + foodData.FoodName);
            }
        }
    }


    [ContextMenu("Delete All Datas in foodTable")]
    public void DropFoodTable()
    {
        MongoClient _client = new MongoClient("mongodb+srv://CarbCount:mongodb321.@cluster0.s7hv463.mongodb.net/?retryWrites=true&w=majority");
        IMongoDatabase _database = _client.GetDatabase(TableNameManager.Instance.DatabaseTable);

        var collection = _database.GetCollection<FoodData>(TableNameManager.Instance.AllFoodsTable);
        collection.DeleteMany(new BsonDocument());
    }
}