using PBIPortal.Core.BaseProvider;
using PBIPortal.Core.Utilities;
using PBIPortal.Entity.DomainModels;
using System.Threading.Tasks;
using PBIPortal.Entity.DomainModels.PBI;
using PBIPortal.Entity.DomainModels.System;

namespace PBIPortal.System.IServices
{
    public partial interface ISys_UserService
    {

        Task<WebResponseContent> Login(LoginInfo loginInfo, bool verificationCode = true);
        Task<WebResponseContent> ReplaceToken();
        Task<WebResponseContent> ModifyPwd(string oldPwd, string newPwd);
        Task<WebResponseContent> GetCurrentUserInfo();

        Task<WebResponseContent> PBILogin(PostData token);
        Task<WebResponseContent> SSOLogin(string sign);
        Task<WebResponseContent> UserCodeLogin(string clientId, string usercode, string src);
        Task<WebResponseContent> ModifyUserInfo(PBIuserinfo sys_User);

        Task<WebResponseContent> PortalLogin(string token, string src);


    }
}

