using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
namespace DBUtility
{
    /// <summary>
    /// 
    /// </summary>
    public class OracleHelper
    {
        static OracleHelper()
        {

        }
        public static readonly string CnnString = GetCnnFromWebConfig();

        public static string GetCnnFromWebConfig()
        {
            string strConn = ConfigurationManager.ConnectionStrings["IceOracle"].ConnectionString;
            return strConn;
        }
        public static OracleConnection GetCnn()
        {
            return GetCnn(CnnString);
        }

        public static OracleConnection GetCnn(string cnnString)
        {
            return new OracleConnection(cnnString);
        }

        public static object ExecuteScalar(string sql, OracleParameter[]
            parameters)
        {
            OracleConnection cnn = new OracleConnection(OracleHelper.CnnString);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            foreach (OracleParameter parameter in parameters)
                cmd.Parameters.Add(parameter);
            try
            {
                cnn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("数据库错误：" + e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        /// <summary>
        /// 返回单一值执行SQL
        /// </summary>
        /// <param name="sql"></param>
        public static object ExecuteScalar(string sql)
        {
            OracleConnection cnn = new OracleConnection(OracleHelper.CnnString);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cmd.CommandTimeout = 1000;
            try
            {
                cnn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("数据库错误：" + e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        /// <summary>
        /// 无返回执行SQL
        /// </summary>
        /// <param name="sql"></param>
        public static int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, 100);
        }

        public static int ExecuteNonQuery(string sql, int timeOut)
        {
            OracleCommand cmd = OracleHelper.GetCnn().CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = timeOut;
            return ExecuteNonQuery(cmd);
        }

        public static int ExecuteNonQuery(string sql, OracleParameter[]
            parameters)
        {
            OracleCommand cmd = OracleHelper.GetCnn().CreateCommand();
            cmd.CommandText = sql;
            foreach (OracleParameter parameter in parameters)
                cmd.Parameters.Add(parameter);
            return ExecuteNonQuery(cmd);
        }

        public static int ExecuteNonQuery(OracleCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ep)
            {
                throw new Exception("数据库错误：" + ep.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// 执行Sql返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql)
        {
            OracleConnection cnn = new OracleConnection(OracleHelper.CnnString);
            OracleCommand cmd = cnn.CreateCommand();
            cmd.CommandText = sql;
            return ExecuteDataTable(cmd);
        }

        public static DataTable ExecuteDataTable(string sql, OracleParameter[]
            parameters)
        {
            OracleCommand cmd = OracleHelper.GetCnn().CreateCommand();
            cmd.CommandText = sql;
            foreach (OracleParameter parameter in parameters)
                cmd.Parameters.Add(parameter);
            return ExecuteDataTable(cmd);
        }

        public static DataTable ExecuteDataTable(OracleCommand cmd)
        {
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            try
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ep)
            {
                throw new Exception("数据库错误：" + ep.Message);
            }
        }

        /// <summary>
        /// 执行Sql返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql)
        {
            OracleConnection cnn = new OracleConnection(OracleHelper.CnnString);
            OracleCommand cmd = cnn.CreateCommand();
            cmd.CommandText = sql;
            return ExecuteDataSet(cmd);
        }

        public static DataSet ExecuteDataSet(string sql, OracleParameter[]
            parameters)
        {
            OracleCommand cmd = OracleHelper.GetCnn().CreateCommand();
            cmd.CommandText = sql;
            foreach (OracleParameter parameter in parameters)
                cmd.Parameters.Add(parameter);
            return ExecuteDataSet(cmd);
        }

        public static DataSet ExecuteDataSet(OracleCommand cmd)
        {
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            try
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ep)
            {
                throw new Exception("数据库错误：" + ep.Message);
            }
        }
        public static DataSet ExecuteDataSet(string sql, string tableName)
        {
            OracleConnection cnn = new OracleConnection(OracleHelper.CnnString);
            OracleCommand cmd = cnn.CreateCommand();
            cmd.CommandText = sql;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            try
            {
                DataSet ds = new DataSet();
                da.Fill(ds, tableName);
                return ds;
            }
            catch (Exception ep)
            {
                throw new Exception("数据库错误：" + ep.Message);
            }
        }
        public static DataSet ExecuteDataSet(string sql, string tableName, DataSet ds)
        {
            OracleConnection cnn = new OracleConnection(OracleHelper.CnnString);
            OracleCommand cmd = cnn.CreateCommand();
            cmd.CommandText = sql;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            try
            {
                da.Fill(ds, tableName);
                return ds;
            }
            catch (Exception ep)
            {
                throw new Exception("数据库错误：" + ep.Message);
            }
        }
        /// <summary>
        /// Will run a stored procedure, can only be called by those classes deriving
        /// from this base. It returns a OracleDataReader containing the result of the stored
        /// procedure.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of parameters to be passed to the procedure</param>
        /// <returns>A newly instantiated OracleDataReader object</returns>
        public static OracleDataReader GetDataReader(string sql)
        {
            System.Data.OracleClient.OracleCommand cmd = new OracleCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = 0;
            cmd.Connection = new OracleConnection(OracleHelper.CnnString);
            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.Connection.Open();
                OracleDataReader returnReader = cmd.ExecuteReader();
                return returnReader;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();

            }
        }


        ///// <summary>
        ///// Will run a stored procedure, can only be called by those classes deriving
        ///// from this base. It returns a OracleDataReader containing the result of the stored
        ///// procedure.
        ///// </summary>
        ///// <param name="storedProcName">Name of the stored procedure</param>
        ///// <param name="parameters">Array of parameters to be passed to the procedure</param>
        ///// <returns>A newly instantiated OracleDataReader object</returns>
        //public static OracleDataReader GetDataReader(string sql, OracleParameter[] parameters)
        //{
        //    System.Data.OracleClient.OracleCommand cmd = new OracleCommand();
        //    cmd.CommandText = sql;
        //    cmd.CommandTimeout = 0;
        //    cmd.Connection = new OracleConnection(Database.CnnString);
        //    cmd.CommandType = CommandType.Text;

        //    if (parameters != null)
        //    {
        //        for (int i = 0; i < parameters.Length; i++)
        //        {
        //            cmd.Parameters.Add(parameters[i]);
        //        }
        //    }
        //    try
        //    {
        //        cmd.Connection.Open();
        //        OracleDataReader returnReader = cmd.ExecuteReader();
        //        return returnReader;
        //    }
        //    catch(Exception ex )
        //    {
        //        cmd.Connection.Close();
        //        cmd.Connection.Dispose();
        //    }
        //}

        /// <summary>
        /// 获取某表的字段名称集合
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string[] GetTableFieldNames(string tableName)
        {
            OracleConnection cnn = new OracleConnection(OracleHelper.CnnString);
            OracleCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT TOP 0 * FROM " + tableName;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            try
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                string[] rs = new string[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    rs[i] = dt.Columns[i].ColumnName;
                }
                return rs;
            }
            catch (Exception ep)
            {
                throw new Exception("数据库错误：" + ep.Message);
            }
        }

        /// <summary>
        /// 获取数据库中所有表名
        /// </summary>
        /// <returns>id, Name</returns>
        public static DataTable GetTables()
        {
            return OracleHelper.ExecuteDataTable("SELECT id,name FROM sysobjects WHERE type='U' ORDER BY name");
        }

        /// <summary>
        /// 判断某表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool TableExist(string tableName)
        {
            int c = (int)OracleHelper.ExecuteScalar("SELECT COUNT(1) FROM sysobjects WHERE type='U' AND name='" + tableName + "'");
            return c == 1;
        }

        /// <summary>
        /// 获取某数据表在数据中的Id号
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static int TableIdByTableName(string tableName)
        {
            return (int)OracleHelper.ExecuteScalar("SELECT id FROM sysobjects WHERE type='U' AND name='" + tableName + "'");
        }

        /// <summary>
        /// 获取所有字段名称
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFields()
        {
            return OracleHelper.ExecuteDataTable("SELECT id,name FROM syscolumns");
        }

        /// <summary>
        /// 获取某表的字段名称
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetFields(string tableName)
        {
            return OracleHelper.ExecuteDataTable("SELECT id,name FROM syscolumns WHERE id=" + TableIdByTableName(tableName).ToString());
        }

        /// <summary>
        /// 获取所有函数的名称
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFunctions()
        {
            return OracleHelper.ExecuteDataTable("SELECT name FROM sysobjects WHERE type='TF' OR type='IF' OR type='FN' ORDER BY name");
        }

        /// <summary>
        /// 根据DataTable的结构创建数据表
        /// </summary>
        /// <param name="dt"></param>
        public static void CreateTable(string tableName, DataTable dt)
        {
            #region Hide
            DropTable(tableName);
            int dataType, dataLen1, dataLen2;
            bool isNull;
            string nullString;
            string sql = "CREATE TABLE " + tableName + "(";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql += (string)dt.Rows[i]["EName"];
                dataType = (int)dt.Rows[i]["DataType"];
                if (dt.Rows[i]["DataLen1"] == DBNull.Value) dataLen1 = 0;
                else dataLen1 = (int)dt.Rows[i]["DataLen1"];
                if (dt.Rows[i]["DataLen2"] == DBNull.Value) dataLen2 = 0;
                else dataLen2 = (int)dt.Rows[i]["DataLen2"];
                if (dt.Rows[i]["IsNull"] == DBNull.Value) isNull = true;
                else isNull = (bool)dt.Rows[i]["IsNull"];
                if (isNull) nullString = " NULL ";
                else nullString = " NOT NULL ";
                switch (dataType)
                {
                    case 1:
                        sql += " VARCHAR(" + dataLen1.ToString() + ")" + nullString +
                            ",";
                        break;
                    case 2:
                        sql += " INT" + nullString + " " + (string)dt.Rows[i]["Identity"]
                            + ",";
                        break;
                    case 3:
                        sql += " NUMERIC(" + dataLen1.ToString() + "," +
                            dataLen2.ToString()
                            + ")" + nullString + ",";
                        break;
                    case 4:
                        sql += " IMAGE" + nullString + ",";
                        break;
                    case 5:
                        sql += " DATETIME" + nullString + ",";
                        break;
                    case 6:
                        sql += " TIMESTAMP" + nullString + ",";
                        break;
                }
            }
            sql = sql.Substring(0, sql.Length - 1) + ")";
            OracleHelper.ExecuteNonQuery(sql);
            #endregion
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName"></param>
        public static void DropTable(string tableName)
        {
            if (TableExist(tableName))
            {
                OracleHelper.ExecuteNonQuery("Drop Table " + tableName);
            }
        }

        /// <summary>
        /// 创建主键
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keyName"></param>
        /// <param name="keyType"></param>
        public static void CreatePrimaryKey(string tableName, string keyName,
            string keyType)
        {
            string sql = "ALTER TABLE " + tableName + " ADD CONSTRAINT pk_" +
                tableName + keyName
                + " PRIMARY KEY  CLUSTERED(" + keyName + ") ON [PRIMARY]";
            OracleHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        public static void SetDefaultValue(string tableName, string
            fieldName, string defaultValue)
        {
            string sql = "ALTER TABLE " + tableName + " ADD CONSTRAINT DF_" +
                tableName + "_"
                + fieldName + " DEFAULT (" + defaultValue + ") FOR [" + fieldName +
                "]";
            OracleHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取某数据表的Schema结构
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetSchemaTable(string tableName)
        {
            OracleCommand cmd = GetCnn().CreateCommand();
            cmd.CommandText = "SELECT * FROM " + tableName;
            try
            {
                cmd.Connection.Open();
                OracleDataReader reader =
                    cmd.ExecuteReader(CommandBehavior.SchemaOnly);
                DataTable rs = reader.GetSchemaTable();
                reader.Close();
                return rs;
            }
            catch (Exception e)
            {
                throw new Exception("数据库错误：" + e.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// 从DataTable创建数据库表（目前忽略类型）
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        public static void CreateTableByDataTable(string tableName, DataTable
            dt)
        {
            string sql = "CREATE TABLE " + tableName + " (";
            object enable;
            foreach (DataColumn column in dt.Columns)
            {
                enable = column.ExtendedProperties["Enable"];
                if (enable != null && enable.Equals(false)) continue;
                sql += column.ColumnName + " CHAR(10),";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            OracleHelper.ExecuteNonQuery(sql);
        }

        public static void
            SetAdapterConnection(System.Data.OracleClient.OracleDataAdapter da)
        {
            SetAdapterConnection(da, GetCnn());
        }

        public static void
            SetAdapterConnection(System.Data.OracleClient.OracleDataAdapter da,
            OracleConnection cnn)
        {
            if (da.SelectCommand != null) da.SelectCommand.Connection = cnn;
            if (da.UpdateCommand != null) da.UpdateCommand.Connection = cnn;
            if (da.InsertCommand != null) da.InsertCommand.Connection = cnn;
            if (da.DeleteCommand != null) da.DeleteCommand.Connection = cnn;
        }

        /// <summary>
        /// 根据row插入数据表
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="row"></param>
        /// <param name="objAccess"></param>
        public static void InsertRecordByDataRow(string strTableName, DataRow
            row,
            string keyFieldName, bool createKey)
        {
            string strExtendSql = "";

            ArrayList objParams = new ArrayList();
            DataTable dt = row.Table;

            string strSql = "";

            bool autoID = false;

            if (createKey)
            {
                DataColumn keyColumn = dt.Columns[keyFieldName];
                if (keyColumn.DataType == typeof(int) || keyColumn.DataType == typeof(decimal))
                {
                    strSql = string.Format("DECLARE @{0} DECIMAL;SET @{0}=-1;SELECT @{0}=MAXID FROM GY_MAXID WHERE TABLENAME='{1}';"
                        + "IF @{0}=-1 BEGIN SET @{0}=1;INSERT INTO GY_MAXID(TABLENAME,MAXID) VALUES('{1}',2);END ELSE BEGIN UPDATE GY_MAXID SET MAXID=MAXID+1 WHERE TABLENAME='{1}';END;",
                        keyFieldName, strTableName);
                    autoID = true;
                }
            }

            strSql += "INSERT INTO " + strTableName + "(";
            string strValues = "VALUES(";
            object value;
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ExtendedProperties.Contains("ExtendField")) continue;
                value = row[column];
                if (value == DBNull.Value || value == null)
                {
                    continue;
                }
                //如果包含了扩展值的话,说明,这个值从扩展SQL中去取,而不直接从DR中去取.
                if (column.ExtendedProperties.Contains("ExtendValue"))
                {
                    strSql += column.ColumnName + ",";
                    strValues += "@" + column.ColumnName + ",";
                    strExtendSql += value.ToString();
                }
                else
                {
                    // 判断是否需要插入生成主键
                    if (autoID && column.ColumnName.ToLower() == keyFieldName.ToLower())
                    {
                        strSql += column.ColumnName + ",";
                        strValues += "@" + column.ColumnName + ",";
                    }
                    else
                    {
                        strSql += column.ColumnName + ",";
                        strValues += "@" + column.ColumnName + ",";
                        objParams.Add(new OracleParameter("@" + column.ColumnName, value));
                    }
                }
            }
            strSql = strSql.Substring(0, strSql.Length - 1) + ")"
                + strValues.Substring(0, strValues.Length - 1) + ")";
            strSql = strExtendSql + strSql;
            if (createKey)
            {
                strSql += string.Format("SELECT * FROM {0} WHERE {1}=@{1}",
                    strTableName, keyFieldName);
                DataTable dtTemp = ExecuteDataTable(strSql,
                    (OracleParameter[])objParams.ToArray(typeof(OracleParameter)));
                if (dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dtTemp.Columns.Contains(dt.Columns[i].ColumnName))
                        {
                            row[i] = dtTemp.Rows[0][dt.Columns[i].ColumnName];
                        }
                    }
                }
            }
            else
            {
                OracleParameter[] a = (OracleParameter[])objParams.ToArray(typeof(OracleParameter));

                ExecuteNonQuery(strSql,
                    (OracleParameter[])objParams.ToArray(typeof(OracleParameter)));
            }
        }

        /// <summary>
        /// 根据row更新数据表行
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="row"></param>
        /// <param name="objAccess"></param>
        /// <param name="strKeyFieldName"></param>
        public static void UpdateRecordByDataRow(string strTableName, DataRow
            row, string[] keyFieldNames)
        {
            string strExtendSql = "";
            if (keyFieldNames == null || keyFieldNames.Length < 1)
                throw new Exception("KeyFieldNames参数至少含有一个元素！");
            for (int i = 0; i < keyFieldNames.Length; i++)
            {
                if (row[keyFieldNames[i]] == DBNull.Value)
                    throw new Exception("KeyFieldNames参数不能是Null值！");
            }
            ArrayList objParams = new ArrayList();
            DataTable dt = row.Table;

            string strSql = "UPDATE " + strTableName + " SET ";
            object value;
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ExtendedProperties.Contains("ExtendField")) continue;
                if (column.ExtendedProperties.Contains("ExtendValue"))
                {
                    strSql += column.ColumnName + " = @" + column.ColumnName + ",";
                    strExtendSql += row[column.ColumnName].ToString();
                    continue;
                }

                value = row[column];
                if (value == DBNull.Value || value == null)
                {
                    strSql += column.ColumnName + "=null,";
                }
                else
                {
                    strSql += column.ColumnName + "=@" + column.ColumnName + ",";
                    if (row[column] is byte[])
                    {
                        objParams.Add(new OracleParameter("@" + column.ColumnName, value));
                    }
                    else
                    {
                        objParams.Add(new OracleParameter("@" + column.ColumnName, value));
                    }
                }
            }
            string strKeyFieldName1 = keyFieldNames[0];
            strSql = strSql.Substring(0, strSql.Length - 1)
                + " WHERE " + strKeyFieldName1 + "=@" + strKeyFieldName1;

            for (int i = 1; i < keyFieldNames.Length; i++)
            {
                strSql += " AND " + keyFieldNames[1] + "=@" + keyFieldNames[1];
                objParams.Add(new OracleParameter("@" + keyFieldNames[1],
                    row[keyFieldNames[1], DataRowVersion.Original]));
            }
            for (int i = 0; i < keyFieldNames.Length; i++)
            {
                bool foundKeyPara = false;
                for (int j = 0; j < objParams.Count; j++)
                {
                    if (((OracleParameter)objParams[j]).ParameterName.ToLower() == "@" + keyFieldNames[i].ToLower())
                    {
                        foundKeyPara = true;
                        break;
                    }
                }
                if (!foundKeyPara)
                {
                    objParams.Add(new OracleParameter("@" + keyFieldNames[i], row[keyFieldNames[i]]));
                }
            }
            if (strExtendSql != "")
            {
                strSql = strExtendSql + strSql;
            }
            ExecuteNonQuery(strSql,
                (OracleParameter[])objParams.ToArray(typeof(OracleParameter)));
        }

        public static void DeleteRecordByDataRow(string strTableName, DataRow
            row, string[] keyFieldNames)
        {
            if (keyFieldNames == null || keyFieldNames.Length < 1)
                throw new Exception("KeyFieldNames参数至少含有一个元素！");

            string strKeyFieldName1 = keyFieldNames[0];
            ArrayList objParams = new ArrayList();
            string strSql = "DELETE FROM " + strTableName + " WHERE " +
                strKeyFieldName1 + "=@" + strKeyFieldName1;
            objParams.Add(new OracleParameter("@" + strKeyFieldName1,
                row[strKeyFieldName1, DataRowVersion.Original]));

            for (int i = 1; i < keyFieldNames.Length; i++)
            {
                strSql += " AND " + keyFieldNames[i] + "=@" + keyFieldNames[i];
                objParams.Add(new OracleParameter("@" + keyFieldNames[i],
                    row[keyFieldNames[i], DataRowVersion.Original]));
            }

            ExecuteNonQuery(strSql,
                (OracleParameter[])objParams.ToArray(typeof(OracleParameter)));
        }

        public static void SaveRecordByDataRow(string strTableName, DataRow
            row,
            string strKeyFieldName, bool createKey)
        {
            SaveRecordByDataRow(strTableName, row, strKeyFieldName, true,
                createKey);
        }

        public static void SaveRecordByDataRow(string strTableName, DataRow
            row,
            string strKeyFieldName, bool bolAcceptChanges, bool createKey)
        {
            SaveRecordByDataRow(strTableName, row, new string[] { 
																	strKeyFieldName }, bolAcceptChanges, createKey);
        }

        /// <summary>
        /// 根据row的状态判定，是插入还是更新
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="row"></param>
        /// <param name="objAccess"></param>
        /// <param name="strKeyFieldName"></param>
        public static void SaveRecordByDataRow(string strTableName, DataRow
            row,
            string[] keyFieldNames, bool bolAcceptChanges, bool createKey)
        {
            if (row.RowState == DataRowState.Added)
            {
                InsertRecordByDataRow(strTableName, row, keyFieldNames[0],
                    createKey);
            }
            else if (row.RowState == DataRowState.Deleted)
            {
                DeleteRecordByDataRow(strTableName, row, keyFieldNames);
            }
            else if (row.RowState == DataRowState.Modified)
            {
                UpdateRecordByDataRow(strTableName, row, keyFieldNames);
            }
            else
            {
                return;
            }

            // 应用修改
            if (bolAcceptChanges)
            {
                row.AcceptChanges();
            }
        }

        /// <summary>
        /// 根据row的状态判定，是插入还是更新,并加上一个枚举的操作类型
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="row"></param>
        /// <param name="objAccess"></param>
        /// <param name="strKeyFieldName1"></param>
        /// <param name="strKeyFieldName2"></param>
        /// <param name="type"></param>
        public static void SaveRecordByDataRow(string strTableName, DataRow
            row,
            string[] keyFieldNames, UpdateTypes types, bool bolAcceptChanges,
            bool createKey)
        {
            if (row.RowState == DataRowState.Added && ((types |
                UpdateTypes.Insert) == types))
            {
                InsertRecordByDataRow(strTableName, row, keyFieldNames[0],
                    createKey);
            }
            else if (row.RowState == DataRowState.Deleted && ((types |
                UpdateTypes.Delete) == types))
            {
                DeleteRecordByDataRow(strTableName, row, keyFieldNames);
            }
            else if (row.RowState == DataRowState.Modified && (types |
                UpdateTypes.Update) == types)
            {
                UpdateRecordByDataRow(strTableName, row, keyFieldNames);
            }
            else
            {
                return;
            }

            if (bolAcceptChanges)
            {
                row.AcceptChanges();
            }
        }

        public static void SaveRecordByTable(DataTable dtTable, string
            strKeyFieldName, bool createKey)
        {
            SaveRecordByTable(dtTable, strKeyFieldName, true, createKey);
        }

        public static void SaveRecordByTable(DataTable dtTable, string
            strKeyFieldName,
            bool bolAcceptChanges, bool createKey)
        {
            SaveRecordByTable(dtTable, new string[] { strKeyFieldName },
                bolAcceptChanges, createKey);
        }

        public static void SaveRecordByTable(DataTable dtTable, string[]
            keyFieldNames,
            bool bolAcceptChanges, bool createKey)
        {
            foreach (DataRow dr in dtTable.Rows)
            {
                SaveRecordByDataRow(dtTable.TableName, dr, keyFieldNames, false,
                    createKey);
            }

            if (bolAcceptChanges)
            {
                dtTable.AcceptChanges();
            }
        }

        /// <summary>
        /// 通过DataTable更新数据表, 根据特定过滤条件
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="row"></param>
        /// <param name="objAccess"></param>
        /// <param name="strKeyFieldName1"></param>
        /// <param name="strKeyFieldName2"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static void SaveRecordByTable(DataTable dtTable, string[]
            keyFieldNames,
            UpdateTypes type, bool createKey)
        {
            foreach (DataRow dr in dtTable.Rows)
            {
                SaveRecordByDataRow(dtTable.TableName, dr, keyFieldNames, type,
                    false, createKey);
            }
        }

        /// <summary>
        /// 更新子表的外键
        /// </summary>
        /// <param name="dtDetail"></param>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        public static void UpdateDetailTableFK(DataTable dtDetail, string
            keyName, object keyValue)
        {
            foreach (DataRow row in dtDetail.Rows)
                if (row.RowState == DataRowState.Added)
                    row[keyName] = keyValue;
        }

        /// <summary>
        /// 恢复数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="databaseName"></param>
        /// <param name="destFolder"></param>
        /// <param name="canOverride"></param>
        public static void RestoreDatabase(string fileName, string
            databaseName, string destFolder,
            bool canOverride)
        {
            string sql = null, mdfLogical = null, ldfLogical = null;
            OracleCommand cmd = OracleHelper.GetCnn().CreateCommand();
            if (canOverride == false)
            {
                // 验证databaseName是否已存在
                cmd.CommandText = "SELECT COUNT(*) FROM sysdatabases WHERE 	name=@name";
                cmd.Parameters.AddWithValue("@name", databaseName);
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    throw new Exception("数据库已存在！\n" + databaseName);
                }
            }

            // 获取备份数据库文件的信息
            try
            {
                sql = string.Format("RESTORE FILELISTONLY FROM DISK='{0}'",
                    fileName);
                DataTable dtFileList = ExecuteDataTable(sql);
                mdfLogical = (string)dtFileList.Rows[0]["LogicalName"];
                ldfLogical = (string)dtFileList.Rows[0]["LogicalName"];
            }
            catch (Exception ep)
            {
                throw new Exception("解析备份文件时出错！\n" + ep.Message);
            }

            // 还原数据库
            string mdfFileName = System.IO.Path.Combine(destFolder, databaseName
                + ".mdf");
            string ldfFileName = System.IO.Path.Combine(destFolder, databaseName
                + "_log.ldf");
            sql = string.Format(@"RESTORE DATABASE {0} FROM DISK='{1}' "
                + @"WITH MOVE '{4}' TO '{2}',MOVE '{5}' TO '{3}'",
                new string[] { databaseName, fileName, mdfFileName, ldfFileName, 
								 mdfLogical, ldfLogical});
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        public static DataTable GetTableWithEmptyRow(string tableName)
        {
            string sql = "SELECT top 0 * from " + tableName;
            DataTable dt = OracleHelper.ExecuteDataTable(sql);
            dt.Rows.Add(dt.NewRow());
            return dt;
        }
        public static decimal GetMaxID(string tablename, string fieldname)
        {
            string strSql = string.Format("SELECT MAX({0}) FROM {1}", fieldname, tablename);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows[0][0] == DBNull.Value)
                return 1;
            else
                return ((decimal)dt.Rows[0][0]) + 1;
        }
        public static decimal GetMaxID(string tablename)
        {

            return GetMaxID(tablename, "id");
        }

        #region 执行存储过程的函数
        public static void ExecuteStoredProcedure(string StoredName, OracleParameter[] parameters)
        {
            System.Data.OracleClient.OracleCommand cmd = new OracleCommand();
            cmd.CommandText = StoredName;
            cmd.Connection = new OracleConnection(OracleHelper.CnnString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
            }
            try
            {
                cmd.CommandTimeout = 1800;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }
        public static void ExecuteStoredProcedure(string StoredName)
        {
            OracleHelper.ExecuteStoredProcedure(StoredName, null);
        }
        public static object ExecuteStoredProcedureForReturn(string StoredName, OracleParameter[] parameters)
        {
            System.Data.OracleClient.OracleCommand cmd = new OracleCommand();
            cmd.CommandText = StoredName;
            cmd.Connection = new OracleConnection(OracleHelper.CnnString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
                cmd.Parameters.Add(new OracleParameter("ReturnValue",
                    OracleType.Int32,
                    4, /* Size */
                    ParameterDirection.ReturnValue, //指定是返回值类型，则会把返回值传给本参数"ReturnValue",不论它叫什么
                    false, /* is nullable */
                    0,     /* byte precision */
                    0,     /* byte scale */
                    string.Empty,
                    DataRowVersion.Default,
                    null));
            }
            try
            {
                cmd.CommandTimeout = 1800;
                cmd.ExecuteNonQuery();
                return (object)cmd.Parameters["ReturnValue"].Value;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }
        /// <summary>
        /// 可用第一个执行存储过程的函数代替
        /// </summary>
        /// <param name="StoredName"></param>
        /// <param name="parameters"></param>
        /// <param name="outputParameters"></param>
        public static void ExecuteStoredProcedureForOutput(string StoredName, OracleParameter[] parameters, OracleParameter[] outputParameters)
        {
            System.Data.OracleClient.OracleCommand cmd = new OracleCommand();
            cmd.CommandText = StoredName;
            cmd.Connection = new OracleConnection(OracleHelper.CnnString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
                for (int j = 0; j < outputParameters.Length; j++)
                {
                    cmd.Parameters.Add(outputParameters[j]);
                }
            }
            try
            {
                cmd.CommandTimeout = 1800;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        /// <summary>
        /// Will run a stored procedure, can only be called by those classes deriving
        /// from this base. It returns a OracleDataReader containing the result of the stored
        /// procedure.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of parameters to be passed to the procedure</param>
        /// <returns>A newly instantiated OracleDataReader object</returns>
        public static OracleDataReader RunProcedure(string StoredName, IDataParameter[] parameters)
        {
            System.Data.OracleClient.OracleCommand cmd = new OracleCommand();
            cmd.CommandText = StoredName;
            cmd.Connection = new OracleConnection(OracleHelper.CnnString);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
            }
            try
            {
                cmd.Connection.Open();
                OracleDataReader returnReader = cmd.ExecuteReader();
                return returnReader;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();

            }

            //OracleDataReader returnReader;

            //Connection.Open();
            //OracleCommand command = BuildQueryCommand(storedProcName, parameters);
            //command.CommandType = CommandType.StoredProcedure;

            //returnReader = command.ExecuteReader();
            ////Connection.Close();
            //return returnReader;
        }

        /// <summary>
        /// Creates a DataSet by running the stored procedure and placing the results
        /// of the query/proc into the given tablename.
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet RunProcedure(string StoredName, IDataParameter[] parameters, string tableName)
        {
            System.Data.OracleClient.OracleCommand cmd = new OracleCommand();
            cmd.CommandText = StoredName;
            cmd.Connection = new OracleConnection(OracleHelper.CnnString);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
            }
            try
            {
                cmd.Connection.Open();
                DataSet dataSet = new DataSet();
                OracleDataAdapter sqlDA = new OracleDataAdapter();
                sqlDA.SelectCommand = cmd;
                sqlDA.Fill(dataSet, tableName);
                return dataSet;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        /// <summary>
        /// Runs a stored procedure, can only be called by those classes deriving
        /// from this base. It returns an integer indicating the return value of the
        /// stored procedure, and also returns the value of the RowsAffected aspect
        /// of the stored procedure that is returned by the ExecuteNonQuery method.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure</param>
        /// <param name="parameters">Array of IDataParameter objects</param>
        /// <param name="rowsAffected">Number of rows affected by the stored procedure.</param>
        /// <returns>An integer indicating return value of the stored procedure</returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            int result;

            System.Data.OracleClient.OracleCommand cmd = new OracleCommand();
            cmd.CommandText = storedProcName;
            cmd.Connection = new OracleConnection(OracleHelper.CnnString);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
                cmd.Parameters.Add(new OracleParameter("ReturnValue",
                    OracleType.Int32,
                    4, /* Size */
                    ParameterDirection.ReturnValue, //指定是返回值类型，则会把返回值传给本参数"ReturnValue",不论它叫什么
                    false, /* is nullable */
                    0,     /* byte precision */
                    0,     /* byte scale */
                    string.Empty,
                    DataRowVersion.Default,
                    null));
            }
            try
            {
                cmd.Connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                result = (int)cmd.Parameters["ReturnValue"].Value;
                return result;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }

        ///// <summary>
        ///// Builds a OracleCommand designed to return a OracleDataReader, and not
        ///// an actual integer value.
        ///// </summary>
        ///// <param name="storedProcName">Name of the stored procedure</param>
        ///// <param name="parameters">Array of IDataParameter objects</param>
        ///// <returns></returns>
        //public static OracleCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
        //{
        //    OracleCommand command = new OracleCommand(storedProcName, Connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    foreach (OracleParameter parameter in parameters)
        //    {
        //        command.Parameters.Add(parameter);
        //    }

        //    return command;

        //}

        #endregion

        public static DataTable AddTable(DataTable dt1, DataTable dt2)
        {
            int c = dt1.Columns.Count;
            int r = dt2.Rows.Count;
            try
            {
                for (int i = 0; i < r; i++)
                {
                    System.Data.DataRow row = dt1.NewRow();
                    for (int j = 0; j < c; j++)
                    {
                        //if(dt2.Rows[i][j] != null)
                        row[j] = dt2.Rows[i][j];
                    }
                    dt1.Rows.Add(row);
                }
                return dt1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static DataTable AddTable(ArrayList alTable)
        {
            int n = alTable.Count;
            if (n == 0)
                return null;

            DataTable dt1 = (DataTable)alTable[0];
            if (n == 1)
                return dt1;

            //			int c = dt1.Columns.Count;
            //			int r = dt2.Rows.Count;
            try
            {
                for (int k = 1; k < n; k++)
                {
                    dt1 = OracleHelper.AddTable(dt1, (DataTable)alTable[k]);
                }
                return dt1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        /// 判断某表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool IsHaveTable(string tableName)
        {
            int c = (int)OracleHelper.ExecuteScalar("SELECT COUNT(1) FROM sysobjects WHERE type='U' AND name='" + tableName + "'");
            return c == 1;
        }

        public static void UpdateDataBase(string dtName, DataTable dt)
        {
            string mySelectQuery = "SELECT * FROM " + dtName.Trim() + " WHERE 1=2";
            System.Data.OracleClient.OracleConnection myConn = OracleHelper.GetCnn();
            System.Data.OracleClient.OracleDataAdapter myDataAdapter = new OracleDataAdapter();
            myDataAdapter.SelectCommand = new  OracleCommand(mySelectQuery, myConn);
            System.Data.OracleClient.OracleCommandBuilder custCB = new OracleCommandBuilder(myDataAdapter);
            myConn.Open();

            DataTable dtModel = new DataTable();
            myDataAdapter.Fill(dtModel);

            //code to modify data in DataTable here
            dtModel = dt;
            myDataAdapter.Update(dt);
            myConn.Close();
        }


        //************add*********************************//
        public static bool Exists(string cmdText, OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            using (OracleConnection conn = new OracleConnection(CnnString))
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
        /// Prepare a command for execution
        /// </summary>
        /// <param name="conn">SqlConnection Object </param>
        /// <param name="tran">SqlTransaction Object </param>
        /// <param name="cmd">SqlCommand Object </param>
        /// <param name="cmdType">Cmd Type e.g . stored procedure or text</param>
        /// <param name="cmdText">CommandText e.g. Select * From Products</param>
        /// <param name="cmdParms">SqlParameter to use in the command</param>
        private static void PrepareCommand(OracleConnection conn, OracleTransaction tran, OracleCommand cmd, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
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
                foreach (OracleParameter parms in cmdParms)
                {
                    cmd.Parameters.Add(parms);
                }
            }
        }
        public static int ExecuteNonQuery(string connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            using (OracleConnection conn = new OracleConnection(CnnString))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalar(string connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            using (OracleConnection conn = new OracleConnection(connection))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                return val;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(CnnString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
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
        public static DataSet Query(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection conn = new OracleConnection(CnnString))
            {
                OracleCommand cmd = new OracleCommand();
                //PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                PrepareCommand(conn, null, cmd, CommandType.Text, SQLString, cmdParms);
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
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
        /// Execute a SqlCommand that returns a resultset aginst the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <param name="connection">a valid connection string of a SqlConnection</param>
        /// <param name="cmdType">the CommandType(e.g.:stored Procedure)</param>
        /// <param name="cmdText">the stored procedure or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static OracleDataReader ExecuteReader(string connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleConnection conn = new OracleConnection(CnnString);
            OracleCommand cmd = new OracleCommand();

            try
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, commandParameters);
                OracleDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return sdr;
            }
            catch(Exception ex)
            {
                conn.Close();
                throw;
            }
        }


    }


    /// <summary>
    /// 写入数据库的操作状态枚举
    /// </summary>
    public enum UpdateTypes
    {
        Insert = 1,
        Update = 2,
        Delete = 4

    }
}

