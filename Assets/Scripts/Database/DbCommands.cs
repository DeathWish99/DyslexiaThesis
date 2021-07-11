using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.Networking;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;

public static class DbCommands
{
    static string connectionString = "URI=file:" + Application.persistentDataPath + "/Database/score.db";
    static string pathToPersistentDb = Application.persistentDataPath + "/Database";
    static string streamingAssetsPath;
    static FirebaseDatabase _db;
    //Create new table
    public static void CreateDbAndTable()
    {
#if UNITY_ANDROID
        streamingAssetsPath = "jar:file://" + Application.dataPath + "!/assets";
#elif UNITY_EDITOR
        streamingAssetsPath = Application.dataPath + "/StreamingAssets";
#endif
        if (File.Exists(streamingAssetsPath + "/score.db"))
        {
            Debug.Log("File Exists");
        }
        else
        {
            Debug.Log("File Does not Exist");
        }
        if (!File.Exists(pathToPersistentDb + "/score.db"))
        {
            System.IO.Directory.CreateDirectory(pathToPersistentDb);
            WWW loadDB = new WWW(streamingAssetsPath + "/score.db");

            DateTime start = DateTime.Now;
            while (!loadDB.isDone && DateTime.Now.Subtract(start).Seconds < 6) {}  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

            if(DateTime.Now.Subtract(start).Seconds >= 6)
            {
                throw new Exception("Db Connection Timed Out");
            }

            // then save to Application.persistentDataPath

            File.WriteAllBytes(pathToPersistentDb + "/score.db", loadDB.bytes);

        }
        string uniqueUserId = Guid.NewGuid().ToString();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS MsScore(Id INTEGER PRIMARY KEY AUTOINCREMENT, Word VARCHAR(100), Score DOUBLE, InputDatetime DATETIME);";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS User(UserId VARCHAR(50));";
                command.ExecuteNonQuery();

                command.CommandText = "SELECT COUNT(*) FROM User;";

                int rowCount = Convert.ToInt32(command.ExecuteScalar());
                if (rowCount == 0)
                {
                    Debug.Log("IM IN BABY");
                    command.CommandText = "INSERT INTO User (UserId) VALUES ('"+uniqueUserId+"')";
                    command.ExecuteNonQuery();
                }
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
                command.CommandText = "SELECT * FROM MsScore ORDER BY Word, InputDatetime;";
                
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
                    }
                }
            }
            returnText = returnText.Remove(returnText.Length - 1, 1);
            returnText += "]}}";

            connection.Close();
        }

        if (first)
        {
            return "{}";
        }
        return returnText;
    }

    //Insert or update data to cloud Firebase storage.
    public static async Task<bool> InsertUpdateDataToFirebase()
    {
        _db = FirebaseDatabase.DefaultInstance;

        string userScores = GetScoresJson();

        string userId = "";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT UserId FROM User LIMIT 1";

                userId = command.ExecuteScalar().ToString();
            }
        }

        if(userScores == "{}") { return false; }
        var splittedUserScore = userScores.Split(new[] { ':' }, 2);
        string combinedData = splittedUserScore[0] + ":["+ splittedUserScore[1].Substring(0, splittedUserScore[1].Length - 1) + "]}";

        Debug.Log(combinedData);

        await _db.GetReference(userId).SetRawJsonValueAsync(combinedData);

        return true;
    }
}