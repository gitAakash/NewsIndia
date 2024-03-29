var categorytable;
$(document).ready(function () {

    DataTableLoad();

});
function DataTableLoad() {
    $('#tblUserGrid').dataTable({
        "bLengthChange": false, 'iDisplayLength': 7
    });
    $("#tblUserGrid_filter").hide();

    oTable = $('#tblUserGrid').DataTable();

    $('#txtSearchUser').on('keyup', function () {
        oTable.search(this.value).draw();
    });
}
var IsNewEnrty;
var CategoryId;
$(document).ready(function () {
    //$("#txtCategoryName").popover({ trigger: 'manual' });

    //$("#txtCategoryName").change(function () {
    //    $("#txtCategoryName").popover('hide');
    //});
    //$("#btnSaveChanges").click(function () {
    //    debugger;
    //    $("#txtCategoryName").attr("data-content", "Looks like this  category is already Present!!!");
    //    if ($("#txtCategoryName").val() == "") {
    //        $("#txtCategoryName").attr("data-content", "Please Enter Category!!!");
    //        $("#txtCategoryName").popover('show');
    //    } else {
    //        CheckCategoryName();
    //    }
    //});

});

function RemoveUser(userId) {
    if (confirm('Are you sure you want to delete this User.')) {
        $.ajax({
            url: '/UserManager/RemoveUser',
            type: "POST",
            data: { userId: userId },
            success: function (data) {

                if (data) {
                    showNotification("User has been Removed.", "success");
                    $("#divLoading").show();
                    $("#divUserTable").load("/UserManager/ShowUserTable", function () {
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

function SaveCategory() {
    $.ajax({
        url: '/CategoryManager/SaveCategoryInfo',
        type: "POST",
        data: { categoryId: CategoryId, categoryName: $("#txtCategoryName").val(), isVisible: $('#chkIsVisible').prop('checked') },
        success: function (data) {
            debugger;
            var categorySaved = data.CategorySaved;
            if (categorySaved) {
                showNotification("Category has been saved.", "success");
                $('#popup').modal('toggle');
                $("#divLoading").show();
                $("#divCategoryTable").load("/CategoryManager/ShowCategoryTable", function () {
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

function CheckCategoryName() {
    $.ajax({
        url: '/CategoryManager/CheckCategoryName',
        type: "POST",
        data: { categoryName: $("#txtCategoryName").val(), categoryId: CategoryId },
        success: function (data) {
            var isCategoryPresent = data.IsCategoryPresent;
            if (isCategoryPresent) {
                //$("#txtCategoryName").popover({ trigger: 'manual' });
                $("#txtCategoryName").popover('show');
            } else {
                SaveCategory();
            }

        },
        error: function (data) {
            showNotification("Error while Communicating with server.", "warning");
        }
    });
}


function loadCategoryInfo() {
    $.ajax({
        url: '/CategoryManager/GetCategoryInfo',
        type: "POST",
        data: { categoryId: CategoryId },
        success: function (data) {
            debugger;
            var categoryInfo = data.CategoryInformation;
            if (categoryInfo.CategoryName != null) {
                $("#txtCategoryName").val(categoryInfo.CategoryName);
                $('#chkIsVisible').prop('checked', categoryInfo.IsVisible);
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
    var categoryId = eventObject.data('categoryid');
    IsNewEnrty = categoryId == 0;
    CategoryId = categoryId;

    if (categoryId == 0)
        NewEntryPopUp(modal);
    else
        EditEntryPopUp(modal);
});
//model Close event
$('#popup').on('hidden.bs.modal', function () {
    $("#txtCategoryName").popover('hide');
});

function EditEntryPopUp(modal) {
    modal.find('.modal-title').text("Edit Category");
    loadCategoryInfo();
}

function NewEntryPopUp(modal) {
    modal.find('.modal-title').text("Add Category");

}

function ResetPopUp(modal) {
    modal.find('.modal-title').text('');
    $("#txtCategoryName").val("");
    $('#chkIsVisible').prop('checked', false);

}
