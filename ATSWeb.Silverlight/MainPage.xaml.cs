using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ATSWeb.Silverlight.EmployeeServiceReference;

namespace ATSWeb.Silverlight
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
        EmployeesWebServiceSoapClient employeesWebServiceSoapClient = new EmployeesWebServiceSoapClient();
        DepartmentServiceReference.DepartmentWebServiceSoapClient departmentWebServiceSoapClient = new DepartmentServiceReference.DepartmentWebServiceSoapClient();
        CategoryServiceReference.CategoryWebServiceSoapClient categoryWebServiceSoapClient = new CategoryServiceReference.CategoryWebServiceSoapClient();

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            employeesWebServiceSoapClient.FindAllCompleted += employeesWebServiceSoapClient_FindAllCompleted;
            employeesWebServiceSoapClient.FindAllAsync();
            departmentWebServiceSoapClient.FindAllCompleted += departmentWebServiceSoapClient_FindAllCompleted;
            departmentWebServiceSoapClient.FindAllAsync();
            categoryWebServiceSoapClient.FindAllCompleted += categoryWebServiceSoapClient_FindAllCompleted;
            categoryWebServiceSoapClient.FindAllAsync();
            indicator.IsBusy = true;
        }

        void categoryWebServiceSoapClient_FindAllCompleted(object sender, CategoryServiceReference.FindAllCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                if (e.Result.Message == string.Empty)
                {
                    CommonData.CategoryList = e.Result.ReturnedEntities.ToList<CategoryServiceReference.Category>();
                    indicator.IsBusy = false;
                }
                else
                {
                }
            }
            else
            {
            }
        }

        void departmentWebServiceSoapClient_FindAllCompleted(object sender, DepartmentServiceReference.FindAllCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                if (e.Result.Message == string.Empty)
                {
                    CommonData.DeptList = e.Result.ReturnedEntities.ToList<DepartmentServiceReference.Department>();
                    //indicator.IsBusy = false;
                }
                else
                {
                }
            }
            else
            {
            }
        }

        void employeesWebServiceSoapClient_FindAllCompleted(object sender, FindAllCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                if (e.Result.Message == string.Empty)
                {
                    CommonData.EmployeeList = e.Result.ReturnedEntities.ToList<Employees>();
                    indicator.IsBusy = false;
                }
                else
                {
                }
            }
            else
            {
            }
        }

        // After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
        }

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }


    }
}