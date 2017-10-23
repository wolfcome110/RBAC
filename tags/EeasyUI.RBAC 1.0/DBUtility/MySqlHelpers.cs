using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;

namespace DBUtility
{
    public abstract class MySqlHelpers
    {
        public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.ConnectionStrings["IceMySql"].ConnectionString;


        /// <summary>
        /// Execute a MySqlCommand (that returns no resultset) against the database specified in the connection string
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid connection string for a MySqlConnection</param>
        /// <param name="cmdType">the CommandType (e.g. stored procedure)</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>an int representing the numbers of rows effected by the command</returns>
        public static int ExecuteNonQuery(string connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //MySqlCommand
            //MySqlConnection
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        
        public static int ExecuteNonQuery(MySqlTransaction tran, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(tran.Connection, tran, cmd, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, CommandType.Text, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }

        }
        /// <summary>
        /// Execute a MySqlCommand that returns a resultset aginst the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <param name="connection">a valid connection string of a MySqlConnection</param>
        /// <param name="cmdType">the CommandType(e.g.:stored Procedure)</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the results</returns>
        public static MySqlDataReader ExecuteReader(string connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionStringLocalTransaction);
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                //MySqlDataReader
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                MySqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return sdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }


        /// <summary>
        /// Execute a MySqlCommand that returns the first column of the first record aginst then database specified in the connection string
        /// using the provided parameters
        /// </summary>
        /// <param name="connection">a valid connection string for a MySqlConnection</param>
        /// <param name="cmdType">the CommandType (e.g.:stored procedure</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>A object that should be convert to expected type using Convert.To{type}</returns>
        public static object ExecuteScalar(string connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                return val;
            }
        }

        public static bool Exists(string cmdText, MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, CommandType.Text, cmdText, commandParameters);
                int val = Convert.ToInt32(cmd.ExecuteScalar());
                if (val > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// ÷¥––≤È—Ø”Ôæ‰£¨∑µªÿDataSet
        /// </summary>
        /// <param name="SQLString">≤È—Ø”Ôæ‰</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionStringLocalTransaction))
            {
                DataSet ds = new DataSet();

                //MySqlDataAdapter
                connection.Open();
                MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");

                return ds;
            }
        }

        /// <summary>
        /// ÷¥––≤È—Ø”Ôæ‰£¨∑µªÿDataSet
        /// </summary>
        /// <param name="SQLString">≤È—Ø”Ôæ‰</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionStringLocalTransaction))
            {
                MySqlCommand cmd = new MySqlCommand();
                //PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                PrepareCommand(conn, null, cmd, CommandType.Text, SQLString, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();

                    return ds;
                }
            }
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="conn">MySqlConnection Object </param>
        /// <param name="tran">MySqlTransaction Object </param>
        /// <param name="cmd">MySqlCommand Object </param>
        /// <param name="cmdType">Cmd Type e.g . stored procedure or text</param>
        /// <param name="cmdText">CommandText e.g. Select * From Products</param>
        /// <param name="cmdParms">MySqlParameter to use in the command</param>
        private static void PrepareCommand(MySqlConnection conn, MySqlTransaction tran, MySqlCommand cmd, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            //MySqlParameter
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (tran != null)
            {
                cmd.Transaction = tran;
            }
            if (cmdParms != null)
            {
                foreach (MySqlParameter parms in cmdParms)
                {
                    cmd.Parameters.Add(parms);
                }
            }
        }

    }
}
