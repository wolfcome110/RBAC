using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web;

namespace DBUtility
{
    public class AccessHelper
    {
        //public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.ConnectionStrings["Ice"].ConnectionString;
        public static readonly string path = System.IO.Directory.GetCurrentDirectory();
        //public static readonly string ConnectionStringLocalTransaction = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA Source=" + System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + System.IO.Path.AltDirectorySeparatorChar + "App_Data/" + System.Configuration.ConfigurationManager.AppSettings["IceData"]);
        public static readonly string ConnectionStringLocalTransaction = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA Source=" + path + "/App_Data/" + System.Configuration.ConfigurationManager.AppSettings["IceData"];


        /// <summary>
        /// Execute a sqlcommand (that returns no resultset) against the database specified in the connection string
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid connection string for a OleDbConnection</param>
        /// <param name="cmdType">the CommandType (e.g. stored procedure)</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>an int representing the numbers of rows effected by the command</returns>
        public static int ExecuteNonQuery(string connection, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(OleDbTransaction tran,CommandType cmdType,string cmdText,params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(tran.Connection, tran, cmd, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(conn, null, cmd, CommandType.Text, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
 
        }
        /// <summary>
        /// Execute a OleDbCommand that returns a resultset aginst the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <param name="connection">a valid connection string of a OleDbConnection</param>
        /// <param name="cmdType">the CommandType(e.g.:stored Procedure)</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>A OleDbDataReader containing the results</returns>
        public static OleDbDataReader ExecuteReader(string connection, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbConnection conn = new OleDbConnection(connection);
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                OleDbDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
        /// Execute a OleDbCommand that returns the first column of the first record aginst then database specified in the connection string
        /// using the provided parameters
        /// </summary>
        /// <param name="connection">a valid connection string for a OleDbConnection</param>
        /// <param name="cmdType">the CommandType (e.g.:stored procedure</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>A object that should be convert to expected type using Convert.To{type}</returns>
        public static object ExecuteScalar(string connection, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                return val;
            }
        }

        public static bool Exists(string cmdText, OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(ConnectionStringLocalTransaction))
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
            using (OleDbConnection connection = new OleDbConnection(ConnectionStringLocalTransaction))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OleDbDataAdapter command = new OleDbDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.OleDb.OleDbException ex)
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
        public static DataSet Query(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection conn = new OleDbConnection(ConnectionStringLocalTransaction))
            {
                OleDbCommand cmd = new OleDbCommand();
                //PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                PrepareCommand(conn, null, cmd, CommandType.Text, SQLString, cmdParms);
                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.OleDb.OleDbException ex)
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
        /// <param name="conn">OleDbConnection Object </param>
        /// <param name="tran">OleDbTransaction Object </param>
        /// <param name="cmd">OleDbCommand Object </param>
        /// <param name="cmdType">Cmd Type e.g . stored procedure or text</param>
        /// <param name="cmdText">CommandText e.g. Select * From Products</param>
        /// <param name="cmdParms">OleDbParameter to use in the command</param>
        private static void PrepareCommand(OleDbConnection conn, OleDbTransaction tran, OleDbCommand cmd, CommandType cmdType, string cmdText, OleDbParameter[] cmdParms)
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
                foreach (OleDbParameter parms in cmdParms)
                {
                    cmd.Parameters.Add(parms);
                }
            }
        }

    }
}
