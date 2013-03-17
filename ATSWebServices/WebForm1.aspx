<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ATSWebServices.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.9.1.custom.js"></script>
    <link href="Styles/jquery-ui-1.10.1.custom.css" rel="stylesheet" />
    <link href="Styles/CustomStyle.css" rel="stylesheet" />
    <link href="Styles/demos.css" rel="stylesheet" />
    <link href="Styles/jquery.jscrollpane.css" rel="stylesheet" />
    <script src="Scripts/jquery.mousewheel.js"></script>
    <script src="Scripts/jquery.jscrollpane.js"></script>
    
    <script>
        <% var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
        var categoryArray = new Array();
        $(function () {
            $("#div_dialog").dialog({
                autoOpen: false,
                width: 400,
                buttons: [
                    {
                        text: "إغلاق",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
            });
            $("#btn_Click").click(function () {
                $("#div_dialog").dialog("open");
            });

            $("#btn_Save").button();
            $("#btn_Clear").button();

            $("#btn_Save").click(function () {
                var name = $("#txt_Name").val();
                var description = $("#txt_Description").val();

                var isName = false;
                var isDescription = false;

                if (name == "") {
                    $("#txt_err_name").text("حقل اجباري");
                    isName = false;
                }
                else {
                    $("#txt_err_name").text("");
                    isName = true;
                }

                if (description == "") {
                    $("#txt_err_description").text("حقل اجباري");
                    isDescription = false;
                }
                else {
                    $("#txt_err_description").text("");
                    isDescription = true;
                }

                if (isName == true && isDescription == true) {
                    $("#btn_Save").attr("disabled", true);
                    $("#btn_Clear").attr("disabled", true);
                    $.ajax({
                        url: "Services/Devices/DeviceCategoryWebService.asmx/Add",
                        type: "POST",
                        data: "{name:'" + name + "', description:'" + description + "'}",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (msg) {

                            if (msg.d.Message == null) {
                                $("#txt_err_generalError").css("color", "green");
                                $("#txt_err_generalError").text("تمت الاضافة بنجاح");
                                $("#txt_Description").val("");
                                $("#txt_Name").val("");
                                $("#btn_Save").removeAttr("disabled");
                                $("#btn_Clear").removeAttr("disabled");
                            }
                            else {
                                $("#txt_err_generalError").css("color", "red");
                                $("#txt_err_generalError").text(msg.d.Message);
                                $("#btn_Save").removeAttr("disabled");
                                $("#btn_Clear").removeAttr("disabled");
                            }

                        },
                        error: function (e) {
                            $("#txt_err_generalError").css("color", "red");
                            $("#txt_err_generalError").text("حدث خطأ الؤجاء التأكد من مسؤول النظام");
                            $("#btn_Save").removeAttr("disabled");
                            $("#btn_Clear").removeAttr("disabled");
                        }
                    });
                }


            });

            $("#btn_Clear").click(function(){
                $("#txt_Description").val("");
                $("#txt_Name").val("");
            });

            function GetAllCategories() {
                $.ajax({
                    url: "Services/Devices/DeviceWebService.asmx/FindAll",
                    type: "POST",
                    data: "{}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        categoryArray = msg.d;
                        alert(categoryArray.length);
                    },
                    error: function (e) {
                        alert(e.error);
                    }
                });
            }

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" value="Test" id="btn_Click" style="width: 200px; height: 20px;" />
            <asp:DropDownList ID="DropDownList1" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
            <div id="div_dialog" title="إضافة وصف جهاز" class="div_dialog">
                <div class="control_Splitter">
                    <label class="dialog_Lables">الاسم : </label>
                    <input type="text" id="txt_Name" class="dialog_TextBox" />
                    <label id="txt_err_name" class="dialog_Errors_Lable"></label>
                </div>
                <div class="control_Splitter">
                    <label class="dialog_Lables">الوصف : </label>
                    <input type="text" id="txt_Description" class="dialog_TextBox" />
                    <label id="txt_err_description" class="dialog_Errors_Lable"></label>
                </div>
                <div class="control_Splitter">
                    <label id="txt_err_generalError" class="dialog_General_Errors_Lable"></label>
                </div>
                <div class="control_Splitter_Buttons">
                    <button  id="btn_Save">حفظ</button>
                    <button  id="btn_Clear" >مسح</button>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
