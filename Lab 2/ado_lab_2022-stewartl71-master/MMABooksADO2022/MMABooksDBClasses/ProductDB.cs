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
    }
}
