using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ADONETExercises
{
    public class SqlCommands
    {
        private string query = string.Empty;
        private StringBuilder sb = new StringBuilder();
        public string VillainsNamesWithMoreThan3Minions(SqlConnection sqlConnection)
        {
            sb = new StringBuilder();
            query = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                      FROM Villains AS v 
                      JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                      GROUP BY v.Id, v.Name 
                      HAVING COUNT(mv.VillainId) > 3 
                      ORDER BY COUNT(mv.VillainId)";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                sb.Append($"{reader["Name"]} - {reader["MinionsCount"]}");
            }
            return sb.ToString().TrimEnd();
        }
        public string VillainsNameAndMinions(SqlConnection sqlConnection, int id)
        {
            sb = new StringBuilder();
            query = @$"SELECT Name FROM Villains WHERE Id = {id}";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sb.AppendLine($"{reader["Name"]}");
            }
            if (sb.ToString() == "")
            {
                return $"No villain with ID {id} exists in the database.";
            }
            reader.Close(); // closing first reader so the second can run

            int stringLengthBeforeMinions = sb.ToString().Length; // taking string length to check if there are minions
            query = $@"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = {id}
                                ORDER BY m.Name";
            cmd = new SqlCommand(query, sqlConnection);
            using SqlDataReader readerTwo = cmd.ExecuteReader();
            while (readerTwo.Read())
            {
                int i = 1;
                sb.AppendLine($"{i++}. {readerTwo["Name"]} {readerTwo["Age"]}");
            }
            int stringLengthAfterMinions = sb.ToString().Length;
            if (stringLengthBeforeMinions == stringLengthAfterMinions)
            {
                return $"(no minions)";
            }
            readerTwo.Close();
            return sb.ToString().TrimEnd();
        }
        public string AddMinionToVillain(SqlConnection sqlConnection, string minion, string villain)
        {
            StringBuilder sb = new StringBuilder();
            string[] minionInfo = minion.Split(": ", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            string minionName = minionInfo[0];
            int minionAge = int.Parse(minionInfo[1]);
            string minionTown = minionInfo[2];

            string[] villainInfo = villain.Split(": ", StringSplitOptions.RemoveEmptyEntries);
            string villainName = villainInfo[1];

            query = $@"SELECT Name FROM Towns WHERE Name = '{minionTown}'";
            SqlCommand townSearchCmd = new SqlCommand(query, sqlConnection);
            using SqlDataReader townReader = townSearchCmd.ExecuteReader();
            townReader.Read();
            var dbTownName = string.Empty;
            while (townReader.Read())
            {
                dbTownName = (string)townReader["Name"];
            }
            townReader.Close();

            query = $@"SELECT Name FROM Villains WHERE Name = '{villainName}'";
            SqlCommand villainNameCmd = new SqlCommand(query, sqlConnection);
            using SqlDataReader villainReader = villainNameCmd.ExecuteReader();
            var dbVillainName = string.Empty;
            while (villainReader.Read())
            {
                dbVillainName = (string)villainReader["Name"];
            }
            villainReader.Close();

            query = $@"SELECT * FROM Minions AS m JOIN Towns AS t ON m.TownId = t.Id";
            SqlTransaction transaction = sqlConnection.BeginTransaction();
            try
            {
                if (dbTownName == "")
                {
                    var insertTownQuery = $@"INSERT INTO Towns (Name) VALUES ('{minionTown}')";
                    SqlCommand insertTown = new SqlCommand(insertTownQuery, sqlConnection, transaction);
                    insertTown.ExecuteNonQuery();
                    sb.AppendLine($"Town {minionTown} was added to the database.");

                    var selectTownIdQuery = $@"SELECT Id FROM Towns WHERE Name = '{minionTown}'";
                    SqlCommand selectTownId = new SqlCommand(selectTownIdQuery, sqlConnection, transaction);
                    using SqlDataReader townIdReader = selectTownId.ExecuteReader();
                    townIdReader.Read();
                    int dbTownId = (int)townIdReader["Id"];
                    townIdReader.Close();

                    var insertMinionQuery = $@"INSERT INTO Minions (Name, Age, TownId) VALUES ('{minionName}', {minionAge}, {dbTownId})";
                    SqlCommand insertMinionInfo = new SqlCommand(insertMinionQuery, sqlConnection, transaction);
                    insertMinionInfo.ExecuteNonQuery();
                }
                if (dbVillainName == "")
                {
                    var insertVillainQuery = $@"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES ('{villainName}', 4)";
                    SqlCommand insertVillain = new SqlCommand(insertVillainQuery, sqlConnection, transaction);
                    insertVillain.ExecuteNonQuery();
                    sb.AppendLine($"Villain {villainName} was added to the database.");
                }

                var selectMinionIdQuery = $@"SELECT Id FROM Minions WHERE Name = '{minionName}' AND Age = {minionAge}";
                SqlCommand selectMinionId = new SqlCommand(selectMinionIdQuery, sqlConnection, transaction);
                using SqlDataReader minionIdReader = selectMinionId.ExecuteReader();
                minionIdReader.Read();
                var dbMinionId = (int)minionIdReader["Id"];
                minionIdReader.Close();

                var selectVillainIdQuery = $@"SELECT Id FROM Villains WHERE Name = '{villainName}'";
                SqlCommand selectVillainId = new SqlCommand(selectVillainIdQuery, sqlConnection, transaction);
                using SqlDataReader villaindIdReader = selectVillainId.ExecuteReader();
                villaindIdReader.Read();
                var dbVillainId = (int)villaindIdReader["Id"];
                villaindIdReader.Close();

                var insertMinionsVillainsQuery = $@"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES ({dbMinionId}, {dbVillainId})";
                SqlCommand insertMinionVillain = new SqlCommand(insertMinionsVillainsQuery, sqlConnection, transaction);
                insertMinionVillain.ExecuteNonQuery();
                sb.AppendLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message;
            }
            return sb.ToString().TrimEnd();
        }
        public void ChangeTownNamesCasing(SqlConnection sqlConnection, string countryName)
        {
            query = $@"SELECT Name
                       FROM Countries
                       WHERE Name = '{countryName}'";
            SqlCommand selectCountryName = new SqlCommand(query, sqlConnection);
            using SqlDataReader countryNameReader = selectCountryName.ExecuteReader();
            countryNameReader.Read();
            var country = countryNameReader["Name"];
            countryNameReader.Close();

            query = $@"SELECT t.Name 
                       FROM Towns as t
                       JOIN Countries AS c ON c.Id = t.CountryCode
                      WHERE c.Name = '{countryName}'";
            SqlCommand selectCountryTowns = new SqlCommand(query, sqlConnection);
            using SqlDataReader countryTownsReader = selectCountryTowns.ExecuteReader();
            List<string> townNames = new List<string>();
            while (countryTownsReader.Read())
            {
                townNames.Add((string)countryTownsReader["Name"]);
            }
            countryTownsReader.Close();
            if (country == "" || townNames.Count == 0)
            {
                Console.WriteLine("No town names were affected.");
            }
            else if (country != "" && townNames.Count > 0)
            {
                query = $@"UPDATE Towns
                           SET Name = UPPER(Name)
                         WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = '{countryName}')";
                SqlCommand updateTownNames = new SqlCommand(query, sqlConnection);
                var countOfUpdates = updateTownNames.ExecuteNonQuery();
                Console.WriteLine($"{countOfUpdates} town names were affected.");
                Console.WriteLine("[" + String.Join(", ", townNames) + "]");
            }
        }
        public string RemoveVillain(SqlConnection sqlConnection, int villainId)
        {
            sb = new StringBuilder();
            query = $@"SELECT Name FROM Villains WHERE Id = {villainId}";
            SqlCommand villainName = new SqlCommand(query, sqlConnection);
            using SqlDataReader villainNameReader = villainName.ExecuteReader();
            var name = string.Empty;
            while (villainNameReader.Read())
            {
                name = (string)villainNameReader["Name"];
            }
            villainNameReader.Close();

            if (name == "")
            {
                return "No such villain was found.";
            }

            query = $@"DELETE FROM MinionsVillains 
                       WHERE VillainId = {villainId}";
            SqlCommand deleteMinionsAndVillainsId = new SqlCommand(query, sqlConnection);
            var countOfDeletedMinions = deleteMinionsAndVillainsId.ExecuteNonQuery();

            query = $@"DELETE FROM Villains
                       WHERE Id = {villainId}";
            SqlCommand deleteVillain = new SqlCommand(query, sqlConnection);
            deleteVillain.ExecuteNonQuery();
            sb.AppendLine($"{name} was deleted");
            sb.AppendLine($"{countOfDeletedMinions} minions were released.");
            return sb.ToString().TrimEnd();
        }
    }
}