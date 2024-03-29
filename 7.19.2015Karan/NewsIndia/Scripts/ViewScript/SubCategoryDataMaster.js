var categorytable;

$(document).ready(function () {
    DataTableLoad();
});



function DataTableLoad() {

    $('#tblSubCategoryDataGrid').dataTable({
        "bLengthChange": false, 'iDisplayLength': 7
    });
    $("#tblSubCategoryDataGrid_filter").hide();

    oTable = $('#tblSubCategoryDataGrid').DataTable();

    $('#txtSearchSubCategoryData').on('keyup', function () {
        oTable.search(this.value).draw();
    });

}
var IsNewEnrty;
var SubCategoryId;

$(document).ready(function () {
    $("#btnAddNewSubCategoryData").click(function() {
        window.location = "/SubCategoryDataManager/DisplaySubCategoryData";
    });


    $("#txtSubCategoryName").popover({ trigger: 'manual' });
    $("#ddlCategory").popover({ trigger: 'manual' });

    $("#txtSubCategoryName").change(function () {
        $("#txtSubCategoryName").popover('hide');
    });

    

    //Used  on the change of the Category to load SubCategory
    $("#ddlCategory").change(function () {
        $("#divLoading").show();
        $.ajax({
            url: '/SubCategoryDataManager/GetSubCategories',
            type: "POST",
            data: { categoryId: $("#ddlCategory").val() },
            success: function (data) {
                debugger;
                document.getElementById("ddlSubCategory").innerHTML = "";
                var ddldata = "<option value=" + 0 + ">SubCategory Name</option>";
                $(ddldata).appendTo('#ddlSubCategory');
                $.each(data, function (i, obj) {

                    ddldata = "<option value=" + obj.SubCategoryId + ">" + obj.SubCategoryName + "</option>";
                    $(ddldata).appendTo('#ddlSubCategory');
                });
                $("#divLoading").hide();

            },
            error: function (data) {
                showNotification("Error while Communicating with server.", "warning");
                $("#divLoading").hide();
            }
        });

        $("#ddlCategory").popover('hide');
    });

    $("#btnSaveChanges").click(function () {
        //  $("#txtSubCategoryDataName").attr("data-content", "Looks like this Subcategory is already Present!!!");
        if ($("#txtSubCategoryDataName").val() == "") {
            $("#txtSubCategoryDataName").focus();
            $("#txtSubCategoryDataName").attr("data-content", "Please Enter Title!!!");
            $("#txtSubCategoryDataName").popover('show');
        } else {
            if ($("#ddlCategory").val() == "0") {
                $("#ddlCategory").focus();
                $("#ddlCategory").popover('show');
            }
            else if ($("#ddlSubCategory").val() == "0") {
                $("#ddlSubCategory").focus();
                $("#ddlSubCategory").popover('show');
            }
            else {
                CheckSubCategoryName();
            }
        }
    });

});

function RemoveSubCategoryData(subCategoryDataId) {
    if (confirm('Are you sure you want to delete this Sub-Category Data.')) {
        $.ajax({
            url: '/SubCategoryDataManager/RemoveSubCategoryData',
            type: "POST",
            data: { subCategoryDataID: subCategoryDataId },
            success: function (data) {

                if (data.IsSubCategoryRemoved) {
                    showNotification("Sub-Category has been Removed.", "success");
                    $("#divLoading").show();
                    $("#divSubCategoryDataTable").load("/SubCategoryDataManager/ShowSubCategoryDataTable", function () {
                        $("#divLoading").hide();
                        DataTableLoad();
                        ReloadMenuDashboard();
                    });

                } else {
                    showNotification("Error while processing your request.", "warning");
                }

            },
            error: function (data) {
                showNotification("Error while Communicating with server.", "warning");
            }
        });
    }

}

function SaveSubCategoryData() {
    $.ajax({
        url: '/SubCategoryManager/SaveSubCategoryInfo',
        type: "POST",
        data: { subCategoryId: SubCategoryId, subCategoryName: $("#txtSubCategoryName").val(), isVisible: $('#chkIsVisible').prop('checked'), categoryId: $("#ddlCategory").val() },
        success: function (data) {
            debugger;
            var subCategorySaved = data.SubCategorySaved;
            if (subCategorySaved) {
                showNotification("Sub-Category has been saved.", "success");
                $('#popup').modal('toggle');
                $("#divLoading").show();
                $("#divSubCategoryTable").load("/SubCategoryManager/ShowSubCategoryTable", function () {
                    $("#divLoading").hide();
                    DataTableLoad();
                    ReloadMenuDashboard();
                });

            } else {
                showNotification("Error while processing your request.", "warning");
            }

        },
        error: function (data) {
            showNotification("Error while Communicating with server.", "warning");
        }
    });
}

function CheckSubCategoryName() {
    $.ajax({
        url: '/SubCategoryManager/CheckSubCategoryName',
        type: "POST",
        data: { subCategoryName: $("#txtSubCategoryName").val(), subCategoryId: SubCategoryId, categoryId: $("#ddlCategory").val() },
        success: function (data) {
            debugger;
            var isSubCategoryPresent = data.IsSubCategoryPresent;
            if (isSubCategoryPresent) {

                $("#txtSubCategoryName").popover('show');
            } else {
                SaveSubCategory();
            }

        },
        error: function (data) {
            showNotification("Error while Communicating with server.", "warning");
        }
    });
}


function loadSubCategoryInfo() {
    $.ajax({
        url: '/SubCategoryManager/GetSubCategoryInfo',
        type: "POST",
        data: { subCategoryId: SubCategoryId },
        success: function (data) {
            var subCategoryInfo = data.SubCategoryInformation;
            if (subCategoryInfo.SubCategoryName != null) {
                $("#txtSubCategoryName").val(subCategoryInfo.SubCategoryName);
                $('#chkIsVisible').prop('checked', subCategoryInfo.IsVisible);
                SetCategoryDropDown(subCategoryInfo.CategoryId);
            } else {
                showNotification("Error while processing your request.", "warning");
            }

        },
        error: function (data) {

            showNotification("Error while Communicating with server.", "warning");
            $('#popup').modal('toggle');
        }
    });

}


$('#popup').on('show.bs.modal', function (event) {
    var modal = $(this);
    ResetPopUp(modal);

    var eventObject = $(event.relatedTarget);
    var subCategoryId = eventObject.data('subcategoryid');

    SubCategoryId = subCategoryId;

    if (SubCategoryId == 0)
        NewEntryPopUp(modal);
    else
        EditEntryPopUp(modal);
});
//model Close event
$('#popup').on('hidden.bs.modal', function () {
    $("#txtSubCategoryName").popover('hide');
    $("#ddlCategory").popover('hide');

});

function EditEntryPopUp(modal) {
    modal.find('.modal-title').text("Edit Sub-Category Data");
    loadSubCategoryInfo();
}

function NewEntryPopUp(modal) {
    modal.find('.modal-title').text("Add Sub-Category Data");

}
function SetCategoryDropDown(value) {
    $("#ddlCategory").val(value).attr("selected", "selected");
}

$("#inputFiles").change(function () {

    var inp = document.getElementById('inputFiles');
    var widthInPercentage = "46% !Important";
    $("#modalMain").removeClass("singleFiles threeFiles fourFiles mutipleFiles");
    if (inp.files.length > 1)
        switch (inp.files.length) {
            case 2:
                $("#modalMain").addClass("twoFiles");
            case 3:
                $("#modalMain").addClass("threeFiles");
                break;
            case 4:
                $("#modalMain").addClass("fourFiles");
                break;
            default:
                $("#modalMain").addClass("mutipleFiles");
                break;
        }
    else {
        $("#modalMain").addClass("singleFiles");
    }

    // $("#modalMain").css({ "width": widthInPercentage });
});

$(".fileinput-remove").click(function () {
    debugger;
    $("#modalMain").removeClass("singleFiles threeFiles fourFiles mutipleFiles");
    $("#modalMain").addClass("singleFiles");
});

function ResetPopUp(modal) {
    modal.find('.modal-title').text('');
    $("#txtSubCategoryName").val("");
    SetCategoryDropDown(0);
    $('#chkIsVisible').prop('checked', false);

}

$(function() {
    $('#fileupload').fileupload({
        autoUpload: true,
        url: '/Question/SaveUpload/',
        filesContainer: '.Table1',
        disableImageResize: /Android(?!.*Chrome)|Opera/
            .test(window.navigator.userAgent),
        //acceptFileTypes: /(\.|\/)(gif|jpe?g|png|doc|docx|xls?x|bmp|ppt|pptx|pdf)$/i,
        acceptFileTypes: /(\.|\/)(gif|jpeg|jpg|png|doc|docx|xlsx|xls|bmp|ppt|pptx|pdf|txt|pdf)$/i,
        maxRetries: 100,
        retryTimeout: 500,
    });

    function SetFileName(id) {
        debugger;
        var fup = document.getElementById(id.id);
        var fileName = fup.value;
        var selectedFileName = fileName.substring(fileName.lastIndexOf('\\') + 1);
        if ($('#UploadedFileNames').val() == '') {
            $('#UploadedFileNames').attr('value', selectedFileName);
        } else {
            var previousValue = $('#UploadedFileNames').val() + "," + selectedFileName;
            $('#UploadedFileNames').attr('value', previousValue);
        }
    }

});