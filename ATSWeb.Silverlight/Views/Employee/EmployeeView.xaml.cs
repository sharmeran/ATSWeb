using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ATSWeb.Silverlight.Common;

namespace ATSWeb.Silverlight.Views.Employee
{
    public partial class EmployeeView : UserControl
    {
        public EmployeeView()
        {
            InitializeComponent();
        }

      
        private void txt_UserIDSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {

                var x = from z in CommonData.EmployeeList where z.UserID == Convert.ToInt32(txt_UserIDSearch.SearchText) select z;
                List<EmployeeServiceReference.Employees> list = x.ToList<EmployeeServiceReference.Employees>();
                if (list.Count > 0)
                {
                    txt_UserIDSearch.SelectedItem = x.ToList<EmployeeServiceReference.Employees>().First();
                    txt_NameSearch.SelectedItem = x.ToList<EmployeeServiceReference.Employees>().First();
                    var k = from c in CommonData.DeptList where c.Code == Convert.ToDecimal(((EmployeeServiceReference.Employees)txt_NameSearch.SelectedItem).Department.Code) select c;
                    List<DepartmentServiceReference.Department> deptList = k.ToList<DepartmentServiceReference.Department>();
                    if (deptList.Count > 0)
                    {
                        txt_DeptIDSearch.SelectedItem = k.ToList<DepartmentServiceReference.Department>().First();

                        var b = from v in CommonData.CategoryList where v.ID == (float)Convert.ToDecimal(((EmployeeServiceReference.Employees)txt_NameSearch.SelectedItem).Category.ID) select v;
                        List<CategoryServiceReference.Category> catList = b.ToList<CategoryServiceReference.Category>();
                        if (catList.Count > 0)
                        {
                            txt_Category.SelectedItem = b.ToList<CategoryServiceReference.Category>().First();
                        }
                    }
                }


            }


        }

        private void txt_NameSearch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var x = from z in CommonData.EmployeeList where z.Name.Contains(txt_NameSearch.SearchText) select z;
                List<EmployeeServiceReference.Employees> list = x.ToList<EmployeeServiceReference.Employees>();
                if (list.Count > 0)
                {
                    txt_NameSearch.SelectedItem = x.ToList<EmployeeServiceReference.Employees>().First();
                    txt_UserIDSearch.SelectedItem = x.ToList<EmployeeServiceReference.Employees>().First();
                    var k = from c in CommonData.DeptList where c.Code == Convert.ToDecimal(((EmployeeServiceReference.Employees)txt_NameSearch.SelectedItem).Department.Code) select c;
                    List<DepartmentServiceReference.Department> deptList = k.ToList<DepartmentServiceReference.Department>();
                    if (deptList.Count > 0)
                    {
                        txt_DeptIDSearch.SelectedItem = k.ToList<DepartmentServiceReference.Department>().First();

                        var b = from v in CommonData.CategoryList where v.ID == (float)Convert.ToDecimal(((EmployeeServiceReference.Employees)txt_NameSearch.SelectedItem).Category.ID) select v;
                        List<CategoryServiceReference.Category> catList = b.ToList<CategoryServiceReference.Category>();
                        if (catList.Count > 0)
                        {
                            txt_Category.SelectedItem = b.ToList<CategoryServiceReference.Category>().First();
                        }
                    }

                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txt_UserIDSearch.Focus();
            txt_NameSearch.ItemsSource = CommonData.EmployeeList;
            txt_UserIDSearch.ItemsSource = CommonData.EmployeeList;
            txt_DeptIDSearch.ItemsSource = CommonData.DeptList;
            txt_Category.ItemsSource = CommonData.CategoryList;

            this.UpdateLayout();
        }

        private void txt_NameSearch_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void txt_UserIDSearch_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {


        }
    }
}
