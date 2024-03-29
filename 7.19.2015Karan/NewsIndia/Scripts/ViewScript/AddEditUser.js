var categorytable;

function goBack() {
    window.history.back();
}

$(document).ready(function () {

    $("#ddlCountry").change(function () {
        if ($("#ddlCountry").val() != "") {
            $("#divLoading").show();

            $.ajax({
                url: '/UserManager/GetStatesForCountry',
                type: "POST",
                data: { countryId: $("#ddlCountry").val() },
                success: function (data) {
                    debugger;
                    /*
                <select id="ddlState" class="form-control" data-val="true" data-val-number="The field State must be a number." data-val-required="Please select State" name="State" style="width: 100%;"><option value="">Select State</option>
</select>*/

                    document.getElementById("ddlState").innerHTML = "";
                    var ddldata = "<option value=''>Select State</option>";
                    $(ddldata).appendTo('#ddlState');
                    $.each(data, function (i, obj) {

                        ddldata = "<option value=" + obj.StateId + ">" + obj.StateName + "</option>";
                        $(ddldata).appendTo('#ddlState');
                    });

                    $("#divLoading").hide();
                },
                error: function (data) {

                    showNotification("Error while Communicating with server.", "warning");
                    $("#divLoading").hide();
                }
            });
        }
    });
});

