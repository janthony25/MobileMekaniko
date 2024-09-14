$(document).ready(function () {
    GetCustomer();

    $('#CustomerModal').on('hidden.bs.modal', function () {
        HideModal();
    })
});

function GetCustomer() {
    $.ajax({
        url: '/customer/PopulateCustomerTable',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            let tableRows = '';

            if (response == null || response == undefined || response.length == 0) {
                tableRows = `
                    <tr>
                        <td class="text-center" colspan="6">No Customer Available</td>
                    </tr>`;
            }
            else {
                $.each(response, function (index, item) {
                    let dateAdded = new Date().toLocaleDateString();

                    tableRows += `
                        <tr>
                            <td class="text-center">
                                <a href="#" class="btn" style="color:blue" onclick="ViewCustomerModal(${item.customerId}, 'update');">${item.customerId}</a>
                            </td>
                            <td class="text-center">${item.customerName}</td>
                            <td class="text-center">${item.customerEmail}</td>
                            <td class="text-center">${item.customerNumber}</td>
                            <td class="text-center">${dateAdded}</td>
                            <div>   
                                <td class="text-center">
                                    <a href="#" class="btn btn-primary btn-sm"><i class="bi bi-car-front"></i> View</a>
                                    <a href="#" class="btn btn-danger btn-sm ms-2"  onclick="ViewCustomerModal(${item.customerId}, 'delete');"><i class="bi bi-trash3"></i></a>
                                </td>
                            </div>
                        </tr>`;
                });
            }
            $('#tblBody').html(tableRows);
        },
        error: function () {
            alert('Something went wrong.');
        }
    });
}

$('#btnAddCustomer').click(function () {
    $('#CustomerModal').modal('show');
    $('#modalTitle').text('Add Customer');
})


// Common Field validations
//function Validate() {
//    var isValid = true;

//    // Validate Customer Name
//    if ($('#CustomerName').val().trim() == "") {
//        $('#CustomerName').css('border-color', 'Red');
//        $('#CustomerNameError').text('Customer name is required.');
//        isValid = false;
//    } else {
//        $('#CustomerName').css('border-color', 'Lightgrey');
//        $('#CustomerNameError').text('');
//    }

//    // Validate Customer Email

//    let email = $('#CustomerEmail').val().trim();
//    let emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/; // Email regex pattern

//    if (email == "") {
//        $('#CustomerEmail').css('border-color', 'Red');
//        $('#CustomerEmailError').text('Customer Email is Required.');
//        isValid = false;
//    } else if (!emailPattern.test(email)) {
//        $('#CustomerEmail').css('border-color', 'Red');
//        $('#CustomerEmailError').text('Please enter a valid email address');
//        isValid = false;
//    } else {
//        $('#CustomerEmail').css('border-color', 'Lightgrey');
//        $('#CustomerEmailError').text('');
//    }

//    // Validate Customer Number
//    if ($('#CustomerNumber').val().trim() == "") {
//        $('#CustomerNumber').css('border-color', 'Red');
//        $('#CustomerNumberError').text('Contact # is required.');
//        isValid = false;
//    } else {
//        $('#CustomerNumber').css('border-color', 'Lightgrey');
//        $('#CustomerNumberError').text('');
//    }

//    return isValid;
//}


function Validate(nameFieldId, emailFieldId, numberFieldId, nameErrorId, emailErrorId, numberErrorId) {
    var isValid = true;

    // Validate Customer Name
    if ($(nameFieldId).val().trim() == "") {
        $(nameFieldId).css('border-color', 'Red');
        $(nameErrorId).text('Customer name is required.');
        isValid = false;
    } else {
        $(nameFieldId).css('border-color', 'Lightgrey');
        $(nameErrorId).text('');
    }

    // Validate Customer Email
    let email = $(emailFieldId).val().trim();
    let emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/; // Email regex pattern

    if (email == "") {
        $(emailFieldId).css('border-color', 'Red');
        $(emailErrorId).text('Customer Email is Required.');
        isValid = false;
    } else if (!emailPattern.test(email)) {
        $(emailFieldId).css('border-color', 'Red');
        $(emailErrorId).text('Please enter a valid email address');
        isValid = false;
    } else {
        $(emailFieldId).css('border-color', 'Lightgrey');
        $(emailErrorId).text('');
    }

    // Validate Customer Number
    if ($(numberFieldId).val().trim() == "") {
        $(numberFieldId).css('border-color', 'Red');
        $(numberErrorId).text('Contact # is required.');
        isValid = false;
    } else {
        $(numberFieldId).css('border-color', 'Lightgrey');
        $(numberErrorId).text('');
    }

    return isValid;
}

function HideModal() {
    ClearData();
    $('#CustomerModal').modal('hide');
}

function ClearData() {
    $('#CustomerName').val('');
    $('#CustomerEmail').val('');
    $('#CustomerNumber').val('');

    // Span validations
    $('#CustomerNameError').text('');
    $('#CustomerEmailError').text('');
    $('#CustomerNumberError').text('');

    $('#CustomerName').css('border-color', 'LightGrey');
    $('#CustomerEmail').css('border-color', 'LightGrey');
    $('#CustomerNumber').css('border-color', 'LightGrey');
}

//$('#CustomerName').change(function () {
//    Validate();
//})

//$('#CustomerEmail').change(function () {
//    Validate();
//})

//$('#CustomerNumber').change(function () {
//    Validate();
//})

$('#Close').click(function () {
    HideModal();
})

function AddCustomer() {
    let result = Validate('#CustomerName', '#CustomerEmail', '#CustomerNumber',
                          '#CustomerNameError', '#CustomerEmailError', '#CustomerNumberError');

    if (result == false) {
        return false;
    }

    let token = $('input[name="__RequestVerificationToken"]').val();

    let formData = {
        __RequestVerificationToken: token,
        customerName: $('#CustomerName').val(),
        customerEmail: $('#CustomerEmail').val(),
        customerNumber: $('#CustomerNumber').val()
    };

    $.ajax({
        url: '/customer/AddCustomer',
        data: formData,
        type: 'POST',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to add customer');
            }
            else {
                GetCustomer();
                alert('Product successfully added.');
                HideModal();
            }
        },
        error: function () {
            alert('An error occurred.');
        }
    });
}

function ViewCustomerModal(customerId, action) {
    console.log(customerId);
    $.ajax({
        url: '/customer/UpdateDeleteCustomer?id=' + customerId,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            console.log(response);
            $('#UpdateDeleteCustomerModal').modal('show');

            if (action == 'delete') {
                $('#updateDeleteModalTitle').text('Delete Customer');
                $('#Delete').css('display', 'block');
                $('#Update').css('display', 'none')

                $('#CustomerId').val(response.customerId);
                $('#UpdateDeleteCustomerName').val(response.customerName).prop('readonly', true);
                $('#UpdateDeleteCustomerEmail').val(response.customerEmail).prop('readonly', true);
                $('#UpdateDeleteCustomerNumber').val(response.customerNumber).prop('readonly', true);
                $('#DateEditedContainer').css('display', 'none');
            }
            else if (action == 'update') {
                $('#updateDeleteModalTitle').text('Update Customer');
                $('#Delete').css('display', 'none');
                $('#Update').css('display', 'block');

                $('#CustomerId').val(response.customerId);
                $('#UpdateDeleteCustomerName').val(response.customerName).prop('readonly', false);
                $('#UpdateDeleteCustomerEmail').val(response.customerEmail).prop('readonly', false);
                $('#UpdateDeleteCustomerNumber').val(response.customerNumber).prop('readonly', false);
                $('#DateEditedContainer').css('display', 'block');
                if (response.dateEdited && new Date(response.dateEdited).getTime() != 0) {
                    var formattedDate = new Date(response.dateEdited).toLocaleString();
                    $('#DateEdited').val(formattedDate);
                }
                else {
                    $('#DateEdited').val('');
                }
                console.log($('#DateEdited').val()); // Ensure it's set correctly





                console.log('Formatted Date:', new Date(response.dateEdited).toLocaleString());


            }
        },
        error: function () {
            alert('Unable to fetch product details.');
        }
    });
}

function DeleteCustomer() {
    const token = $('input[name="__RequestVerificationToken"]').val();
    const customerId = $('#CustomerId').val();

    $.ajax({
        url: '/customer/DeleteCustomer?id=' + customerId,
        type: 'POST',
        data: {
            __RequestVerificationToken: token
        },
        success: function (response) {
            if (response.success) {
                alert(response.message);
                GetCustomer();
                $('#UpdateDeleteCustomerModal').modal('hide'); // Close modal
            } else {
                alert(response.message)
            }
        },
        error: function () {
            alert('An error occurred while deleting customer.');
        }
    });
}

function UpdateCustomer() {

    let result = Validate('#UpdateDeleteCustomerName', '#UpdateDeleteCustomerEmail', '#UpdateDeleteCustomerNumber',
                          '#UpdateDeleteCustomerNameError', '#UpdateDeleteCustomerEmailError', '#UpdateDeleteCustomerNumberError');
    if (result == false) {
        return false;
    }

    const token = $('input[name="__RequestVerificationToken"]').val();
    const customerId = $('#CustomerId').val();

    let formData = {
        __RequestVerificationToken: token,
        customerId: customerId,
        customerName: $('#UpdateDeleteCustomerName').val(),
        customerEmail: $('#UpdateDeleteCustomerEmail').val(),
        customerNumber: $('#UpdateDeleteCustomerNumber').val()
    };

    $.ajax({
        url: '/customer/UpdateCustomer',
        type: 'POST',
        data: formData,
        success: function (response) {
            console.log(response);
            if (response.success) {
                alert(response.message);
                GetCustomer();
                $('#UpdateDeleteCustomerModal').modal('hide');
            }
            else {
                alert(response.message);
            }
        },
        error: function () {
            alert('An error occurred while updating customer.');
        }
    });
}