using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PBIPortal.Core.Configuration;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Extensions;
using PBIPortal.Core.ManageUser;
using PBIPortal.Core.Services;
using PBIPortal.Core.Utilities;
using PBIPortal.Entity.DomainModels;
using PBIPortal.Entity.DomainModels.PBI;
using PBIPortal.Entity.DomainModels.System;
using System.Data;

namespace PBIPortal.System.Services
{
    public partial class Sys_UserService
    {


        /// <summary>
        /// 加密 AES/ECB/PKCS5Padding  返回Base64编码
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string EncryptDES_ECB_Base64(string pToEncrypt, string sKey)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(sKey);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(pToEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            //rDel.IV = null;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 解密 AES/ECB/PKCS5Padding   Base64编码 返回明文
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DecryptDES_ECB_Base64(string DecryptStr, string Key)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                // byte[] keyArray = Convert.FromBase64String(Key);
                byte[] toEncryptArray = Convert.FromBase64String(DecryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);//  UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<WebResponseContent> UserCodeLogin(string clientId, string usercode,string src)
        {

          

            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            if (clientId.Trim() != "")
            {
                clientId = clientId.ToLower().Trim();
                var builder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .AddEnvironmentVariables()
                  .Build();
                // var configuration = builder.Build();
                //  string sPath = Environment.GetEnvironmentVariable("databaseHost");             
                string UserAESKey = builder[$"UserAESKey:{clientId}"];
                string rusercode =  DecryptDES_ECB_Base64(usercode,UserAESKey);
                string Urlstring = src;

                try
                {
                    rusercode = rusercode.Trim();
                    if (rusercode != "")
                    {
                        if (repository.Exists<PBI_SSO_USER>(x => x.USER_IDCODE == rusercode))
                        {
                            List<PBI_SSO_USER> pBI_SSO_USERs = repository.Find<PBI_SSO_USER>(x => x.USER_IDCODE == rusercode);



                            if (repository.Exists(x => x.UserName == pBI_SSO_USERs[0].USER_LOGIN_NAME))
                            {
                                Sys_User user = repository.FindFirst(x => x.UserName == pBI_SSO_USERs[0].USER_LOGIN_NAME);

                                repository.Update(user, true);
                                string token = JwtHelper.IssueJwt(new UserInfo()
                                {
                                    User_Id = user.User_Id,
                                    UserName = user.UserName,
                                    Role_Id = user.Role_Id,
                                    UserTrueName = user.UserTrueName

                                });
                                user.Token = token;
                                // user.UserPwd = postDatatoken;
                                // repository.Update(user, x => x.Token, true);
                                repository.Update(user, true);
                                UserContext.Current.LogOut(user.User_Id);
                                responseContent.Data = new { token, userName = user.UserName,  user.UserTrueName};

                                Urlstring += "?rs:embed=true&username=" + HttpUtility.HtmlEncode(user.UserName);
                                Urlstring += "&token=" + HttpUtility.HtmlEncode(token);

                                // Urlstring += "?rs:embed=true&username=" +user.UserName;
                                // Urlstring += "&token=" +token;
                                Context.Response.StatusCode = 302;
                                Context.Response.Redirect(Urlstring);

                                //   responseContent.Data = new { postData.token, userName = USER_LOGIN_NAME };
                                return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));

                                //   return Task.FromResult(responseContent.OK());
                            }
                            else
                            {
                                var t = repository.FindAsIQueryable(x => true).OrderByDescending(x => x.User_Id).Take(1);
                                int tid = t.First(x => true).User_Id;
                                Sys_User user = new Sys_User();

                                user.UserName = pBI_SSO_USERs[0].USER_LOGIN_NAME;
                                user.Role_Id = 3;
                                user.RoleName = "普通用户";
                                user.CreateDate = DateTime.Now;
                                user.HeadImageUrl = "";
                                user.UserTrueName = pBI_SSO_USERs[0].USER_TrueName;
                                user.UserPwd = rusercode.Substring(9, 8).EncryptDES(AppSetting.Secret.User);
                                user.Enable = 1;
                                string token = JwtHelper.IssueJwt(new UserInfo()
                                {
                                    User_Id =tid+1,
                                    UserName = user.UserName,
                                    Role_Id = user.Role_Id,
                                    UserTrueName = user.UserTrueName

                                });
                                user.Token = token;
                                repository.Add(user, true);

                                Urlstring += "?rs:embed=true&username=" + HttpUtility.HtmlEncode(user.UserName);
                                Urlstring += "&token=" + HttpUtility.HtmlEncode(token);

                                // Urlstring += "?rs:embed=true&username=" + user.UserName;
                                // Urlstring += "&token=" + token;
                                Context.Response.StatusCode = 302;
                                Context.Response.Redirect(Urlstring);



                                responseContent.Data = new { token, userName = user.UserName, user.UserTrueName };

                                return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));

                            }

                        }
                        else
                        {

                            return Task.FromResult(responseContent.Error("无此用户！"));

                        }

                    }
                    else
                    {

                        return Task.FromResult(responseContent.Error("参数错误！"));
                    }

                           
                     

                }
                catch (Exception ex)
                {

                    msg = ex.Message + ex.StackTrace;
                    return Task.FromResult(responseContent.Error(ResponseType.ServerError));
                }
                finally
                {
                    Logger.Info(LoggerType.Login, rusercode, Urlstring, msg);
                }





            }
            else {

                return Task.FromResult(responseContent.Error("参数错误！"));
            }



        }


        public Task<WebResponseContent> PortalLogin(string token, string src)
        {
            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            var builder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddEnvironmentVariables()
              .Build();
            // var configuration = builder.Build();
            //  string sPath = Environment.GetEnvironmentVariable("databaseHost");
            string SSOUrl = builder["SSOUrl"];
            string SSOclient_id = builder["SSOclient_id"];
            string PBIUrl = builder["PBIUrl"];
            if (src.Substring(0, 1) == "/")
            {
                src = src.Substring(1, src.Length - 1);
            }
            string Urlstring = PBIUrl+src;


            try
            {

                if (token != "")
                {


                    string p = "?client_id=" + SSOclient_id;
                    p += "&token=" + token;
                    SSOUrl = SSOUrl + p;


                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SSOUrl);
                    request.ContentType = "application/json";
                    request.Method = "POST";
                    request.Timeout = 300000;


                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8);
                    string result = reader.ReadToEnd();
                    response.Close();


                    JObject joCenter = (JObject)JsonConvert.DeserializeObject(result);

                    string authAccessFlag = joCenter["authAccessFlag"].ToString();
                    if (authAccessFlag == "false")
                    {


                        return Task.FromResult(responseContent.Error("登录失败！"));


                    }
                    else
                    {


                        string USER_LOGIN_NAME = joCenter["userInfo"]["USER_LOGIN_NAME"].ToString();

                        Logger.Info(LoggerType.Login, USER_LOGIN_NAME, SSOUrl, msg);

                        if (repository.Exists(x => x.UserName == USER_LOGIN_NAME))
                        {
                            Sys_User user = repository.FindFirst(x => x.UserName == USER_LOGIN_NAME);
                            user.UserPwd = token;
                            repository.Update(user, true);
                            string token1 = JwtHelper.IssueJwt(new UserInfo()
                            {
                                User_Id = user.User_Id,
                                UserName = user.UserName,
                                Role_Id = user.Role_Id,
                                UserTrueName = user.UserTrueName

                            });
                            user.Token = token1;
                            user.UserPwd = token;
                            // repository.Update(user, x => x.Token, true);
                            repository.Update(user, true);
                            UserContext.Current.LogOut(user.User_Id);
                            responseContent.Data = new { token1, userName = user.UserName, user.UserTrueName };

                            //   Urlstring += "?rs:embed=true&username=" + HttpUtility.HtmlEncode(user.UserName);
                            Urlstring += "?rs:embed=true";
                            Urlstring += "&token=" + HttpUtility.HtmlEncode(token);

                            // Urlstring += "?rs:embed=true&username=" +user.UserName;
                            // Urlstring += "&token=" +token;
                            Context.Response.StatusCode = 302;
                            Context.Response.Redirect(Urlstring);

                            //   responseContent.Data = new { postData.token, userName = USER_LOGIN_NAME };
                            return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));

                            //   return Task.FromResult(responseContent.OK());
                        }
                        else
                        {
                            var t = repository.FindAsIQueryable(x => true).OrderByDescending(x => x.User_Id).Take(1);
                            int tid = t.First(x => true).User_Id;
                            Sys_User user = new Sys_User();
                            // user.User_Id = tid + 1;
                            user.UserName = USER_LOGIN_NAME;
                            user.Role_Id = 3;
                            user.RoleName = "普通用户";
                            user.CreateDate = DateTime.Now;
                            user.HeadImageUrl = "";
                            user.UserTrueName = USER_LOGIN_NAME;
                            user.UserPwd = token;
                            user.Enable = 1;

                            string token1 = JwtHelper.IssueJwt(new UserInfo()
                            {
                                User_Id = tid + 1,
                                UserName = user.UserName,
                                Role_Id = user.Role_Id,
                                UserTrueName = user.UserTrueName

                            });
                            user.Token = token;
                            repository.Add(user, true);
                            //Urlstring += "?rs:embed=true&username=" + HttpUtility.HtmlEncode(user.UserName);
                          

                            Urlstring += "?rs:embed=true";
                            Urlstring += "&token=" + HttpUtility.HtmlEncode(token);


                            // Urlstring += "?rs:embed=true&username=" + user.UserName;
                            // Urlstring += "&token=" + token;
                            Context.Response.StatusCode = 302;
                            Context.Response.Redirect(Urlstring);



                            responseContent.Data = new { token, userName = user.UserName, user.UserTrueName };

                            return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));

                        }

                    }


                }
                else
                {
                    return Task.FromResult(responseContent.Error("参数错误！"));
                }

            }
            catch (Exception ex)
            {

                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Login, token, responseContent.Message, msg);
            }






        }




        /// <summary>
        /// WebApi登陆
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        /// 
        public Task<WebResponseContent> SSOLogin(string sign)
        {
            string msg = string.Empty;     
            string sqlstr = "";
            string UserName = "";

            var builder = new ConfigurationBuilder()
                         .AddJsonFile("appsettings.json")
                         .AddEnvironmentVariables()
                         .Build();
            // var configuration = builder.Build();
            //  string sPath = Environment.GetEnvironmentVariable("databaseHost");
            string PBIUrl = builder["PBIUrl"];



            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                 
                if (sign.Trim() != "")
                {
                    
                    sqlstr = $"select top 1 [USER_ID]  ,[USER_TrueName]   ,[USER_LOGIN_NAME]   ,[ODEPT_CODE] ,[USER_IDCODE] from PBI_SSO_USER where substring(sys.fn_sqlvarbasetostr(HashBytes('MD5',CONVERT(varchar, getdate(),23)+rtrim(ltrim(CONVERT(varchar, USER_LOGIN_NAME))))),3,32)='{sign}'";

                    var userReader = repository.DapperContext.ExecuteReader(sqlstr, "", CommandType.Text);
                    if (userReader.Read())
                    {
                        UserName = userReader["USER_LOGIN_NAME"].ToString().Trim();

                        if (repository.Exists(x => x.UserName == UserName))
                        {
                            Sys_User user = repository.FindFirst(x => x.UserName == UserName);
                            string token = JwtHelper.IssueJwt(new UserInfo()
                            {
                                User_Id = user.User_Id,
                                UserName = user.UserName,
                                Role_Id = user.Role_Id,
                                UserTrueName = user.UserTrueName

                            });
                            user.Token = token;
                            // repository.Update(user, x => x.Token, true);
                            repository.Update(user, true);
                            UserContext.Current.LogOut(user.User_Id);
                            responseContent.Data = new { token, userName = user.UserName, img = user.HeadImageUrl, user.Role_Id, user.UserTrueName, PBIUrl, pbitoken = user.UserPwd };

                            //   responseContent.Data = new { postData.token, userName = USER_LOGIN_NAME };
                            return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));

                            //   return Task.FromResult(responseContent.OK());
                        }
                        else
                        {
                            var t = repository.FindAsIQueryable(x => true).OrderByDescending(x => x.User_Id).Take(1);
                            int tid = t.First(x => true).User_Id;

                            Sys_User user = new Sys_User();
                            user.UserName = UserName;
                            user.Role_Id = 3;
                            user.RoleName = "普通用户";
                            user.CreateDate = DateTime.Now;
                            user.HeadImageUrl = "";
                            user.UserTrueName = userReader["USER_TrueName"].ToString().Trim();
                            user.UserPwd = userReader["USER_IDCODE"].ToString().Trim().Substring(10, 8).EncryptDES(AppSetting.Secret.User);
                            user.Enable = 1;
                            string token = JwtHelper.IssueJwt(new UserInfo()
                            {
                                User_Id = tid + 1,
                                UserName = user.UserName,
                                Role_Id = user.Role_Id,
                                UserTrueName = user.UserTrueName

                            });
                            user.Token = token;
                            repository.Add(user, true);

                            responseContent.Data = new { token, userName = user.UserName, img = user.HeadImageUrl, user.Role_Id, user.UserTrueName, PBIUrl, pbitoken = user.UserPwd };
                            return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));

                        }

                    }
                    else
                    {

                        sqlstr = $"select top 1 username from Sys_User where substring(sys.fn_sqlvarbasetostr(HashBytes('MD5', CONVERT(varchar, getdate(), 23) + rtrim(ltrim(CONVERT(varchar, username))))), 3, 32) = '{sign}'";
                        var sysuserReader = repository.DapperContext.ExecuteReader(sqlstr, "", CommandType.Text);
                        if (sysuserReader.Read())
                        {
                            UserName = sysuserReader["username"].ToString().Trim();
                            Sys_User user = repository.FindFirst(x => x.UserName == UserName);
                            string token = JwtHelper.IssueJwt(new UserInfo()
                            {
                                User_Id = user.User_Id,
                                UserName = user.UserName,
                                Role_Id = user.Role_Id,
                                UserTrueName = user.UserTrueName

                            });
                            user.Token = token;
                            // repository.Update(user, x => x.Token, true);
                            repository.Update(user, true);
                            UserContext.Current.LogOut(user.User_Id);
                            responseContent.Data = new { token, userName = user.UserName, img = user.HeadImageUrl, user.Role_Id, user.UserTrueName, PBIUrl, pbitoken = user.UserPwd };

                            //   responseContent.Data = new { postData.token, userName = USER_LOGIN_NAME };
                            return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));


                        }
                        else
                        {
                            return Task.FromResult(responseContent.Error("未找到用户"));

                        }
                    

                    }


                }
                else
                {
                    return Task.FromResult(responseContent.Error("参数错误！"));

                }


            }
            catch (Exception ex)
            {

                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError));
            }
            finally
            {
                Logger.Info(LoggerType.Login, "SSOlogin", "SSOlogin", msg);
            }
        }



        

        

        public  Task<WebResponseContent> PBILogin(PostData postData)
        {
            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            var builder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddEnvironmentVariables()
              .Build();
            // var configuration = builder.Build();
            //  string sPath = Environment.GetEnvironmentVariable("databaseHost");
            string SSOUrl= builder["SSOUrl"];
           string SSOclient_id= builder["SSOclient_id"];
            string PBIUrl = builder["PBIUrl"];
             

            try
            {

                if (postData.token != "")
                    {


                        string p = "?client_id=" + SSOclient_id;
                        p += "&token=" + postData.token;
                        SSOUrl = SSOUrl + p;


                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SSOUrl);
                        request.ContentType = "application/json";
                        request.Method = "POST";
                        request.Timeout = 300000;


                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8);
                        string result = reader.ReadToEnd();
                        response.Close();

                    
                        JObject joCenter = (JObject)JsonConvert.DeserializeObject(result);

                        string authAccessFlag = joCenter["authAccessFlag"].ToString();
                        if (authAccessFlag == "false")
                        {


                             return Task.FromResult(responseContent.Error("登录失败！"));
                              

                         }
                        else
                        {
                          
                            
                            string USER_LOGIN_NAME = joCenter["userInfo"]["USER_LOGIN_NAME"].ToString();

                            Logger.Info(LoggerType.Login, USER_LOGIN_NAME, SSOUrl, msg);

                        if (repository.Exists(x => x.UserName == USER_LOGIN_NAME))
                            {
                                 Sys_User user = repository.FindFirst(x => x.UserName == USER_LOGIN_NAME);
                                 user.UserPwd = postData.token;
                                 repository.Update(user, true);
                                 string token = JwtHelper.IssueJwt(new UserInfo()
                                {
                                    User_Id = user.User_Id,
                                    UserName = user.UserName,
                                    Role_Id = user.Role_Id,
                                    UserTrueName = user.UserTrueName

                                });
                                user.Token = token;
                                user.UserPwd = postData.token;
                            // repository.Update(user, x => x.Token, true);
                            repository.Update(user, true);
                            UserContext.Current.LogOut(user.User_Id);
                            responseContent.Data = new { token, userName = user.UserName, img = user.HeadImageUrl, user.Role_Id, user.UserTrueName, PBIUrl,pbitoken=user.UserPwd};


                            //   responseContent.Data = new { postData.token, userName = USER_LOGIN_NAME };
                                return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));

                         //   return Task.FromResult(responseContent.OK());
                            }
                            else
                            {
                                var t = repository.FindAsIQueryable(x => true).OrderByDescending(x=>x.User_Id).Take(1);
                                int tid = t.First(x => true).User_Id;
                                Sys_User user = new Sys_User();
                                // user.User_Id = tid + 1;
                                 user.UserName = USER_LOGIN_NAME;
                                 user.Role_Id = 3;
                                 user.RoleName = "普通用户";
                                 user.CreateDate = DateTime.Now;
                                 user.HeadImageUrl = "";
                                 user.UserTrueName = USER_LOGIN_NAME;
                                 user.UserPwd= postData.token;
                                 user.Enable = 1;
                                 
                                string token = JwtHelper.IssueJwt(new UserInfo()
                                {
                                    User_Id = tid + 1,
                                    UserName = user.UserName,
                                    Role_Id = user.Role_Id,
                                    UserTrueName = user.UserTrueName

                                });
                                user.Token = token;
                                repository.Add(user,true);
 
                            responseContent.Data = new { token, userName = user.UserName, img = user.HeadImageUrl, user.Role_Id, user.UserTrueName, PBIUrl, pbitoken = user.UserPwd };

                            return Task.FromResult(responseContent.OK(ResponseType.LoginSuccess));

                        }

                         }


                      }
                    else
                    {
                    return Task.FromResult(responseContent.Error("参数错误！"));
                    }

            }
            catch (Exception ex)
            {

                msg = ex.Message + ex.StackTrace;
                return Task.FromResult(responseContent.Error(ResponseType.ServerError)); 
            }
            finally
            {
                Logger.Info(LoggerType.Login, postData.token, responseContent.Message, msg);
            }


        }
        public async Task<WebResponseContent> Login(LoginInfo loginInfo, bool verificationCode = true)
        {

            var builder = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .AddEnvironmentVariables()
                 .Build();
            // var configuration = builder.Build();
            //  string sPath = Environment.GetEnvironmentVariable("databaseHost");
          string   PBIUrl = builder["PBIUrl"];

            string msg = string.Empty;
            WebResponseContent responseContent = new WebResponseContent();
            try
            {
                Sys_User user = await repository.FindAsIQueryable(x => x.UserName == loginInfo.UserName)
                    .FirstOrDefaultAsync();

                if (user == null || loginInfo.PassWord.Trim() != (user.UserPwd ?? "").DecryptDES(AppSetting.Secret.User))
                    return responseContent.Error(ResponseType.LoginError);

                string token = JwtHelper.IssueJwt(new UserInfo()
                {
                    User_Id = user.User_Id,
                    UserName = user.UserName,
                    Role_Id = user.Role_Id,
                    UserTrueName = user.UserTrueName

                });
                user.Token = token;
                responseContent.Data = new { token, userName = user.UserName, img = user.HeadImageUrl,user.Role_Id,user.UserTrueName, PBIUrl, pbitoken ="" };
                repository.Update(user, x => x.Token, true);
                UserContext.Current.LogOut(user.User_Id);

                loginInfo.PassWord = string.Empty;

                return responseContent.OK(ResponseType.LoginSuccess);
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return responseContent.Error(ResponseType.ServerError);
            }
            finally
            {
                Logger.Info(LoggerType.Login, loginInfo.Serialize(), responseContent.Message, msg);
            }
        }

        /// <summary>
        ///当token将要过期时，提前置换一个新的token
        /// </summary>
        /// <returns></returns>
        public async Task<WebResponseContent> ReplaceToken()
        {
            WebResponseContent responseContent = new WebResponseContent();
            string error = "";
            UserInfo userInfo = null;
            try
            {
                string requestToken = HttpContext.Current.Request.Headers[AppSetting.TokenHeaderName];
                requestToken = requestToken?.Replace("Bearer ", "");
                if (UserContext.Current.Token != requestToken) return responseContent.Error("Token已失效!");

                if (JwtHelper.IsExp(requestToken)) return responseContent.Error("Token已过期!");

                int userId = UserContext.Current.UserId;
                userInfo = await
                     repository.FindFirstAsync(x => x.User_Id == userId,
                     s => new UserInfo()
                     {
                         User_Id = userId,
                         UserName = s.UserName,
                         UserTrueName = s.UserTrueName,
                         Role_Id = s.Role_Id,
                         RoleName = s.RoleName
                     });

                if (userInfo == null) return responseContent.Error("未查到用户信息!");

                string token = JwtHelper.IssueJwt(userInfo);
                //移除当前缓存
                base.CacheContext.Remove(userId.GetUserIdKey());
                //只更新的token字段
                repository.Update(new Sys_User() { User_Id = userId, Token = token }, x => x.Token, true);
                responseContent.OK(null, token);
            }
            catch (Exception ex)
            {
                error = ex.Message + ex.StackTrace + ex.Source;
                responseContent.Error("token替换出错了..");
            }
            finally
            {
                Logger.Info(LoggerType.ReplaceToeken, ($"用户Id:{userInfo?.User_Id},用户{userInfo?.UserTrueName}")
                    + (responseContent.Status ? "token替换成功" : "token替换失败"), null, error);
            }
            return responseContent;
        }

        public async Task<WebResponseContent> ModifyUserInfo(PBIuserinfo sys_User)
        {
            WebResponseContent webResponse = new WebResponseContent();       
            string message = "";
            try
            {
                var t = repository.FindFirst(x => x.UserName == sys_User.UserName);
                t.Address = sys_User.Address;
                t.Tel = sys_User.Tel;
                t.Remark = sys_User.Remark;
                t.Email = sys_User.Email;    
              
              
                await Task.Run(() =>
                {
                    repository.Update(t, true);                        });

                webResponse.OK("信息修改成功");
            }
            catch (Exception ex)
            {
                message = ex.Message+ex.StackTrace;
                Console.WriteLine(message);
                webResponse.Error("服务器了点问题,请稍后再试");
            }
            finally
            {
                if (message == "")
                {
                    Logger.OK(LoggerType.ApiModifyPwd, "信息修改成功");
                }
                else
                {
                    Logger.Error(LoggerType.ApiModifyPwd, message);
                }
            }
            return webResponse;


        }            /// <summary>
            /// 修改密码
            /// </summary>
            /// <param name="parameters"></param>
            /// <returns></returns>
            public async Task<WebResponseContent> ModifyPwd(string oldPwd, string newPwd)
        {
            oldPwd = oldPwd?.Trim();
            newPwd = newPwd?.Trim();
            string message = "";
            WebResponseContent webResponse = new WebResponseContent();
            try
            {
                if (string.IsNullOrEmpty(oldPwd)) return webResponse.Error("旧密码不能为空");
                if (string.IsNullOrEmpty(newPwd)) return webResponse.Error("新密码不能为空");
                if (newPwd.Length < 6) return webResponse.Error("密码不能少于6位");

                int userId = UserContext.Current.UserId;
                string userCurrentPwd = await base.repository.FindFirstAsync(x => x.User_Id == userId, s => s.UserPwd);

                string _oldPwd = oldPwd.EncryptDES(AppSetting.Secret.User);
                if (_oldPwd != userCurrentPwd) return webResponse.Error("旧密码不正确");

                string _newPwd = newPwd.EncryptDES(AppSetting.Secret.User);
                if (userCurrentPwd == _newPwd) return webResponse.Error("新密码不能与旧密码相同");

                await Task.Run(() =>
                {
                    base.repository.Update(new Sys_User
                    {
                        User_Id = userId,
                        UserPwd = _newPwd,
                        LastModifyPwdDate = DateTime.Now
                    }, x => new { x.UserPwd, x.LastModifyPwdDate }, true);
                });

                webResponse.OK("密码修改成功");
            }
            catch (Exception ex)
            {
                message = ex.Message;
                webResponse.Error("服务器了点问题,请稍后再试");
            }
            finally
            {
                if (message == "")
                {
                    Logger.OK(LoggerType.ApiModifyPwd, "密码修改成功");
                }
                else
                {
                    Logger.Error(LoggerType.ApiModifyPwd, message);
                }
            }
            return webResponse;
        }
        /// <summary>
        /// 个人中心获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<WebResponseContent> GetCurrentUserInfo()
        {
            var data = await base.repository
                .FindAsIQueryable(x => x.User_Id == UserContext.Current.UserId)
                .Select(s => new
                {
                    s.UserName,
                    s.UserTrueName,
                    s.Address,
                    s.PhoneNo,
                    s.Email,
                    s.Remark,
                    s.Gender,
                    s.RoleName,
                    s.HeadImageUrl,
                    s.CreateDate,
                    s.Tel
                })
                .FirstOrDefaultAsync();
            return new WebResponseContent().OK(null, data);
        }

        /// <summary>
        /// 设置固定排序方式及显示用户过滤
        /// </summary>
        /// <param name="pageData"></param>
        /// <returns></returns>
        public override PageGridData<Sys_User> GetPageData(PageDataOptions pageData)
        {

            QueryRelativeExpression = (IQueryable<Sys_User> queryable) =>
            {
                if (UserContext.Current.IsSuperAdmin) return queryable;

                //查看用户时，只能看下自己角色下的所有用户
                List<int> roleIds = Sys_RoleService
                   .Instance
                   .GetAllChildrenRoleId(UserContext.Current.RoleId).Result;
                //roleIds.Contains(x.Role_Id) || x.User_Id == UserContext.Current.UserId此处查询存在性能问题，根据实际情况自行解决
                return queryable.Where(x => roleIds.Contains(x.Role_Id) || x.User_Id == UserContext.Current.UserId);
            };
            base.OrderByExpression = x => new Dictionary<object, Core.Enums.QueryOrderBy>() {
                { x.CreateDate, Core.Enums.QueryOrderBy.Desc },
                { x.User_Id,Core.Enums.QueryOrderBy.Desc}
            };
            return base.GetPageData(pageData);
        }

        /// <summary>
        /// 新建用户，根据实际情况自行处理
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public override WebResponseContent Add(SaveModel saveModel)
        {
            WebResponseContent responseData = new WebResponseContent();
            base.AddOnExecute = (SaveModel userModel) =>
            {
                int roleId = userModel?.MainData?["Role_Id"].GetInt() ?? 0;
                if (roleId > 0)
                {
                    string roleName = GetChildrenName(roleId);
                    if (!UserContext.Current.IsSuperAdmin || roleId == 1 || string.IsNullOrEmpty(roleName))
                        return responseData.Error("不能选择此角色");
                    //选择新建的角色ID，手动添加角色ID的名称
                    userModel.MainData["RoleName"] = roleName;
                }
                return responseData.OK();
            };


            ///生成6位数随机密码
            string pwd = 6.GenerateRandomNumber();
            //在AddOnExecuting之前已经对提交的数据做过验证是否为空
            base.AddOnExecuting = (Sys_User user, object obj) =>
            {
                user.UserName = user.UserName.Trim();
                if (repository.Exists(x => x.UserName == user.UserName))
                    return responseData.Error("用户名已经被注册");
                user.UserPwd = pwd.EncryptDES(AppSetting.Secret.User);
                //设置默认头像
                return responseData.OK();
            };

            base.AddOnExecuted = (Sys_User user, object list) =>
            {
                return responseData.OK($"用户新建成功.帐号{user.UserName}密码{pwd}");
            };
            return base.Add(saveModel); ;
        }

        /// <summary>
        /// 删除用户拦截过滤
        /// 用户被删除后同时清空对应缓存
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="delList"></param>
        /// <returns></returns>
        public override WebResponseContent Del(object[] keys, bool delList = false)
        {
            base.DelOnExecuting = (object[] ids) =>
            {
                int[] userIds = ids.Select(x => Convert.ToInt32(x)).ToArray();
                //校验只能删除当前角色下能看到的用户
                var xxx = repository.Find(x => userIds.Contains(x.User_Id));
                var delUserIds = repository.Find(x => userIds.Contains(x.User_Id), s => new { s.User_Id, s.Role_Id, s.UserTrueName });
                List<int> roleIds = Sys_RoleService
                   .Instance
                   .GetAllChildrenRoleId(UserContext.Current.RoleId)
                   .Result;

                string[] userNames = delUserIds.Where(x => !roleIds.Contains(x.Role_Id))
                 .Select(s => s.UserTrueName)
                 .ToArray();
                if (userNames.Count() > 0)
                {
                    return new WebResponseContent().Error($"没有权限删除用户：{string.Join(',', userNames)}");
                }
                return new WebResponseContent().OK();
            };
            base.DelOnExecuted = (object[] userIds) =>
            {
                var objKeys = userIds.Select(x => x.GetInt().GetUserIdKey());
                base.CacheContext.RemoveAll(objKeys);
                return new WebResponseContent() { Status = true };
            };
            return base.Del(keys, delList);
        }

        private string GetChildrenName(int roleId)
        {
            //只能修改当前角色能看到的用户
            string roleName = Sys_RoleService
                .Instance
                .GetAllChildren(UserContext.Current.UserInfo.Role_Id)
                .Result.Where(x => x.Id == roleId)
                .Select(s => s.RoleName).FirstOrDefault();
            return roleName;
        }

        /// <summary>
        /// 修改用户拦截过滤
        /// 
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public override WebResponseContent Update(SaveModel saveModel)
        {
            WebResponseContent responseContent = new WebResponseContent();
            UserInfo userInfo = UserContext.Current.UserInfo;
            //禁止修改用户名
            base.UpdateOnExecute = (SaveModel saveInfo) =>
            {
                int roleId = saveModel.MainData["Role_Id"].GetInt();
                string roleName = GetChildrenName(roleId);

                if (roleId == 1)
                    return responseContent.Error("不能选择超级管理员");

                if (UserContext.IsRoleIdSuperAdmin(userInfo.Role_Id)|| UserContext.IsRoleIdAdmin(userInfo.Role_Id))
                {
                    saveInfo.MainData.Add("RoleName", roleName);
                    return responseContent.OK();
                }
                if (saveInfo.MainData.ContainsKey("RoleName"))
                    saveInfo.MainData.Remove("RoleName");
                if (string.IsNullOrEmpty(roleName)) return responseContent.Error("不能选择此角色");

                return responseContent.OK();
            };
            base.UpdateOnExecuting = (Sys_User user, object obj1, object obj2, List<object> list) =>
            {
                if (user.User_Id == userInfo.User_Id && user.Role_Id != userInfo.Role_Id)
                    return responseContent.Error("不能修改自己的角色");

            

                var _user = repository.Find(x => x.User_Id == user.User_Id,
                    s => new { s.UserName, s.UserPwd })
                    .FirstOrDefault();
                user.UserName = _user.UserName;
                //Sys_User实体的UserPwd用户密码字段的属性不是编辑，此处不会修改密码。但防止代码生成器将密码字段的修改成了可编辑造成密码被修改
                user.UserPwd = _user.UserPwd;
                return responseContent.OK();
            };
            //用户信息被修改后，将用户的缓存信息清除
            base.UpdateOnExecuted = (Sys_User user, object obj1, object obj2, List<object> List) =>
            {
                base.CacheContext.Remove(user.User_Id.GetUserIdKey());
                return new WebResponseContent(true);
            };
            return base.Update(saveModel);
        }

        /// <summary>
        /// 导出处理
        /// </summary>
        /// <param name="pageData"></param>
        /// <returns></returns>
        public override WebResponseContent Export(PageDataOptions pageData)
        {
            //限定只能导出当前角色能看到的所有用户
            QueryRelativeExpression = (IQueryable<Sys_User> queryable) =>
            {
                if (UserContext.Current.IsSuperAdmin) return queryable;
                List<int> roleIds = Sys_RoleService
                 .Instance
                 .GetAllChildrenRoleId(UserContext.Current.RoleId)
                 .Result;
                return queryable.Where(x => roleIds.Contains(x.Role_Id) || x.User_Id == UserContext.Current.UserId);
            };

            base.ExportOnExecuting = (List<Sys_User> list, List<string> ignoreColumn) =>
            {
                if (!ignoreColumn.Contains("Role_Id"))
                {
                    ignoreColumn.Add("Role_Id");
                }
                if (!ignoreColumn.Contains("RoleName"))
                {
                    ignoreColumn.Remove("RoleName");
                }
                WebResponseContent responseData = new WebResponseContent(true);
                return responseData;
            };
            return base.Export(pageData);
        }
    }
}

