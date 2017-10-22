using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;

using System.Collections;
using System.Data.SqlClient;

namespace DBUtility
{
    public abstract class SqlHelper
    {
        public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.ConnectionStrings["BakupOrRestore"].ConnectionString;


        /// <summary>
        /// Execute a sqlcommand (that returns no resultset) against the database specified in the connection string
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid connection string for a SqlConnection</param>
        /// <param name="cmdType">the CommandType (e.g. stored procedure)</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>an int representing the numbers of rows effected by the command</returns>
        public static int ExecuteNonQuery(string connection, CommandType cmdType, string cmdText, params System.Data.SqlClient.SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params System.Data.SqlClient.SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }



        public static int ExecuteNonQuery(SqlTransaction tran, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(tran.Connection, tran, cmd, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(string connection, string cmdText, CommandType cmdType, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, CommandType.Text, cmdText, null);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }

        }
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, CommandType.Text, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }

        }

        public static int ExecuteNonQuery(string connection, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                PrepareCommand(conn, null, cmd, CommandType.Text, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }

        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset aginst the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <param name="connection">a valid connection string of a SqlConnection</param>
        /// <param name="cmdType">the CommandType(e.g.:stored Procedure)</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand();

            try
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
        /// Execute a SqlCommand that returns the first column of the first record aginst then database specified in the connection string
        /// using the provided parameters
        /// </summary>
        /// <param name="connection">a valid connection string for a SqlConnection</param>
        /// <param name="cmdType">the CommandType (e.g.:stored procedure</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>A object that should be convert to expected type using Convert.To{type}</returns>
        public static object ExecuteScalar(string connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                return val;
            }
        }

        public static object ExecuteScalar(string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, CommandType.Text, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                return val;
            }
        }

        public static bool Exists(string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
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
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStringLocalTransaction))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
            {
                SqlCommand cmd = new SqlCommand();
                //PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                PrepareCommand(conn, null, cmd, CommandType.Text, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString,CommandType cmdType, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
            {
                SqlCommand cmd = new SqlCommand();
                //PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                PrepareCommand(conn, null, cmd, cmdType, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        public static DataSet Query(string connection, string SQLString, CommandType cmdType, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand();
                //PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                PrepareCommand(conn, null, cmd, cmdType, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="conn">SqlConnection Object </param>
        /// <param name="tran">SqlTransaction Object </param>
        /// <param name="cmd">SqlCommand Object </param>
        /// <param name="cmdType">Cmd Type e.g . stored procedure or text</param>
        /// <param name="cmdText">CommandText e.g. Select * From Products</param>
        /// <param name="cmdParms">SqlParameter to use in the command</param>
        private static void PrepareCommand(SqlConnection conn, SqlTransaction tran, SqlCommand cmd, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
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
                foreach (SqlParameter parms in cmdParms)
                {
                    cmd.Parameters.Add(parms);
                }
            }
        }




    }
}
