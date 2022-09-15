<%@ Page language="c#" Codebehind="Logon.aspx.cs" AutoEventWireup="false" Inherits="PowerBI.SSO.Logon, PowerBI.SSO" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
 
<HTML>
   <HEAD>
      <title>Power BI 报表服务器</title>
      <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
      <meta name="CODE_LANGUAGE" Content="C#">
      <meta name="vs_defaultClientScript" content="JavaScript">
      <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
   </HEAD>

   <body MS_POSITIONING="GridLayout">
       <div style="float:left; background-color:azure; width:100%; height:100%;">
       <div style=" float:left; background-color:goldenrod; width:40%; height:100%;text-align: center;" > </div>
       <div style="background-color: Green; width:60%;height:100%;float:right;display: table-cell;position: relative; vertical-align: middle; text-align: center;font-family:'Microsoft YaHei UI';">
         <div style="width:60%;height:500px; display: inline-block;margin-top:300px;vertical-align: middle; position: relative; top:50%;margin-top:-50px;margin-left:-50px  ">
      <form id="Form1" method="post" runat="server">
          <div style="height:60px">  <asp:Label id="Label1" style="font-family:'Microsoft YaHei UI';color:#ffffff;font-size:30px" runat="server"
            Width="416px" Height="32px"  meta:resourcekey="Label1Resource1"  Text="报表服务器登录"></asp:Label>


          </div>
          <div style="height:60px">
               <asp:Label id="LblUser" style="font-family:'Microsoft YaHei UI';color:#ffffff;font-size:22px" runat="server"
            Width="96px"   meta:resourcekey="LblUserResource1">登录名:</asp:Label>
               <asp:TextBox id="TxtUser" style="font-family:'Microsoft YaHei UI'" runat="server"
            tabIndex="1" Width="260px" meta:resourcekey="TxtUserResource1"></asp:TextBox>
          </div>
           <div style="height:60px">
                <asp:Label id="LblPwd" style="font-family:'Microsoft YaHei UI';color:#ffffff;font-size:22px" runat="server"
            Width="96px"     meta:resourcekey="LblPwdResource1">密&nbsp;&nbsp;码:</asp:Label>
                        <asp:TextBox id="TxtPwd" style="font-family:'Microsoft YaHei UI'" runat="server"
            tabIndex="2" Width="260px" TextMode="Password" meta:resourcekey="TxtPwdResource1"></asp:TextBox>
           </div>
           <div>  &nbsp; &nbsp;   &nbsp; &nbsp; &nbsp; &nbsp;
               <asp:Button id="BtnLogon" style="font-family:'Microsoft YaHei UI'"
            runat="server" Width="104px" Text="登录" tabIndex="3" meta:resourcekey="BtnLogonResource1"></asp:Button>
               &nbsp; &nbsp;  &nbsp;
                   <asp:Button id="Button1" style="font-family:'Microsoft YaHei UI';"
            runat="server" Width="104px" Text="取消" tabIndex="4" meta:resourcekey="BtnRegisterResource1"></asp:Button>

           </div>
        
        
          <div> <asp:Label id="lblMessage" style="font-family:'Microsoft YaHei UI'"
            runat="server" Width="321px" meta:resourcekey="lblMessageResource1"></asp:Label> </div>
        &nbsp;
        
         <asp:Button id="BtnRegister" style="font-family:'Microsoft YaHei UI';display:none;"
            runat="server" Width="104px" Text="Register User" tabIndex="4" meta:resourcekey="BtnRegisterResource1"></asp:Button>

      
        
       
      </form>

             </div>
    </div>

           </div>
   </body>
  
</HTML>

