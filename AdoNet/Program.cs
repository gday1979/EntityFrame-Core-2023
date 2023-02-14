using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace AdoNet
{
     public class Program
    {
        const string SqlConnectionString = "Server=.;Database=MinionsDB;Integrated Security=true";
        static void Main(string[] args)
        {
            using var connection = new SqlConnection(SqlConnectionString);
            connection.Open();
            string createDataBase = "CREATE DATABASE MinionsDB";
            using (var command = new SqlCommand(createDataBase, connection))
            {
                command.ExecuteNonQuery();
            }
            var createTableStatements = GetCreateTableStatements();
            foreach
                (var query in createTableStatements)
            {
                ExecuteNonQuery(connection, query);
            }
        }

        private static void ExecuteNonQuery(SqlConnection connection, string query)
        {
            using var command = new SqlCommand(query, connection);  
        }

        private static string[] GetCreateTableStatements()
        {
            var result = new string[]
            {
             "CREATE TABLE Countries(Id INT PRIMARY KEY,Name VARCHAR(50))","CREATE TABLE Towns (Id INT PRIMARY KEY,Name VARCHAR(50),CountryCode INT REFERENCES Countries(Id",
              "CREATE TABLE Minions(Id INT PRIMARY KEY,Name VARCHAR(50),Age INT,TownId INT REFERENCES Towns(Id))",

                "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY,Name VARCHAR(50))",
               "CREATE TABLE Vilians (Id INT PRIMARY KEY,Name VARCHAR(50),EvilnessFactorId INT REFERENCES EvilnessFactors(Id))"

            };
        }
    }
}
