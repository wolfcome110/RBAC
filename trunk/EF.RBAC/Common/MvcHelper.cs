using EF.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace EF.RBAC
{
    public class MvcHelper
    {
        private static RBACEntities db = new RBACEntities();

        #region 批量添加，修改，删除

        /// <summary>
        /// 批量添加，修改，删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertedRows"> json数组 </param>
        /// <param name="updatedRows"> json数组 </param>
        /// <param name="deletedRows"> json数组 </param>
        /// <returns></returns>
        public static object BacthSave<T>(string insertedRows, string updatedRows, string deletedRows) where T : class
        {
            try
            {
                //string insertedRows = Request["insertedRows"];
                //string updatedRows = Request["updatedRows"];
                //string deletedRows = Request["deletedRows"];

                //添加
                if (!string.IsNullOrEmpty(insertedRows))
                {
                    List<T> list = JsonHelper.DeserializeData<List<T>>(insertedRows);
                    foreach (var model in list)
                    {
                        //db.Sys_Navigations.Add(btn);
                        db.Entry(model).State = EntityState.Added;
                    }
                    //db.Sys_Buttons.Add(unicorn);//添加到上下文中
                    //插入到数据库中
                }

                //修改
                if (!string.IsNullOrEmpty(updatedRows))
                {
                    Type type = typeof(T);

                    List<T> list = JsonHelper.DeserializeData<List<T>>(updatedRows);
                    PropertyInfo propertyInfo = type.GetProperty("Id");

                    foreach (var model in list)
                    {
                        string n = propertyInfo.Name;
                        n = propertyInfo.PropertyType.Name;
                        n = propertyInfo.PropertyType.FullName;
                        Nullable<int> id = propertyInfo.GetValue(model) as Nullable<int>;
                        //string id = model.GetType().InvokeMember("Id", System.Reflection.BindingFlags.GetProperty, null, model, null) as string;

                        var entry = db.Entry(model);
                        if (entry.State == EntityState.Detached)
                        {
                            var set = db.Set<T>();
                            T attached = set.Local.SingleOrDefault(p => propertyInfo.GetValue(p) as int? == id);

                            //如果已经被上下文追踪
                            if (attached != null)
                            {
                                var attachedEntry = db.Entry(attached);
                                attachedEntry.CurrentValues.SetValues(model);
                            }
                            else //如果不在当前上下文追踪
                            {
                                entry.State = EntityState.Modified;
                            }
                        }
                        //db.Entry(model).State = EntityState.Modified;
                    }

                }

                //删除
                if (!string.IsNullOrEmpty(deletedRows))
                {
                    Type type = typeof(T);
                    List<T> list = JsonHelper.DeserializeData<List<T>>(deletedRows);

                    PropertyInfo propertyInfo = type.GetProperty("Id");

                    foreach (var model in list)
                    {
                        Nullable<int> id = propertyInfo.GetValue(model) as Nullable<int>;
                        var entry = db.Entry(model);
                        if (entry.State == EntityState.Detached)
                        {
                            var set = db.Set<T>();
                            T attached = set.Local.SingleOrDefault(p => propertyInfo.GetValue(p) as int? == id);

                            //如果已经被上下文追踪
                            if (attached != null)
                            {
                                var attachedEntry = db.Entry(attached);
                                //attachedEntry.CurrentValues.SetValues(model);
                                attachedEntry.State = EntityState.Deleted;
                            }
                            else //如果不在当前上下文追踪
                            {
                                entry.State = EntityState.Deleted;
                            }
                        }

                        //---------------------------------------------
                        //db.Entry(model).State = EntityState.Deleted;
                        //db.Sys_Buttons.Remove(btn);//也可以使用这个方法
                    }
                }
                db.SaveChanges();
                return new { success = true, Message = "保存成功！" };
            }
            catch (Exception ex)
            {
                return new { success = false, Message = "保存失败！" };
            }
        }

        /// <summary>
        /// 移动树类型的节点,问题，待处理。。。
        /// </summary>
        /// <param name="SrcId">要移动的节点id</param>
        /// <param name="DestId">目标节点id</param>
        /// <returns></returns>
        public static object MoveNode<T>(int SrcId, int DestId) where T : class,new()
        {
            try
            {
                T t = new T();

                Type type = typeof(T);
                PropertyInfo propertyId = type.GetProperty("Id");
                PropertyInfo propertyParentId = type.GetProperty("ParentId");
                propertyId.SetValue(t, SrcId);
                propertyParentId.SetValue(t, DestId);
                //db.Entry<T>(t);

                var set = db.Set<T>();
                T attached = set.Local.SingleOrDefault(p => propertyId.GetValue(p) as int? == SrcId);
                //attached = set.SingleOrDefault(p => propertyId.GetValue(p) as int? == SrcId);
                //如果已经被上下文追踪
                if (attached != null)
                {
                    var attachedEntry = db.Entry(attached);
                    //attachedEntry.CurrentValues.SetValues(model);
                }
                else //如果不在当前上下文追踪
                {
                    //entry.State = EntityState.Modified;
                }




                //=====================

                //var model = db.Sys_Departments.Where(o => o.Id == SrcId).FirstOrDefault();
                //model.ParentId = DestId;
                db.Entry(t).State = EntityState.Modified;
                db.Entry(t).Property("ParentId").IsModified = true;
                //db.Entry(t).Property("").IsModified = true;

                db.SaveChanges();
                return new { success = true, Message = "保存成功！" };
            }
            catch (Exception ex)
            {
                return new { success = false, Message = "保存失败！" };
            }
        }
        #endregion

        #region 返回树型目录，供easyui的treegrid使用
        /// <summary>
        /// 返回树型目录，供easyui的treegrid使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id">父分类的编号</param>
        /// <param name="models">要进行分类的数据集合</param>
        /// <returns></returns>
        public static IEnumerable<T> TreeGrid<T>(int? Id, List<T> Models) where T : class,new()
        {

            var treegrid = Models.Where<T>(o => o.GetType().GetProperty("ParentId").GetValue(o) as int? == Id);

            foreach (var model in treegrid)
            {
                var _id = model.GetType().GetProperty("Id").GetValue(model);
                var children = Models.Where<T>(o => o.GetType().GetProperty("ParentId").GetValue(o) as int? == _id as int?);
                model.GetType().GetProperty("children").SetValue(model, children);
                
                TreeGrid<T>(_id as int?, Models);
            }
            return treegrid as IEnumerable<T>;
        }
        #endregion


        #region 数据库备份/还原

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="DataBase">要备份的数据库名称</param>
        /// <param name="BackUpPath">要备份的绝对路径+文件名,例如：d:\bak\20160629.bak </param>
        public static void BackupDataBase(string DataBase, string BackUpPath)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.AppendFormat("alter database {0} set online  with rollback immediate" + Environment.NewLine, DataBase, DataBase);
            //db.Database.ExecuteSqlCommand(strSql.ToString());

            //strSql.Clear();

            strSql.AppendFormat("backup database {0}" + Environment.NewLine, DataBase);
            strSql.AppendFormat(@"to disk='{0}'" + Environment.NewLine, BackUpPath);
            strSql.AppendFormat("with format");

            //db.Database.ExecuteSqlCommand(strSql.ToString());
            //DBUtility.SqlHelper.ExecuteNonQuery(strSql.ToString());
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BakupOrRestore"].ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strSql.ToString();
                cmd.CommandType = CommandType.Text;
                int val = cmd.ExecuteNonQuery();
            }

        }

        public static void RestoreDateBase(string DataBase, string BackUpPath)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("alter database {0} set offline with rollback immediate" + Environment.NewLine, DataBase);
            strSql.AppendFormat("restore database {0}" + Environment.NewLine, DataBase);
            strSql.AppendFormat(@"from disk='{0}'" + Environment.NewLine, BackUpPath);
            strSql.AppendFormat("with replace");

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BakupOrRestore"].ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strSql.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteDateBase(string File)
        {
            FileHelper.DeleteFile(File);
        }
        #endregion

    }
}