using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class DbCommands : MonoBehaviour
{
    string connectionString;
    private void Start()
    {
        connectionString = "URI=file:" + Application.persistentDataPath + "/Database/score.db";
        Debug.Log(connectionString);
        CreateDbAndTable();

        InsertScore("test", "90,123124");
    }
    public void CreateDbAndTable()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS MsScore(Id INTEGER PRIMARY KEY AUTOINCREMENT, Word VARCHAR(100) NOT NULL, Score DOUBLE NOT NULL, InputDatetime DATETIME NOT NULL);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void InsertScore(string word, string score)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO MsScore (Word, Score, InputDatetime) VALUES ('"+word+"','"+score+"', '"+DateTime.Now+"');";
                Debug.Log(command.CommandText);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
