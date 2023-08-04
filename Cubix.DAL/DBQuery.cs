using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Cubix.Models;
using Cubix.Utility;
using System.IO;

namespace Cubix.DAL
{
    public class DBQuery
    {


        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(AppSetting.ConnectionString);

            }
        }

        //Remainder of file is unchanged

        #region Queries
        public static async Task<T> ExeScalarQuery<T>(String QueryText, DynamicParameters paras)
        {
            try
            {
                T Result;
                using (IDbConnection conn = Connection)
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    Result = await conn.QueryFirstOrDefaultAsync<T>(QueryText, paras);

                }
                return Result;

            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("proceduresname.txt", QueryText + Environment.NewLine + ex.Message);
                throw ex;
            }



        }
        public static async Task<int> ExeQuery(String QueryText, DynamicParameters paras)
        {

            int Result;
            try
            {
                using (IDbConnection conn = Connection)
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    Result = await conn.ExecuteAsync(QueryText, paras);
                }
                return Result;
            }
            catch (Exception ex)
            {


                Log.LogError(ex);
                System.IO.File.AppendAllText("proceduresname.txt", QueryText + Environment.NewLine + ex.Message);
                return 0;
            }
        }
        public static async Task<List<T>> ExeQueryList<T>(String QueryText, DynamicParameters paras)
        {

            List<T> Result;
            try
            {

                using (IDbConnection conn = Connection)
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    var x = await conn.QueryAsync<T>(QueryText, paras);
                    Result = x.ToList();
                    return Result;
                }

            }
            catch (Exception ex)
            {

                System.IO.File.AppendAllText("proceduresname.txt", QueryText + Environment.NewLine + ex.Message);
                return null;
            }


        }
        public static async Task<T> ExeScalarQuery<T>(String QueryText)
        {
            try
            {


                T Result;
                using (IDbConnection conn = Connection)
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    Result = await conn.QueryFirstOrDefaultAsync<T>(QueryText);
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                System.IO.File.AppendAllText("proceduresname.txt", QueryText + Environment.NewLine + ex.Message);
                return default(T); ;
            }

        }
        public static async Task<List<T>> ExeQueryList<T>(String QueryText)
        {
            try
            {

                List<T> Result;
                using (IDbConnection conn = Connection)
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    //Result = conn.Query<T>(QueryText).ToList();
                    var x = await conn.QueryAsync<T>(QueryText);
                    Result = x.ToList();
                }
                return Result;
            }
            catch (Exception ex)
            {

                Log.LogError(ex);
                System.IO.File.AppendAllText("proceduresname.txt", QueryText + Environment.NewLine + ex.Message);
                return default(List<T>);

            }
        }
        public static async Task<T> ExeSPScaler<T>(String QueryText, DynamicParameters paras)
        {

            try
            {
                T Result;
                using (IDbConnection conn = Connection)
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    Result = await conn.QueryFirstAsync<T>(QueryText, paras, commandType: System.Data.CommandType.StoredProcedure);
                }
                return Result;
            }
            catch (Exception ex)
            {

                Log.LogError(ex);
                System.IO.File.AppendAllText("proceduresname.txt", QueryText + Environment.NewLine + ex.Message);
                return default(T);
            }

        }
        public static async Task<List<T>> ExeSPList<T>(String QueryText, DynamicParameters paras)
        {
            try
            {


                List<T> Result;
                using (IDbConnection conn = Connection)
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    var x = await conn.QueryAsync<T>(QueryText, paras, commandType: System.Data.CommandType.StoredProcedure);
                    Result = x.ToList();
                }
                return Result;
            }
            catch (Exception ex)
            {

                Log.LogError(ex);
                System.IO.File.AppendAllText("proceduresname.txt", QueryText + Environment.NewLine + ex.Message);
                return default(List<T>);
            }
        }
        public static async Task<int> ExeSP(String QueryText, DynamicParameters paras)
        {
            try
            {
                int Result;
                using (IDbConnection conn = Connection)
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    Result = await conn.ExecuteAsync(QueryText, paras, commandType: System.Data.CommandType.StoredProcedure);
                }
                return Result;
            }
            catch (Exception ex)
            {

                Log.LogError(ex);
                System.IO.File.AppendAllText("proceduresname.txt", QueryText + Environment.NewLine + ex.Message);
                return 0;
            }
        }
        public static List<dynamic> ExeSPSclarMultiple(String QueryText, DynamicParameters paras)
        {

            dynamic Result;
            using (IDbConnection conn = Connection)
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                using (var multi = conn.QueryMultiple(QueryText, paras, commandType: System.Data.CommandType.StoredProcedure))
                {
                    List<dynamic> oLst = new List<dynamic>();

                    while (!multi.IsConsumed)
                    {
                        oLst.Add(multi.Read().ToList());
                    }
                    Result = oLst;
                }
                return Result;
            }

        }

        #endregion
    }
}
