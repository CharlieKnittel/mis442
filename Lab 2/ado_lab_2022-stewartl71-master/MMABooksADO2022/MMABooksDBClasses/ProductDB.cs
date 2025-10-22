using MMABooksBusinessClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MMABooksDBClasses
{
    public static class ProductDB
    {
        public static Product GetProduct(string productCode)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement
                = "SELECT ProductCode, Description, OnHandQuantity, UnitPrice "
                + "FROM Products "
                + "WHERE ProductCode = @ProductCode";
            MySqlCommand selectCommand =
                new MySqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ProductCode", productCode);

            try
            {
                connection.Open();
                MySqlDataReader prodReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (prodReader.Read())
                {
                    Product product = new Product();
                    product.ProductCode = prodReader["ProductCode"].ToString();
                    product.Description = prodReader["Description"].ToString();
                    product.OnHandQuantity = (int)prodReader["OnHandQuantity"];
                    product.UnitPrice = (decimal)prodReader["UnitPrice"];
                    return product;
                }
                else
                {
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement = "SELECT ProductCode, Description, OnHandQuanity, UnitPrice "
                                   + "FROM Products "
                                   + "ORDER BY ProductCode";
            MySqlCommand selectCommand =
                new MySqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Product p = new Product();
                    p.ProductCode = reader["ProductCode"].ToString();
                    p.Description = reader["Description"].ToString();
                    p.OnHandQuantity = (int)reader["OnHandQuantity"];
                    p.UnitPrice = (decimal)reader["UnitPrice"];
                    products.Add(p);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return products;
        }

        public static string AddProduct(Product product)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string insertStatement =
                "INSERT Products " +
                "(ProductCode, Description, OnHandQuantity, UnitPrice) " +
                "VALUES (@ProductCode, @Description, @OnHandQuantity, @UnitPrice)";
            MySqlCommand insertCommand =
                new MySqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@ProductCode", product.ProductCode);
            insertCommand.Parameters.AddWithValue(
                "@Description", product.Description);
            insertCommand.Parameters.AddWithValue(
                "@OnHandQuantity", product.OnHandQuantity);
            insertCommand.Parameters.AddWithValue(
                "@UnitPrice", product.UnitPrice);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                // MySQL specific code for getting last pk value
                string selectStatement =
                    "SELECT LAST_INSERT_ID()";
                MySqlCommand selectCommand =
                    new MySqlCommand(selectStatement, connection);
                string productCode = selectCommand.ExecuteScalar().ToString();
                return productCode;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeleteProduct(Product product)
        {
            // get a connection to the database
            MySqlConnection connection = MMABooksDB.GetConnection();
            string deleteStatement =
                "DELETE FROM Products " +
                "WHERE ProductCode = @ProductCode " +
                "AND Description = @Description " +
                "AND OnHandQuantity = @OnHandQuantity " +
                "AND UnitPrice = @UnitPrice ";
            // set up the command object
            MySqlCommand deleteCommand =
                new MySqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            deleteCommand.Parameters.AddWithValue("@Description", product.Description);
            deleteCommand.Parameters.AddWithValue("@OnHandQuantity", product.OnHandQuantity);
            deleteCommand.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);

            try
            {
                // open the connection
                connection.Open();
                // execute the command
                int numRowsAffected = deleteCommand.ExecuteNonQuery();
                // if the number of records returned = 1, return true otherwise return false
                if (numRowsAffected == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                // close the connection
                connection.Close();
            }
        }
    }
}
