﻿using System.Data;
using Dapper;
using FluentAssertions;
using MySql.Data.MySqlClient;
using API_DB.Config;
using API_DB.Models;

namespace API_DB.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private IDbConnection _dbConnection;

        [SetUp]
        public void Setup()
        {
            _dbConnection = new MySqlConnection(AppConfig.ConnectionString);
            Log.Debug("Connection String: {ConnectionString}", AppConfig.ConnectionString);

            _dbConnection.Open();
        }

        [Test]
        public void GetFirstTenItems_ShouldReturnTenRecords()
        {
            // Arrange
            string sqlQuery = "SELECT * FROM items WHERE ID=34;";
            // Act
            var items = _dbConnection.Query<Item>(sqlQuery).ToList();

            Log.Information("Items: {@Items}", items);
            // Assert
            var expectedItem = new
            {
                ID = 34,
                Title = "Polo Shirt",
                Price = 65.00m,
                Size = 50,
                Category_Id = 5,
                Description = "Casual polo T-shirt",
                Image = "/images/shirt3.jpg"
            };

            // Assert
            items.Should().HaveCount(1, "потому что запрос по ID должен вернуть только одну запись");
            items.Should().ContainSingle().Which.Should().BeEquivalentTo(expectedItem);






        }


        [TearDown]
        public void TearDown()
        {
            _dbConnection?.Close();
            _dbConnection?.Dispose();
        }
    }
}
