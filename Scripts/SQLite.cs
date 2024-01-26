using Mono.Data.Sqlite;
using System.Data;
using System.Collections;
using UnityEngine;

public class SQLite : MonoBehaviour
{
    //Set a connection to the place the database is stored
    SqliteConnection connection = new SqliteConnection("URI=file:" + Application.streamingAssetsPath + "/DataFile.db");

    private void Start()
    {
        //Gathers the data that is saved and uses it to start the game off with
        SetData();
    }

    private void Update()
    {
        //Update the data
        UpdateData();
    }

    private void SetData()
    {
        using (connection)
        {
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Currencies";

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    int value = reader.GetInt32(1);

                    //Switch statement to set the data for each currency
                    switch (name)
                    {
                        case "Dough":
                            GameManager.instance.dough = value;
                            break;
                        case "Wood":
                            GameManager.instance.wood = value;
                            break;
                        case "Apple":
                            GameManager.instance.apple = value;
                            break;
                        case "Bread":
                            GameManager.instance.bread = value;
                            break;
                        case "ApplePie":
                            GameManager.instance.applePie = value;
                            break;
                        default:
                            break;
                    }
                }
                reader.Close();
            }
            connection.Close();
        }
    }

    private void UpdateData()
    {
        using (connection)
        {
            //Open the connection
            connection.Open();

            //Using the command to allow changes to be made in the database, it will be filled 
            using (SqliteCommand command = connection.CreateCommand())
            {
                //a single command to insert or replace all values
                command.CommandText = "INSERT OR REPLACE INTO Currencies (name, value) VALUES " +
                #region values
                "('Dough', @dough), " +
                "('Wood', @wood), " +
                "('Apple', @apple), " +
                "('Bread', @bread), " +
                "('ApplePie', @applePie)";
                command.Parameters.AddWithValue("@dough", GameManager.instance.dough);
                command.Parameters.AddWithValue("@wood", GameManager.instance.wood);
                command.Parameters.AddWithValue("@apple", GameManager.instance.apple);
                command.Parameters.AddWithValue("@bread", GameManager.instance.bread);
                command.Parameters.AddWithValue("@applePie", GameManager.instance.applePie);
                command.ExecuteNonQuery();
                #endregion
            }

            //Close the connection
            connection.Close();
        }

        DisplayData();
    }


    private void DisplayData()
    {
        using (connection)
        {
            //Open the connection
            connection.Open();

            //use the connection to create a command
            using (IDbCommand command = connection.CreateCommand())
            {
                //Gather everything from the assigned table
                command.CommandText = "SELECT * FROM Currencies";

                //Iterate through the now given selection in the table
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Debug.Log(reader["name"] + " == " + reader["value"]);
                    }
                    reader.Close();
                }
            }

            //Close the connection
            connection.Close();
        }
    }

    /// <summary>
    /// A function for the player to reset their stats if they want to restart
    /// </summary>
    public void ResetStats()
    {
        //Lazy way to reset stats
        #region Resetting stats
        GameManager.instance.dough = 0;
        GameManager.instance.wood = 0;
        GameManager.instance.apple = 0;
        GameManager.instance.bread = 0;
        GameManager.instance.applePie = 0;
        #endregion
    }
}