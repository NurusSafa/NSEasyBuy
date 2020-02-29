using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BN.Repository.Data
{
    public class CommonDataLayout
    {
        public string FieldType;
        public string FieldName;
        public string FieldValue;
    }
    public class DAO
    {
        public string m_ConnnectionString = "";

        public DAO(string strConnectionString)
        {
            m_ConnnectionString = strConnectionString;
        }
        /// <summary>
        /// Get multiple Objects by dynamic parameters with Strored Procedure
        /// </summary>
        /// <param name="lstParameters">strParameterName,objParameterValue,tParameterType</param>
        /// <param name="strProcedureName"></param>
        /// <returns></returns>
        public List<List<CommonDataLayout>> GetObjects(List<Tuple<string, object, SqlDbType, string>> lstParameters, string strProcedureName)
        {
            List<List<CommonDataLayout>> lstData = new List<List<CommonDataLayout>>();
            int? intResult = null;
            string strCmdText = strProcedureName;
            SqlConnection conn = new SqlConnection(m_ConnnectionString);
            SqlCommand cmd = new SqlCommand(strCmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if(lstParameters!=null)
            {
                for (int intParamIndex = 0; intParamIndex < lstParameters.Count; intParamIndex++)
                {
                    string strParameterName = lstParameters[intParamIndex].Item1;
                    object objParameterValue = lstParameters[intParamIndex].Item2;
                    SqlDbType tParameterType = lstParameters[intParamIndex].Item3;
                    string strTypeName = lstParameters[intParamIndex].Item4;
                    if (!string.IsNullOrEmpty(strTypeName))
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = strParameterName;
                        sqlParameter.Value = objParameterValue;
                        sqlParameter.SqlDbType = tParameterType;
                        sqlParameter.TypeName = strTypeName;

                        cmd.Parameters.Add(sqlParameter);
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(strParameterName, tParameterType, int.MaxValue,
                                              ParameterDirection.Input, 0, 0, null,
                                              DataRowVersion.Current, false, objParameterValue, null, null, null));
                    }
                }

            }


            //cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 4,
            //                        ParameterDirection.Output, 0, 0, null,
            //                        DataRowVersion.Current, false, null, null, null, null));

            SqlDataReader rdr = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    List<CommonDataLayout> lstCol = new List<CommonDataLayout>();
                    for (int FiedIndex = 0; FiedIndex < rdr.FieldCount; FiedIndex++)
                    {
                        CommonDataLayout valCommonDataLayout = new CommonDataLayout();
                        valCommonDataLayout.FieldName = rdr.GetName(FiedIndex);
                        valCommonDataLayout.FieldType = rdr[FiedIndex].GetType().ToString();
                        valCommonDataLayout.FieldValue = rdr[FiedIndex].ToString();

                        lstCol.Add(valCommonDataLayout);
                    }
                    lstData.Add(lstCol);
                }

                rdr.Close();
                rdr = null;

               // intResult = cmd.Parameters[0] != null ? (int?)cmd.Parameters[0].Value : null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                cmd.Dispose();
                conn.Dispose();
            }

            return lstData;
        }
        /// <summary>
        ///  Get multiple Result set of Objects by dynamic parameters with Strored Procedure
        /// </summary>
        /// <param name="lstParameters">strParameterName,objParameterValue,tParameterType,MultipleResults</param>
        /// <param name="strProcedureName"></param>
        /// <returns></returns>
        public List<List<List<CommonDataLayout>>> GetObjects(List<Tuple<string, object, SqlDbType,string>> lstParameters, string strProcedureName,bool MultipleResults=false)
        {
            List<List<List<CommonDataLayout>>> lstDataSets = new List<List<List<CommonDataLayout>>>();
        
            int? intResult = null;
            string strCmdText = strProcedureName;
            SqlConnection conn = new SqlConnection(m_ConnnectionString);
            SqlCommand cmd = new SqlCommand(strCmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (lstParameters != null)
            {
                for (int intParamIndex = 0; intParamIndex < lstParameters.Count; intParamIndex++)
                {
                    string strParameterName = lstParameters[intParamIndex].Item1;
                    object objParameterValue = lstParameters[intParamIndex].Item2;
                    SqlDbType tParameterType = lstParameters[intParamIndex].Item3;
                    string strTypeName = lstParameters[intParamIndex].Item4;
                    if(!string.IsNullOrEmpty(strTypeName))
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = strParameterName;
                        sqlParameter.Value = objParameterValue;
                        sqlParameter.SqlDbType = tParameterType;
                        sqlParameter.TypeName = strTypeName;

                        cmd.Parameters.Add(sqlParameter);
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(strParameterName, tParameterType, int.MaxValue,
                                              ParameterDirection.Input, 0, 0, null,
                                              DataRowVersion.Current, false, objParameterValue, null, null, null));
                    }
                }

            }


            //cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 4,
            //                        ParameterDirection.Output, 0, 0, null,
            //                        DataRowVersion.Current, false, null, null, null, null));

            SqlDataReader rdr = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                rdr = cmd.ExecuteReader();

                do
                {
                    List<List<CommonDataLayout>> lstData = new List<List<CommonDataLayout>>();
                    while (rdr.Read())
                    {
                        List<CommonDataLayout> lstCol = new List<CommonDataLayout>();
                        for (int FiedIndex = 0; FiedIndex < rdr.FieldCount; FiedIndex++)
                        {
                            CommonDataLayout valCommonDataLayout = new CommonDataLayout();
                            valCommonDataLayout.FieldName = rdr.GetName(FiedIndex);
                            valCommonDataLayout.FieldType = rdr[FiedIndex].GetType().ToString();
                            valCommonDataLayout.FieldValue = rdr[FiedIndex].ToString();

                            lstCol.Add(valCommonDataLayout);
                        }
                        lstData.Add(lstCol);
                    }
                    lstDataSets.Add(lstData);
                }
                while (rdr.NextResult());            

                rdr.Close();
                rdr = null;

                // intResult = cmd.Parameters[0] != null ? (int?)cmd.Parameters[0].Value : null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                cmd.Dispose();
                conn.Dispose();
            }

            return lstDataSets;
        }

        /// <summary>
        /// Get Single Object by dynamic parameters with Strored Procedure
        /// </summary>
        /// <param name="lstParameters">strParameterName,objParameterValue,tParameterType</param>
        /// <param name="strProcedureName"></param>
        /// <returns></returns>
        public List<CommonDataLayout> GetObject(List<Tuple<string, object, SqlDbType, string>> lstParameters, string strProcedureName)
        {
            List<CommonDataLayout> lstData = new List<CommonDataLayout>();
            int? intResult = null;
            string strCmdText = strProcedureName;
            SqlConnection conn = new SqlConnection(m_ConnnectionString);
            SqlCommand cmd = new SqlCommand(strCmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (lstParameters != null)
            {
                for (int intParamIndex = 0; intParamIndex < lstParameters.Count; intParamIndex++)
                {
                    string strParameterName = lstParameters[intParamIndex].Item1;
                    object objParameterValue = lstParameters[intParamIndex].Item2;
                    SqlDbType tParameterType = lstParameters[intParamIndex].Item3;
                    string strTypeName = lstParameters[intParamIndex].Item4;
                    if (!string.IsNullOrEmpty(strTypeName))
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = strParameterName;
                        sqlParameter.Value = objParameterValue;
                        sqlParameter.SqlDbType = tParameterType;
                        sqlParameter.TypeName = strTypeName;

                        cmd.Parameters.Add(sqlParameter);
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(strParameterName, tParameterType, int.MaxValue,
                                              ParameterDirection.Input, 0, 0, null,
                                              DataRowVersion.Current, false, objParameterValue, null, null, null));
                    }
                }
            }
            SqlDataReader rdr = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    for (int FiedIndex = 0; FiedIndex < rdr.FieldCount; FiedIndex++)
                    {
                        CommonDataLayout valCommonDataLayout = new CommonDataLayout();
                        valCommonDataLayout.FieldName = rdr.GetName(FiedIndex);
                        valCommonDataLayout.FieldType = rdr[FiedIndex].GetType().ToString();
                        valCommonDataLayout.FieldValue = rdr[FiedIndex].ToString();

                        lstData.Add(valCommonDataLayout);
                    }                    
                }

                rdr.Close();
                rdr = null;

                // intResult = cmd.Parameters[0] != null ? (int?)cmd.Parameters[0].Value : null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                cmd.Dispose();
                conn.Dispose();
            }

            return lstData;
        }
    }
}
