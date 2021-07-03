using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;

public static class DbCommands
{
    static string connectionString = "URI=file:" + Application.persistentDataPath + "/Database/score.db";
    //private void Start()
    //{
    //    Debug.Log(connectionString);
    //    CreateDbAndTable();

    //    //InsertScore("abc", "76.123124");
    //    //InsertScore("test", "88.5515");
    //    //InsertScore("abc", "98.123124");
    //    Debug.Log(GetScoresJson());
    //}

    //Create new table
    public static void CreateDbAndTable()
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

    //Insert word and corresponding score
    public static void InsertScore(string word, string score)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO MsScore (Word, Score, InputDatetime) VALUES ('"+word+"','"+score+"', '"+DateTime.Now+"');";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    /*
     * Gets all data and converts them into JSON. Format is like so:
     * {"Records":{"objectName":"abc","scores:"[76.123124,98.123124]},{"objectName":"test","scores:"[90.123124,90.123124,88.5515]}}
     */
    public static string GetScoresJson()
    {
        string returnText = "{\"Records\":";
        bool first = true;
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM MsScore ORDER BY WORD;";
                
                using (IDataReader reader = command.ExecuteReader())
                {
                    string currWord = "";
                    while (reader.Read())
                    {
                        if (!currWord.Equals(reader["Word"].ToString()) && !first)
                        {
                            returnText = returnText.Remove(returnText.Length - 1, 1);
                            returnText += "]},";
                            currWord = reader["Word"].ToString();
                            returnText += "{\"objectName\":" + "\"" + currWord + "\"" + ",\"scores\":[";
                            returnText += reader["Score"] + ",";
                        }
                        else if(!currWord.Equals(reader["Word"].ToString()))
                        {
                            currWord = reader["Word"].ToString();
                            returnText += "{\"objectName\":" + "\"" + currWord + "\"" + ",\"scores\":[";
                            returnText += reader["Score"] + ",";
                            first = false;
                        }
                        else
                        {
                            returnText += reader["Score"] + ",";
                        }
                        Debug.Log(currWord);
                        Debug.Log(reader["Word"]);
                    }
                }
            }
            returnText = returnText.Remove(returnText.Length - 1, 1);
            returnText += "]}}";

            connection.Close();
        }

        return returnText;
    }
}
