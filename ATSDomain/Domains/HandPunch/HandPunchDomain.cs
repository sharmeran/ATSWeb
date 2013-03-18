using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Entites.Shifts;
using ATSDataAccess.SQLImlementation.Shifts;

namespace ATSDomain.Domains.HandPunch
{
   public class HandPunchDomain 
    {
       public void SaveNewAction(int userID, int loggingType, DateTime loggingDate, int deviceID )
       {
           List<Shift> userShift = new List<Shift>();
           ShiftRepository shiftRepository = new ShiftRepository();
           ActionState actionState= new ActionState();
           userShift = shiftRepository.FindByUserIDAndDate(userID, loggingDate, actionState);
           
       }


       public int GetShiftIDFromManyShifts(List<Shift> userShift, DateTime loggingDate)
       {
           return 1;
       }
    }
}
