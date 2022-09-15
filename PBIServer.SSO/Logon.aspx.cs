#region
// Copyright (c) 2016 Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License (MIT)
/*============================================================================
  File:     Logon.aspx.cs
  Summary:  The code-behind for a logon page that supports Forms
            Authentication in a custom security extension    
--------------------------------------------------------------------
  This file is part of Microsoft SQL Server Code Samples.
    
 This source code is intended only as a supplement to Microsoft
 Development Tools and/or on-line documentation. See these other
 materials for detailed information regarding Microsoft code 
 samples.

 THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF 
 ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
 THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 PARTICULAR PURPOSE.
===========================================================================*/
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.Security;
using Microsoft.ReportingServices.Interfaces;
using PowerBI.SSO.App_LocalResources;
using System.Globalization;
using System.Security.Claims;
using System.IO;
using System.Configuration;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PowerBI.SSO
{
   public class Logon : System.Web.UI.Page
   {
      protected System.Web.UI.WebControls.Label LblUser;
      protected System.Web.UI.WebControls.TextBox TxtPwd;
      protected System.Web.UI.WebControls.TextBox TxtUser;
      protected System.Web.UI.WebControls.Button BtnRegister;
      protected System.Web.UI.WebControls.Button BtnLogon;
      protected System.Web.UI.WebControls.Label lblMessage;
      protected System.Web.UI.WebControls.Label Label1;
      protected System.Web.UI.WebControls.Label LblPwd;
      public Common common = new Common();

     
      private void Page_Load(object sender, System.EventArgs e)
      {
            try
            {
                BtnRegister.Enabled = false;
                string paramToken = "";
                string pbiToken = "";
                string username = "";
                 string PWD = "";
                string sign = "";

                //string SSOUrl = common.GetConfig("SSOUrl");
               // string SSOclient_id = common.GetConfig("SSOclient_id");
                string ReportServer_Url = common.GetConfig("webUrl");

               // common.WriteLogs(SSOUrl);
               // common.WriteLogs(SSOclient_id);

                // string ReportServer_Url = common.GetWebUrl(); ;



                common.WriteLogs("ReportServer_Url:" + ReportServer_Url);

                // string ReportServer_Url =  Properties.Settings.Default.WebURL;

                var redirectUrl = new Uri(ReportServer_Url + Request.QueryString["ReturnUrl"]);
                var redirectQuery = HttpUtility.ParseQueryString(redirectUrl.Query);
                var accessUrl = new Uri(ReportServer_Url + redirectQuery.Get("url"));
                var accessQuery = HttpUtility.ParseQueryString(accessUrl.Query);
                string url = HttpContext.Current.Request.Url.AbsolutePath;
                //(或 string url = HttpContext.Current.Request.Path;)


                //common.WriteLogs("redirectQuery:" + redirectQuery);
                //common.WriteLogs("accessUrl:" + accessUrl);
                //common.WriteLogs("accessQuery:" + accessQuery);
                // common.WriteLogs("redirectQuery:" + redirectQuery.Get("url"));
                //common.WriteLogs("url:" + HttpUtility.UrlDecode(accessUrl.AbsolutePath));
                // common.WriteLogs("url:" + HttpUtility.UrlEncode(accessUrl.AbsolutePath));

           

                    // string dbConStr = ConfigurationManager.AppSettings["Database_ConnectionString"].ToString();

                    // lblMessage.Text =   HttpContext.Current.Request.PhysicalApplicationPath + "\\config.json";
                    // Authorization.path = lblMessage.Text;
                    // string dbConStr = "Data Source = 172.16.10.30; Initial Catalog=ycdw; Persist Security Info = True; User ID=sa; Password =1qaz@WSX; Connect Timeout = 500;";
                    //string paramData = accessQuery.Get("Data");


                if (accessQuery.Get("sign") != null)
                {
                    sign = accessQuery.Get("sign");
                    username = common.GetLoginUserName(sign);
                    if (username.Trim() != "")
                    {
                        common.SetCookie(username);                        
                        FormsAuthentication.RedirectFromLoginPage(username, false);

                    }
                }

                if (accessQuery.Get("pbitoken") != null)
                {
                    pbiToken = accessQuery.Get("pbitoken");
                }
                if (accessQuery.Get("token") != null)
                {
                    paramToken = accessQuery.Get("token");
                }
                if (accessQuery.Get("username") != null)
                {
                    username = accessQuery.Get("username");
                }
                else
                {

                    if (common.GetCookie("PBIUserName").Trim() != "")
                    {
                        common.WriteLogs("获取cookie：" + common.GetCookie("PBIUserName").Trim());
                        username = common.GetCookie("PBIUserName").Trim();
                        if (username.Trim() != "")
                        {
                            FormsAuthentication.RedirectFromLoginPage(username, false);

                        }
                    }


                }

                if (accessQuery.Get("PWD") != null)
                {
                    PWD = accessQuery.Get("PWD");
                }

               // common.WriteLogs("paramToken:" + paramToken);
              //  common.WriteLogs("pbiToken:" + pbiToken);
                common.WriteLogs("username:" + username);
              //  common.setCookie(username);
                //  common.WriteLogs("PWD:" + PWD);


                bool identity = false;

                if (username != "" && PWD != "")
                {
                     
                    identity = AuthenticationUtilities.VerifyPassword(username, PWD);

                    if (identity)
                    {
                        var principal = new ClaimsPrincipal();
                        //   principal.AddIdentity(identity);
                        common.SetCookie(username);
                        FormsAuthentication.RedirectFromLoginPage(username, false);
                    }
                    else
                    {
                        lblMessage.Text = "密码错误！";
                    }
                }

                if (username != "" && paramToken != "")
                {

                    identity = AuthenticationUtilities.VerifyPassword(username, paramToken);

                    if (identity)
                    {
                        var principal = new ClaimsPrincipal();
                        //   principal.AddIdentity(identity);
                        common.SetCookie(username);
                        FormsAuthentication.RedirectFromLoginPage(username, false);
                    }
                    else
                    {
                        lblMessage.Text = "无效Token！";
                    }
                }


                if (username != "" && pbiToken != "")
                {

                    identity = AuthenticationUtilities.VerifyPassword(username, pbiToken);

                    if (identity)
                    {
                        var principal = new ClaimsPrincipal();
                        //   principal.AddIdentity(identity);
                        common.SetCookie(username);
                        FormsAuthentication.RedirectFromLoginPage(username, false);
                    }
                    else
                    {
                        lblMessage.Text = "无效Token！";
                    }
                }
          


            }
            catch (Exception ex)
            {
                common.WriteLogs(string.Format("Logon::Error!{0}", ex.ToString()));                
                lblMessage.Text =  ex.Message;
            }


        }

      #region Web Form Designer generated code
      override protected void OnInit(EventArgs e)
      {
            InitializeComponent();
            base.OnInit(e);
      }
      


   

      private void InitializeComponent()
      {    
         this.BtnLogon.Click += new System.EventHandler(this.ServerBtnLogon_Click);
         this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
         this.Load += new System.EventHandler(this.Page_Load);

      }
      #endregion

       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
      private void BtnRegister_Click(object sender, 
         System.EventArgs e)
      {
         string salt = AuthenticationUtilities.CreateSalt(5);
         string passwordHash =
            AuthenticationUtilities.CreatePasswordHash(TxtPwd.Text, salt);
         if (AuthenticationUtilities.ValidateUserName(TxtUser.Text))
         {
            try
            {
               AuthenticationUtilities.StoreAccountDetails(
                  TxtUser.Text, passwordHash, salt);
            }
            catch(Exception ex)
            {
              lblMessage.Text = string.Format(CultureInfo.InvariantCulture, ex.Message);
            }
         }
         else
         {

           lblMessage.Text = string.Format(CultureInfo.InvariantCulture,
               Logon_aspx.UserNameError);
         }
      }

 


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
      private void ServerBtnLogon_Click(object sender, 
         System.EventArgs e)
      {
         bool passwordVerified = false;
         try
         {
                if (TxtUser.Text.Trim() == ""|| TxtPwd.Text.Trim()=="")
                {
                    return;

                }
            passwordVerified = 
               AuthenticationUtilities.VerifyPassword(TxtUser.Text.Trim(),TxtPwd.Text.Trim());
            if (passwordVerified)
            {
                    Authorization.path = "/";
               FormsAuthentication.RedirectFromLoginPage(
                  TxtUser.Text.Trim(), false);
            }
            else
            {
               Response.Redirect("logon.aspx");
            }
         }
         catch(Exception ex)
         {
           lblMessage.Text = string.Format(CultureInfo.InvariantCulture, ex.Message);
                common.WriteLogs(ex.Message);
            return;
         }
         if (passwordVerified == true )
         {
            // The user is authenticated
            // At this point, an authentication ticket is normally created
            // This can subsequently be used to generate a GenericPrincipal
            // object for .NET authorization purposes
            // For details, see "How To: Use Forms authentication with 
            // GenericPrincipal objects
           lblMessage.Text = string.Format(CultureInfo.InvariantCulture,
              Logon_aspx.LoginSuccess);
           BtnRegister.Enabled = false;
         }
         else
         {
              // lblMessage.Text = string.Format(CultureInfo.InvariantCulture,
            //  Logon_aspx.InvalidUsernamePassword);
                lblMessage.Text = "密码错误！";

         }
      }

       
    }
}
