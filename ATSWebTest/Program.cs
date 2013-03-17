using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Entites.Shifts;
using ATSDataAccess.SQLImlementation.Shifts;

namespace ATSWebTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Shift shift = new Shift();
            //shift.ID = 41;
            //shift.DayID = 1;
            //shift.DeptCode = "11111111111111111111";
            //shift.Description = "test";
            //shift.InAllowTime = 10;
            //shift.InTime = DateTime.Now;
            //shift.IsFormalVacations = true;
            //shift.IsNormalShift = true;
            //shift.MaxAllowWorkTime = 20;
            //shift.MinAllowWorkTime = 20;
            //shift.Name = "test";
            //shift.OutAllowTime = 10;
            //shift.OutTime = DateTime.Now;

            ShiftRepository shiftRepository = new ShiftRepository();
            List<Shift> shiftList = new List<Shift>();
            shiftList = shiftRepository.FindDefaultByUserID(990298, new ATSCommon.ActionState());
        }
    }
}
