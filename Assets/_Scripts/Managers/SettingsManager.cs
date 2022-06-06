using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


[CreateAssetMenu(menuName = "ScriptableObjects/Singletons/SettingsManager")]
public class SettingsManager :ScriptableSingleton<SettingsManager>
{
    [Header("Sound Options")]
    [SerializeField] private float maxSoundValue;
    [SerializeField] private float minSoundValue;
    [Header("Music Options")]
    [SerializeField] private float maxMusicValue;
    [SerializeField] private float minMusicValue;

    public float MaxSoundValue { get => maxSoundValue; }
    public float MinSoundValue { get => minSoundValue; }
    public float MaxMusicValue { get => maxMusicValue; }
    public float MinMusicValue { get => minMusicValue; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        Instance.IsSettingsExists();
    }

    #region PRIVATE METHODS

    private void IsSettingsExists()
    {
        if (SettingsData.Data.soundValue == -1f)
        {
            Debug.Log("Dataaa");
            SetDefaultSettingsValues();
        }            
    }

    #endregion

    #region PUBLIC METHODS
    public void ChangeSoundValue(float value)
    {
        //AudoManager

        SettingsData.Data.Save();
    }

    public void ChangeMusicValue(float value)
    {
        //TODO: AudioManager
        SettingsData.Data.Save();
    }

    public void ChangeNotificaitionPermission(bool permission)
    {
        SettingsData.Data.notification = permission;
        SettingsData.Data.Save();
    }

    public SettingsData GetSettingsData()
    {
        return SettingsData.Data;
    }
    public void SetDefaultSettingsValues()
    {
        SettingsData.Data.soundValue = 0.5f;
        SettingsData.Data.musicValue = 0.5f;
        SettingsData.Data.notification = true;
        SettingsData.Data.Save();
    }
    #endregion

    [Serializable]
    public class SettingsData : Saveable<SettingsData>
    {
        public float soundValue = -1f;
        public float musicValue;
        public bool notification;
    }
}



