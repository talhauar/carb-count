using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using MongoDB.Bson.Serialization.Attributes;

[CreateAssetMenu(menuName = "ScriptableObjects/Singletons/GameSessionManager")]
public class GameSessionManager : ScriptableSingleton<GameSessionManager>
{
    [NonSerialized] public static GameSession CurrentGameSession;

    private static GameSessionManagerCallbackProvider callbackProvider;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        CurrentGameSession = new GameSession();
        Instance.CreateCallbackProvider();
        callbackProvider.StartSendSavedSessionsRoutine();
    }

    private void CreateCallbackProvider()
    {
        callbackProvider = new GameObject().AddComponent<GameSessionManagerCallbackProvider>();
        DontDestroyOnLoad(callbackProvider.gameObject);
    }

    public void OnApplicationQuit()
    {
        CurrentGameSession.SetEndTime();

        if (!SendSession(CurrentGameSession, DatabaseManager.SendType.MainThread))
        {
            GameSessionData.Data.sessionsToSend.Add(CurrentGameSession);
            GameSessionData.Data.Save();
        }
        else Debug.Log(JsonUtility.ToJson(CurrentGameSession));
    }

    public bool SendSession(GameSession session, DatabaseManager.SendType sendType = DatabaseManager.SendType.Async)
    {
        return DatabaseManager.Instance.InsertRecord<GameSession>(TableNameManager.Instance.GameSessionTable, session, sendType);
    }
}

public class GameSessionManagerCallbackProvider : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        GameSessionManager.Instance.OnApplicationQuit();
    }

    public void StartSendSavedSessionsRoutine()
    {
        if (GameSessionData.Data.sessionsToSend.Count == 0) return;
        InvokeRepeating(nameof(TrySendSaveSessions), 6, 6);
    }

    private void TrySendSaveSessions()
    {
        if (!DatabaseManager.Instance.IsConnected) return;
        List<GameSession> sessionsCopy = new List<GameSession>(GameSessionData.Data.sessionsToSend);
        for (int i = 0; i < sessionsCopy.Count; i++)
        {
            Debug.Log("Sending saves game session");
            if (GameSessionManager.Instance.SendSession(sessionsCopy[0]))
            {
                Debug.Log(sessionsCopy[0]);
                GameSessionData.Data.sessionsToSend.RemoveAt(0);  //if success
                sessionsCopy.RemoveAt(0);
            }
            else break;
            // "break" if fails
        }
        GameSessionData.Data.Save();
        if (GameSessionData.Data.sessionsToSend.Count == 0) CancelInvoke();
    }
}

public class GameSessionData : Saveable<GameSessionData>
{
    public List<GameSession> sessionsToSend = new List<GameSession>();
}

[Serializable]
public class GameSession
{
    private DateTime startDateTime;

    public string userId;

    public int secondsPlayed;
    public string startDate;
    public string startTime;
    public string endTime;

    public GameSession()
    {
        SetStartDateTime();
        SetUserId();
    }

    private void SetUserId()
    {
        userId = UserData.Data.Id;
    }

    private void SetStartDateTime()
    {
        startDateTime = DateTime.Now;
        startDate = DateTime.Now.ToString("dd/MM/yyyy");
        startTime = DateTime.Now.ToString("HH:mm:ss");
    }

    public void SetEndTime()
    {
        endTime = DateTime.Now.ToString("HH:mm:ss");
        secondsPlayed = (DateTime.Now - startDateTime).Seconds;
    }
}
