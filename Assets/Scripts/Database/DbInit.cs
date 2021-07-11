using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DbInit : MonoBehaviour
{
        public UnityEvent OnInitialized = new UnityEvent();
        public InitializationFailedEvent OnInitializationFailed = new InitializationFailedEvent();
    private async void Awake()
    {
        await Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.Exception != null)
            {
                OnInitializationFailed.Invoke(task.Exception);
            }
            else
            {
                Debug.Log("Initialized Firebase Db");
                OnInitialized.Invoke();
            }
        });

        DbCommands.CreateDbAndTable();
        await DbCommands.InsertUpdateDataToFirebase();
    }

    [Serializable]
    public class InitializationFailedEvent : UnityEvent<Exception>
    {
    }
}
