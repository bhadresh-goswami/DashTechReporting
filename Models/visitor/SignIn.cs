using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Models.visitor
{
    public class SignIn
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public object _SignIn()
        {
            try
            {
                dashReportingEntities db = new dashReportingEntities();
                UserAccountDetail user = db.UserAccountDetails.SingleOrDefault(asd => asd.EmailId == UserName && asd.Password == Password);
                if (user != null)
                {
                    return new sessionModel()
                    {
                        UserId = user.UserId,
                        defaultUrl = GetDefaultURL(user.RoleMaster.RoleTitle),
                        UserRocketName = user.RocketName,
                        UserRole = user.RoleMaster.RoleTitle
                    };
                }
                else
                {
                    return "Invalid Username or Password!!!";
                }
            }
            catch (Exception err)
            {
                //return "Got Some Error : " + err.Message;
                return "Invalid Username or Password!!!";
            }
        }

        public static string GetDefaultURL(string URL)
        {
            if (URL == "admin")
            {
                return "../admin/dashboard";
            }
            else if (URL == "Marketing Manager")
            {
                return "../marketing/dashboard";
            }
            else if (URL == "Marketing Team Lead")
            {
                return "../marketing/teamlead";
            }
            else if (URL == "Recruiter")
            {
                return "../marketing/recruiter";
            }
            else if (URL == "Sales Associate")
            {
                return "../sales/dashboard";
            }
            else if (URL == "Sales Manager")
            {
                return "../sales/dashboard";
            }
            else if (URL == "Sales Team Lead")
            {
                return "../sales/dashboard";
            }
            else if (URL == "Lead Generator")
            {
                return "../leadgenerate/dashboard";
            }

            else if (URL == "Technical Team Manager")
            {
                return "../technical/manager";
            }
            else if (URL == "Technical Team Lead")
            {
                return "../technical/teamlead";
            }
            else if (URL == "Technical Expert")
            {
                return "../technical/expert";
            }

            else
            {
                return "";
            }
        }
    }
}