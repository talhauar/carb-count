using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[CreateAssetMenu(menuName = "ScriptableObjects/Singletons/InternetManager")]
public class InternetManager : ScriptableSingleton<InternetManager>
{
    [NonSerialized] public NetworkType CurrentNetworkType = NetworkType.None;

    [NonSerialized] private static InternetManagerCallbackProvider callbackProvider;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        Instance.CreateCallbackProvider();
        Instance.CurrentNetworkType = Instance.CheckInternet();
        Instance.SetInternetType(Instance.CurrentNetworkType);
    }

    private void CreateCallbackProvider()
    {
        callbackProvider = new GameObject().AddComponent<InternetManagerCallbackProvider>();
        DontDestroyOnLoad(callbackProvider.gameObject);
    }

    public void Update()
    {
        NetworkType networkType = CheckInternet();
        CheckInternetType(networkType);
    }

    private NetworkType CheckInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return NetworkType.NotReachable;
        }

        return NetworkType.Reachable;
    }

    private void CheckInternetType(NetworkType networkType)
    {
        if (CurrentNetworkType == networkType)
            return;

        if(networkType != CurrentNetworkType)
        {
            CurrentNetworkType = networkType;
            SetInternetType(CurrentNetworkType);
            return;
        }
    }

    private void SetInternetType(NetworkType networkType)
    {
        switch (networkType)
        {
            case NetworkType.Reachable:
                Connected();
                break;
            case NetworkType.NotReachable:
                Disconnected();
                break;
            default:
                break;
        }
    }

    private void Connected()
    {
        Debug.Log("Connected Network");
        DatabaseManager.Instance.ConnectDatabase(TableNameManager.Instance.DatabaseTable);
    }

    private void Disconnected()
    {
        Debug.Log("Disconnected Network");
        DatabaseManager.Instance.Disconnect();
    }
}

public class InternetManagerCallbackProvider : MonoBehaviour
{
    InternetManager instance;

    private void Awake()
    {
        instance = InternetManager.Instance;
    }

    private void Update()
    {
        instance.Update();
    }
}

public enum NetworkType
{
    Reachable,
    NotReachable,
    None
}
