using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ATSWeb.Silverlight.EmployeeServiceReference;


namespace ATSWeb.Silverlight.Common
{
    public static class CommonData
    {

        
        private static List<Employees> employeeList;
        private static List<DepartmentServiceReference.Department> deptList;
        private static List<CategoryServiceReference.Category> categoryList;

        public static List<CategoryServiceReference.Category> CategoryList
        {
            get { return CommonData.categoryList; }
            set { CommonData.categoryList = value; }
        }

        public static List<DepartmentServiceReference.Department> DeptList
        {
            get { return CommonData.deptList; }
            set { CommonData.deptList = value; }
        }

        public static List<Employees> EmployeeList
        {
            get { return CommonData.employeeList; }
            set { CommonData.employeeList = value; }
        }
    }
}
