﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Employee;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Employee
{
   public static class TitleRepositoryConstants 
    {
       public const string ID = CommonConstants.Post_SP+ TitleConstants.ID;
       public const string Name = CommonConstants.Post_SP + TitleConstants.Name;

       public const string FindAll = "TITLES_FIND_ALL";
       public const string FindByID = "TITLES_FIND_BY_ID";
    }
}
