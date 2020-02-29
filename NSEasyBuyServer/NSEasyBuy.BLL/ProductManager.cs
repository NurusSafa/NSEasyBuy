using Newtonsoft.Json;
using NSEasyBuy.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace NSEasyBuy.BLL
{

    public class ProductManager
    {
        public delegate dynamic dlRequestHandler(object param);
        public dlRequestHandler _PoductDataHandler;
        public enum enumMethod : int
        {
            productsbyKey = 0,
            productdetails = 1
        }
        public enumMethod _MethodName
        {
            set
            {
                switch (value)
                {
                    case (enumMethod)0:
                        _PoductDataHandler = GetProductsByKeywords;
                        break;
                    case (enumMethod)1:
                        _PoductDataHandler = GetProductById;
                        break;
                }
            }

        }

        private readonly DataAccessManager _DataAccessManager;
        public ProductManager(DataAccessManager dataAccessManager)
        {
            _DataAccessManager = dataAccessManager;
        }
        private object GetProductsByKeywords(object strKeyword)
        {
            string strProcedureName = "sprocGetProducts";
            List<Tuple<string, object, SqlDbType, string>> lstParameters = new List<Tuple<string, object, SqlDbType, string>>();
            lstParameters.Add(new Tuple<string, object, SqlDbType, string>("keywords", strKeyword.ToString(), SqlDbType.VarChar, null));

            List<List<CommonDataLayout>> lstProducts = _DataAccessManager.GetObjects(lstParameters, strProcedureName);

            List<JSONResult> lstProductResult = new List<JSONResult>();

     
           // jSONResult.PropertyName = "products";

            for (int i = 0; i < lstProducts.Count; i++)
            {
                JSONResult jSONResult = new JSONResult();
                jSONResult.JsnObjects  = new List<JSONObject>();
                for (int colIndex = 0; colIndex < lstProducts[i].Count; colIndex++)
                {
                    JSONObject jSONObject = new JSONObject();

                    jSONObject.Key = lstProducts[i][colIndex].FieldName;
                    jSONObject.Value = lstProducts[i][colIndex].FieldValue;

                    jSONResult.JsnObjects.Add(jSONObject);
                }
                lstProductResult.Add(jSONResult);
            }
            object objJSONResult = BuildJSON("products", lstProductResult);
            // valDAO.GetObjects()
            return objJSONResult;
        }

        public string BuildJSON(string strKey, List<JSONResult> lststringObjects)
        {
            StringBuilder sbJSONResult = new StringBuilder();

            sbJSONResult.Append("\"");
            sbJSONResult.Append(strKey);
            sbJSONResult.Append("\"");
            sbJSONResult.Append(":");

            sbJSONResult.Append("[");
            for (int rowIndex = 0; rowIndex < lststringObjects.Count; rowIndex++)
            {
                JSONResult jSONResult = lststringObjects[rowIndex];
             
                if (rowIndex > 0)
                {
                    sbJSONResult.Append(",");
                }
            
                sbJSONResult.Append("{");

                for (int i = 0; i < jSONResult.JsnObjects.Count; i++)
                {
                    JSONObject jSONObject1 = jSONResult.JsnObjects[i];
                    if (i > 0)
                    {
                        sbJSONResult.Append(",");
                    }


                    JSONObject jSONObject = jSONObject1;
                    sbJSONResult.Append("\"");
                    sbJSONResult.Append(jSONObject.Key);
                    sbJSONResult.Append("\"");
                    sbJSONResult.Append(":");

                    Type typeOfValue = jSONObject.Value.GetType();

                    if (typeOfValue == typeof(List<JSONResult>))
                    {
                        List<JSONResult> lstValueObject = jSONObject.Value as List<JSONResult>;
                        sbJSONResult.Append(BuildJSON(strKey,lstValueObject));
                    }
                    if (typeOfValue == typeof(JSONObject))
                    {
                        JSONObject ValueObjectJson = jSONObject.Value as JSONObject;
                        sbJSONResult.Append("{");
                        sbJSONResult.Append("\"");
                        sbJSONResult.Append(ValueObjectJson.Key);
                        sbJSONResult.Append("\"");
                        sbJSONResult.Append(":");
                        sbJSONResult.Append(ValueObjectJson.Value);
                        sbJSONResult.Append("\"");
                        sbJSONResult.Append("}");
                    }
                    else
                    {
                        sbJSONResult.Append("\"");
                        sbJSONResult.Append(jSONObject.Value);
                        sbJSONResult.Append("\"");
                    }                   
                }
                sbJSONResult.Append("}");
            }
        
       
            sbJSONResult.Append("]");

            return sbJSONResult.ToString();
        }

        public static object GetJSONFromString(string apiResponse)
        {
            object apiReturn;
            StringReader sr = new StringReader(apiResponse);
            JsonTextReader jReader = new JsonTextReader(sr);
            JsonSerializer jsonSerializer = new JsonSerializer();
            apiReturn = jsonSerializer.Deserialize(jReader);
            return apiReturn;
        }

        public static string GetJSONStringFromObject(object objParameter)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter writer = new JsonTextWriter(sw);
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(writer, objParameter);

            return sb.ToString();
        }

        private object GetProductById(object Id)
        {
            string strProcedureName = "sprocGetProducts";
            List<Tuple<string, object, SqlDbType, string>> lstParameters = new List<Tuple<string, object, SqlDbType, string>>();
            lstParameters.Add(new Tuple<string, object, SqlDbType, string>("keywords", Id, SqlDbType.VarChar, null));

            List<List<CommonDataLayout>> lstProducts = _DataAccessManager.GetObjects(lstParameters, strProcedureName);

            for (int i = 0; i < lstProducts.Count; i++)
            {
                for (int colIndex = 0; colIndex < lstProducts[i].Count; colIndex++)
                {
                    var firstCol = lstProducts[i][colIndex];
                }

            }
            // valDAO.GetObjects()
            return lstProducts;
        }
    }
    public class JSONObject
    {
        public string Key;
        public object Value;
    }
    public class JSONResult
    {
        public string PropertyName { get; set; }
        public List<JSONObject> JsnObjects { get; set; }       
    }
}
