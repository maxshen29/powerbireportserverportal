using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PowerBI.SSO
{
   public class Common
    {
        public string GetConfig(string type)
        {

            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = "c:\\PBISSO\\pbiserver\\PBISSO.config"; ;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);          
            return config.AppSettings.Settings[type].Value;
          
        }
        public string GetCookie(string strName)
        {
            WriteLogs("开始获取Cookie值!");
            try
            {

                if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                {
                    WriteLogs("获取Cookie值：" + HttpContext.Current.Request.Cookies[strName].Value.ToString());
                    return HttpContext.Current.Request.Cookies[strName].Value.ToString();
                }
                else
                {

                    return "";
                }
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message);
                return "";
            }

        }
        public void SetCookie(string UserName)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["PBIUserName"];
                if (cookie == null)
                {
                    cookie = new HttpCookie("PBIUserName");

                }
                cookie.Value = UserName;
                WriteLogs("设置Cookie值：" + UserName);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message);
               
            }

        }
        public string GetSQLConnect()
        {
        //    string json = "";
        //    string dbconnect = "";
        //    string configpath = "c:\\PBISSO\\sqlconnect.txt";
        //    using (FileStream fs = new FileStream(configpath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
        //    {
        //        using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8")))
        //        {
        //            json = sr.ReadToEnd().ToString();
        //            dbconnect = json;

        //        }
        //    }
           return  GetConfig("DbConnectionString");

        }

        public string GetLoginUserName(string sign)
        {

            string UserName = "";

            string dbconnect = GetSQLConnect();
            // common.WriteLogs("VerifyPassword:" + dbconnect); 
            using (SqlConnection conn = new SqlConnection(dbconnect))
            {

               


                     string sqlstr = $"select top 1 username from Sys_User where substring(sys.fn_sqlvarbasetostr(HashBytes('MD5', CONVERT(varchar(10), getdate(), 23) + rtrim(ltrim(CONVERT(varchar, username))))), 3, 32) = '{sign}'";
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if( reader.Read())
                        {
                            UserName = reader.GetString(0);
                        }
                       
                    }
                }
                catch (Exception ex)
                {

                    WriteLogs(ex.Message + ex.StackTrace);
                }



                sqlstr = $"select top 1 [USER_LOGIN_NAME]   from PBI_SSO_USER where substring(sys.fn_sqlvarbasetostr(HashBytes('MD5',CONVERT(varchar, getdate(),23)+rtrim(ltrim(CONVERT(varchar, USER_LOGIN_NAME))))),3,32)='{sign}'";
                cmd = new SqlCommand(sqlstr, conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserName = reader.GetString(0);
                        }

                    }
                }
                catch (Exception ex)
                {

                    WriteLogs(ex.Message + ex.StackTrace);
                }



            }

            return UserName;
        
        }
 

        public string GetWebUrl()
        {
            //string json = "";
            //string WebURL = "";
            //string configpath = "c:\\PBISSO\\weburl.txt";
            //using (FileStream fs = new FileStream(configpath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            //{
            //    using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8")))
            //    {
            //        json = sr.ReadToEnd().ToString();

            //        WebURL = json;

            //    }
            //}
            return GetConfig("webUrl");
        }
        public  void WriteLogs(string content)
        { 
            try
            {
                string path = "c:\\temp\\";
                string LogName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace.Split('.')[0];
                string[] sArray = path.Split(new string[] { LogName }, StringSplitOptions.RemoveEmptyEntries);
                string aa = sArray[0] + "\\" + LogName + "Log\\";
                path = aa;
                Console.WriteLine(path);
                if (!string.IsNullOrEmpty(path))
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";//
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }
                    if (File.Exists(path))
                    {
                        StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "----" + content);
                        sw.Close();
                    }
                }
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
       
    }

}
