<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeviceCategoryView.aspx.cs" Inherits="ATSWebServices.Views.Devices.DeviceCategoryView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Scripts/jquery-1.8.2.js"></script>
    <script src="../../Scripts/jquery-ui-1.9.1.custom.js"></script>
    <link href="../../Styles/jquery-ui-1.10.1.custom.css" rel="stylesheet" />
    <link href="../../Styles/CustomStyle.css" rel="stylesheet" />
    <link href="../../Styles/demos.css" rel="stylesheet" />
    <link href="../../Styles/jquery.jscrollpane.css" rel="stylesheet" />
    <script src="../../Scripts/jquery.mousewheel.js"></script>
    <script src="../../Scripts/jquery.jscrollpane.js"></script>
    <link href="../../Styles/Style.css" rel="stylesheet" />

    <script>
        <% var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
        var categoryArray = new Array();
        $(function () {

            $("#divOverlay").show();
            GetAllCategories();
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

            $("#div_UpdateDialog").dialog({
                autoOpen: false,
                width: 400,
                buttons: [

                    {
                        text: "الغاء",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }

                ]
            });

            $("#div_DeleteDialog").dialog({
                autoOpen: false,
                width: 400,
                buttons: [
                     {
                         text: "موافق",
                         click: function () {
                             DeleteCategory();
                             $(this).dialog("close");
                         }
                     },
                    {
                        text: "الغاء",
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
                    $("#divOverlay").show();
                    $.ajax({
                        url: "../../Services/Devices/DeviceCategoryWebService.asmx/Add",
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
                                $("#divOverlay").hide();
                                $(".rich-table-subheader").nextAll().remove();
                                GetAllCategories();

                            }
                            else {
                                $("#txt_err_generalError").css("color", "red");
                                $("#txt_err_generalError").text(msg.d.Message);
                                $("#divOverlay").hide();

                            }

                        },
                        error: function (e) {
                            $("#txt_err_generalError").css("color", "red");
                            $("#txt_err_generalError").text("حدث خطأ الؤجاء التأكد من مسؤول النظام");
                            $("#divOverlay").hide();
                        }
                    });
                }
            });

            $("#btn_Clear").click(function () {
                $("#txt_Description").val("");
                $("#txt_Name").val("");
            });

            function GetAllCategories() {
                $.ajax({
                    url: "../../Services/Devices/DeviceCategoryWebService.asmx/FindAll",
                    type: "POST",
                    data: "{}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        categoryArray = msg.d;
                        AddTableData();
                        $("#divOverlay").hide();

                    },
                    error: function (e) {
                        $("#txt_err_generalError").css("color", "red");
                        $("#txt_err_generalError").text("حدث خطأ الؤجاء التأكد من مسؤول النظام");
                        $("#divOverlay").hide();
                    }
                });
            }

            function AddTableData() {
                var raw = '';
                for (i = 0; i < categoryArray.ReturnedEntities.length; i++) {
                    if (i % 2 != 0) {
                        raw += "<tr class='rowEven'>";
                        raw += "<td>" + categoryArray.ReturnedEntities[i].Name + "</td>";
                        raw += "<td>" + categoryArray.ReturnedEntities[i].Description + "</td>";
                        raw += "<td><input type='button' id='" + categoryArray.ReturnedEntities[i].ID + "' class='barDeleteButton' title='حذف'/></td>";
                        raw += "<td><input type='button' id='" + categoryArray.ReturnedEntities[i].Name + "-" + categoryArray.ReturnedEntities[i].Description + "-" + categoryArray.ReturnedEntities[i].ID + "' class='barUpdateButton' title='تعديل'/></td>";
                        raw += "</tr>";
                    }
                    else {
                        raw += "<tr class='rowOdd'>";
                        raw += "<td>" + categoryArray.ReturnedEntities[i].Name + "</td>";
                        raw += "<td>" + categoryArray.ReturnedEntities[i].Description + "</td>";
                        raw += "<td><input type='button' id='" + categoryArray.ReturnedEntities[i].ID + "' class='barDeleteButton' title='حذف'/></td>";
                        raw += "<td><input type='button' id='" + categoryArray.ReturnedEntities[i].Name + "-" + categoryArray.ReturnedEntities[i].Description + "-" + categoryArray.ReturnedEntities[i].ID + "' class='barUpdateButton' title='تعديل'/></td>";
                        raw += "</tr>";
                    }
                }
                $(".rich-table-subheader").after(raw);
                $('#tableDiv').jScrollPane();
                applyButtonsHandler();

            }

            function applyButtonsHandler() {
                $('.barDeleteButton').click(function () {
                    var test= $(this).attr("id");
                    $("#delete_ID").val(test);                    
                    $("#div_DeleteDialog").dialog('open');
                });


                $('.barUpdateButton').click(function () {

                    var str = $(this).attr("id");
                    str = str.split('-');
                    $("#txt_Update_Name").val(str[0]);
                    $("#txt_Update_Desc").val(str[1]);
                    $("#id_hidden").val(str[2]);
                    $("#div_UpdateDialog").dialog("open");

                });
            }
            $('#btn_Save_Update').click(function () {
                var id = $("#id_hidden").val();
                var name = $("#txt_Update_Name").val();
                var description = $("#txt_Update_Desc").val();

                var isName = false;
                var isDescription = false;

                if (name == "") {
                    $("#txt_Err_Update_Name").text("حقل اجباري");
                    isName = false;
                }
                else {
                    $("#txt_Err_Update_Name").text("");
                    isName = true;
                }

                if (description == "") {
                    $("#txt_Err_Update_Desc").text("حقل اجباري");
                    isDescription = false;
                }
                else {
                    $("#txt_Err_Update_Desc").text("");
                    isDescription = true;
                }

                if (isName == true && isDescription == true) {
                    UpdateCategory(name, description, id);


                }

            });

            function UpdateCategory(name, description, id) {
                $("#divOverlay").show();
                $.ajax({
                    url: "../../Services/Devices/DeviceCategoryWebService.asmx/Update",
                    type: "POST",
                    data: "{name:'" + name + "', description:'" + description + "', id:" + id + "}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {

                        if (msg.d.Message == null) {
                            $("#txt_err_generalError").css("color", "green");
                            $("#txt_err_generalError").text("تم التعديل بنجاح");
                            $("#divOverlay").hide();
                            $("#div_UpdateDialog").dialog("close");
                            $(".rich-table-subheader").nextAll().remove();
                            GetAllCategories();

                        }
                        else {
                            $("#txt_err_generalError").css("color", "red");
                            $("#txt_err_generalError").text(msg.d.Message);
                            $("#divOverlay").hide();

                        }

                    },
                    error: function (e) {
                        $("#txt_err_generalError").css("color", "red");
                        $("#txt_err_generalError").text("حدث خطأ الؤجاء التأكد من مسؤول النظام");
                        $("#divOverlay").hide();
                    }
                });
            }



            function DeleteCategory() {
                var id = $("#delete_ID").val();
                $("#divOverlay").show();
                $.ajax({
                    url: "../../Services/Devices/DeviceCategoryWebService.asmx/Delete",
                    type: "POST",
                    data: "{id:" + id + "}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {

                        if (msg.d.Message == null) {                           
                            $("#divOverlay").hide();
                            $("#div_UpdateDialog").dialog("close");
                            $(".rich-table-subheader").nextAll().remove();
                            GetAllCategories();

                        }
                       
                        $("#divOverlay").hide();
                       

                    },
                    error: function (e) {
                        
                        $("#divOverlay").hide();
                    }
                });
            }



        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%-- mainDiv --%>
            <div>
                <%-- ControlsDiv --%>
                <div style="width: 800px; height: 200px;">

                    <input type="button" id="btn_Click" class="barAddButton" title="اضافة تصنيف جديد" />
                    <%-- EndControlsDiv --%>

                    <%-- GridDiv --%>
                    <div id="tableDiv" style="margin-top: 30px; height: 200px;">
                        <table border="1" cellspacing="0" cellpadding="0" class="grid">
                            <tr class="rich-table-subheader">
                                <th style="width: 300px;">الاسم</th>
                                <th style="width: 300px;">الرقم</th>
                                <th style="width: 25px;">حذف</th>
                                <th style="width: 25px;">تعديل</th>
                            </tr>
                        </table>
                    </div>
                    <%-- EndGridDiv --%>
                </div>
            </div>
            <%-- EndmainDiv --%>
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
                    <button id="btn_Save">حفظ</button>
                    <button id="btn_Clear">مسح</button>
                </div>
            </div>

            <div id="div_UpdateDialog" title="تعديل وصف جهاز" class="div_dialog">
                <div class="control_Splitter">
                    <label class="dialog_Lables">الاسم : </label>
                    <input type="text" id="txt_Update_Name" class="dialog_TextBox" />
                    <label id="txt_Err_Update_Name" class="dialog_Errors_Lable"></label>
                </div>
                <div class="control_Splitter">
                    <label class="dialog_Lables">الوصف : </label>
                    <input type="text" id="txt_Update_Desc" class="dialog_TextBox" />
                    <label id="txt_Err_Update_Desc" class="dialog_Errors_Lable"></label>
                </div>

                <div class="control_Splitter">
                    <label id="Label3" class="dialog_General_Errors_Lable"></label>
                </div>
                <div class="control_Splitter">
                    <input type="hidden" id="id_hidden" />
                </div>
                <div class="control_Splitter_Buttons">
                    <button id="btn_Save_Update">حفظ</button>

                </div>
            </div>
            <div id="div_DeleteDialog" title="حذف وصف جهاز" class="div_dialog">
                <div class="control_Splitter">
                    <img src="../../Styles/images/Update.png" />
                    <label class="dialog_Lables">هل انت متأكد من الحذف ؟ </label>
                    <input type="hidden" id="delete_ID" />

                </div>

            </div>

            <div id="divOverlay">

                <div class="ui-overlay">
                    <div class="ui-widget-overlay">
                    </div>
                </div>
                <div style="position: absolute; width: 220px; height: 25px; left: 40%; top: 40%; padding: 20px;" class="ui-widget ui-widget-content ui-corner-all">
                    <div class="ui-dialog-content ui-widget-content" style="background: none; border: 0;">
                        <img src="../../Styles/images/progress-indicator.gif" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
