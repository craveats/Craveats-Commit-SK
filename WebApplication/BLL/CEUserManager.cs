using Generic.Obfuscation.TripleDES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using WebApplication.DAL;
using WebApplication.DAL.DBCommon;
using WebApplication.Models;
using WebApplication.Models.ViewModel;

namespace WebApplication
{
    public class CEUserManager
    {
        private bool UserExistsForEmail(string emailAddress)
        {
            try
            {
                using (CraveatsDbContext craveatsDbContext = new CraveatsDbContext())
                {
                    return craveatsDbContext.GetUserByEmail(emailAddress).Count() > 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsRegistered(string emailAddress)
        {
            return UserExistsForEmail(emailAddress);
        }

        internal int? RegisterNew(string email, string hashedPassword, string role)
        {
            try {
                Common.UserTypeEnum registeringRole = Common.CommonUtility.GetEnumValueFromDescription<Common.UserTypeEnum>(role);

                if (!(registeringRole.HasFlag(Common.UserTypeEnum.CraveatsDiner) ||
                    registeringRole.HasFlag(Common.UserTypeEnum.PartnerRestaurant)))
                {
                    throw new InvalidOperationException("Requested role could not be used in this context.");
                }

                using (CraveatsDbContext craveatsDbContext = new CraveatsDbContext()) {
                    DBCommonUtility dBCommonUtility = new DBCommonUtility();
                    string sqlCmdParamString = "";
                    SqlParameter[] sqlParameters = dBCommonUtility.GetSqlParameters(
                        new object[] {
                                        email,
                                        hashedPassword,
                                        (int) registeringRole},
                        out sqlCmdParamString
                        , true);
                    StringBuilder sbRawSQL = new StringBuilder("exec RegisterNewActiveUser");
                    sbRawSQL.AppendFormat(" {0}", sqlCmdParamString.Trim());

                    User newUser = craveatsDbContext.User.SqlQuery(
                        sql: sbRawSQL.ToString(),
                        parameters: sqlParameters
                    ).FirstOrDefault();

                    return newUser?.Id;
                }
            }
            catch (Exception e){
                throw e;
            }
        }

        internal void SaveUserDetail(UserDTO userDTO)
        {
            try
            {
                using (CraveatsDbContext craveatsDbContext = new CraveatsDbContext())
                {
                    int userId = int.Parse(DataSecurityTripleDES.GetPlainText(userDTO.Id));
                    User anUser = craveatsDbContext.User.FirstOrDefault(u => u.Id == userId);

                    anUser = EntityDTOHelper.MapToEntity<UserDTO, User>(userDTO, anUser);

                    craveatsDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public User GetSigningUserByEmail(string emailAddress)
        {
            try
            {
                using (CraveatsDbContext craveatsDbContext = new CraveatsDbContext())
                {
                    DBCommonUtility dBCommonUtility = new DBCommonUtility();
                    string sqlCmdParamString = "";
                    SqlParameter[] sqlParameters = dBCommonUtility.GetSqlParameters(
                        new object[] {
                            emailAddress
                        },
                        out sqlCmdParamString
                        , true);
                    StringBuilder sbRawSQL = new StringBuilder("exec GetSigningUserByEmail");
                    sbRawSQL.AppendFormat(" {0}", sqlCmdParamString.Trim());

                    User newUser = craveatsDbContext.User.SqlQuery(
                        sql: sbRawSQL.ToString(),
                        parameters: sqlParameters
                    ).FirstOrDefault();

                    return newUser;
                }
            }
            catch (Exception e) {
                throw e;
            }
        }

        //public static bool RegisterSessionActivity(int? userID = null, DateTime? loggedInAt = null, DateTime? loggedOffAt = null)
        //{
        //    if (EditSessionTracking(new SessionTracking()
        //    {
        //        SessionID = HttpContext.Current.Session.SessionID,
        //        IPAddress = (HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"] ?? string.Empty).Trim() == string.Empty
        //        ? HttpContext.Current.Request.Headers["REMOTE_ADDR"]?.Trim()
        //        : HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"]?.Trim(),
        //        UserId = userID.HasValue ? userID : HttpContext.Current.Session["loggeduser"] != null
        //            ? (int?)int.Parse(DataSecurityTripleDES.GetPlainText(((AuthenticatedUserInfo)HttpContext.Current.Session["loggeduser"]).UserId))
        //            : null,
        //        LoggedInAt = loggedInAt,
        //        LoggedOutAt = loggedOffAt
        //    }) != null) {
        //        return true;
        //    };
        //    return false;
        //}

        //private static SessionTracking EditSessionTracking(SessionTracking sessionTracking)
        //{
        //    SessionTracking savedSessionTracking = null;
        //    try
        //    {
        //        using (CraveatsDbContext craveatsDbContext = new CraveatsDbContext()) {
        //            DBCommonUtility dBCommonUtility = new DBCommonUtility();
        //            string sqlCmdParamString = "";
        //            SqlParameter[] sqlParameters = dBCommonUtility.GetSqlParameters(
        //                new object[] {
        //                    sessionTracking.SessionID,
        //                    sessionTracking.IPAddress,
        //                    sessionTracking.UserId,
        //                    null,
        //                    sessionTracking.LoggedInAt,
        //                    sessionTracking.LoggedOutAt },
        //                out sqlCmdParamString
        //                , true);
        //            StringBuilder sbRawSQL = new StringBuilder("exec EditSessionTracking");
        //            sbRawSQL.AppendFormat(" {0}", sqlCmdParamString.Trim());

        //            savedSessionTracking = craveatsDbContext.Database.SqlQuery<SessionTracking>(
        //                sql: sbRawSQL.ToString(), 
        //                parameters: sqlParameters
        //            ).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }

        //    return savedSessionTracking;
        //}




    }
}