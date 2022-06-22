
var buttonEmployeesSelect = document.getElementById("btn-employees-select");
var buttonEmployeesClear = document.getElementById("btn-employees-clear");
var buttonEmployeesShowInsertForm = document.getElementById("btn-employees-show-insert-form");

var buttonEmployeeInsertSave = document.getElementById("btn-employee-insert-save");
var buttonEmployeeInsertCancel = document.getElementById("btn-employee-insert-cancel");
var buttonEmployeeUpdateSave = document.getElementById("btn-employee-update-save");
var buttonEmployeeUpdateCancel = document.getElementById("btn-employee-update-cancel");
var dynamicEmployeeRows = document.getElementById("dynamic-employee_rows");

buttonEmployeesSelect.addEventListener("click", selectEmployees);
buttonEmployeesClear.addEventListener("click", clearEmployees);
buttonEmployeesShowInsertForm.addEventListener("click", showEmployeeInsertForm);
buttonEmployeeInsertSave.addEventListener("click", insertEmployeeSave);
buttonEmployeeInsertCancel.addEventListener("click", insertEmployeeCancel);
buttonEmployeeUpdateSave.addEventListener("click", updateEmployeeSave);
buttonEmployeeUpdateCancel.addEventListener("click", updateEmployeeCancel);



function selectEmployees() {
    var baseURL = "https://localhost:5001/SelectEmployees";
    var queryString = "";

    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = doAfterGetEmployees;

    xhr.open("GET", baseURL + queryString, true);
    xhr.send();

    function doAfterGetEmployees() {

        if (xhr.readyState === 4) { //done
            if (xhr.status === 200) { //ok

                var response = JSON.parse(xhr.responseText);

                if (response.result === "success") {
                    refreshEmployeeTable(response.employees);
                } else {
                    alert("API Error: " + response.message);
                }
            } else {
                alert("Server Error: " + xhr.status + " " + xhr.statusText);
            }
        }
    }
}

function insertEmployeeSave() {
    var inputFirstName = document.getElementById("employee-insert-first-name");
    var inputLastName = document.getElementById("employee-insert-last-name");
    var inputSalary = document.getElementById("employee-insert-salary");

    var firstName = inputFirstName.value;
    var lastName = inputLastName.value;
    var salary = inputSalary.value;

    var baseURL = "https://localhost:5001/InsertEmployee";
    var queryString = "?firstName=" + firstName + "&lastName=" + lastName + "&salary=" + salary;

    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = doAfterGetEmployees;

    xhr.open("GET", baseURL + queryString, true);
    xhr.send();

    function doAfterGetEmployees() {

        if (xhr.readyState === 4) { //done
            if (xhr.status === 200) { //ok

                var response = JSON.parse(xhr.responseText);

                if (response.result === "success") {
                    refreshEmployeeTable(response.employees);
                    inputFirstName.value = "";
                    inputLastName.value = "";
                    inputSalary.value = "";
                    document.getElementById("form-employee-insert").classList.add("my-hidden");
                } else {
                    alert("API Error: " + response.message);
                }
            } else {
                alert("Server Error: " + xhr.status + " " + xhr.statusText);
            }
        }
    }
}

function insertEmployeeCancel() {
    var inputFirstName = document.getElementById("employee-insert-first-name");
    var inputLastName = document.getElementById("employee-insert-last-name");
    var inputSalary = document.getElementById("employee-insert-salary");

    inputFirstName.value = "";
    inputLastName.value = "";
    inputSalary.value = "";

    document.getElementById("form-employee-insert").classList.add("my-hidden");
}

function updateEmployeeSave() {
    var inputEmployeeId = document.getElementById("employee-update-employee-id");
    var inputFirstName = document.getElementById("employee-update-first-name");
    var inputLastName = document.getElementById("employee-update-last-name");
    var inputSalary = document.getElementById("employee-update-salary");

    var employeeId = inputEmployeeId.value;
    var firstName = inputFirstName.value;
    var lastName = inputLastName.value;
    var salary = inputSalary.value;

    var baseURL = "https://localhost:5001/UpdateEmployee";
    var queryString = "?employeeId=" + employeeId + "&firstName=" + firstName + "&lastName=" + lastName + "&salary=" + salary;

    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = doAfterGetEmployees;

    xhr.open("GET", baseURL + queryString, true);
    xhr.send();

    function doAfterGetEmployees() {

        if (xhr.readyState === 4) { //done
            if (xhr.status === 200) { //ok

                var response = JSON.parse(xhr.responseText);

                if (response.result === "success") {
                    refreshEmployeeTable(response.employees);
                    inputEmployeeId.value = "";
                    inputFirstName.value = "";
                    inputLastName.value = "";
                    inputSalary.value = "";
                    document.getElementById("form-employee-update").classList.add("my-hidden");
                } else {
                    alert("API Error: " + response.message);
                }
            } else {
                alert("Server Error: " + xhr.status + " " + xhr.statusText);
            }
        }
    }
}

function updateEmployeeCancel() {
    var inputEmployeeId = document.getElementById("employee-update-employee-id");
    var inputFirstName = document.getElementById("employee-update-first-name");
    var inputLastName = document.getElementById("employee-update-last-name");
    var inputSalary = document.getElementById("employee-update-salary");

    inputEmployeeId.value = "";
    inputFirstName.value = "";
    inputLastName.value = "";
    inputSalary.value = "";

    document.getElementById("form-employee-update").classList.add("my-hidden");
}

function deleteEmployee(event) {
    var employeeId = event.target.dataset.employeeId;

    var baseURL = "https://localhost:5001/DeleteEmployee";
    var queryString = "?employeeId=" + employeeId;

    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = doAfterGetEmployees;

    xhr.open("GET", baseURL + queryString, true);
    xhr.send();

    function doAfterGetEmployees() {

        if (xhr.readyState === 4) { //done
            if (xhr.status === 200) { //ok

                var response = JSON.parse(xhr.responseText);

                if (response.result === "success") {
                    refreshEmployeeTable(response.employees);
                } else {
                    alert("API Error: " + response.message);
                }
            } else {
                alert("Server Error: " + xhr.status + " " + xhr.statusText);
            }
        }
    }
}

function showUpdateEmployeeForm(event) {
    document.getElementById("employee-update-employee-id").value = event.target.dataset.employeeId;
    document.getElementById("employee-update-first-name").value = event.target.dataset.firstName;
    document.getElementById("employee-update-last-name").value = event.target.dataset.lastName;
    document.getElementById("employee-update-salary").value = event.target.dataset.salary;

    document.getElementById("form-employee-update").classList.remove("my-hidden");
}

function showEmployeeInsertForm(event) {
    document.getElementById("form-employee-insert").classList.remove("my-hidden");
}

function refreshEmployeeTable(employees) {

    var employeeRows = '';

    for (var i = 0; i < employees.length; i++) {
        var employee = employees[i];
        employeeRows += '<tr>';
        employeeRows += '<td>' + employee.employeeId + '</td>';
        employeeRows += '<td>' + employee.firstName + '</td>';
        employeeRows += '<td>' + employee.lastName + '</td>';
        employeeRows += '<td>' + employee.salary + '</td>';
        employeeRows += '<td><img src="' + employee.imagePath + '" /></td>';
        employeeRows += '<td>' + '<button data-employee-id="' + employee.employeeId + '" data-first-name="' + employee.firstName + '" data-last-name="' + employee.lastName + '" data-salary="' + employee.salary + '" type="button" class="btn-employee-update btn btn-outline-secondary btn-sm">Update</button>' + '</td>'
        employeeRows += '<td>' + '<button data-employee-id="' + employee.employeeId + '" type="button" class="btn-employee-delete btn btn-outline-secondary btn-sm">Delete</button>' + '</td>'
        employeeRows += '</tr>';
    }

    dynamicEmployeeRows.innerHTML = employeeRows;

    var employeeUpdateButtons = document.getElementsByClassName("btn-employee-update");

    for (var i = 0; i < employeeUpdateButtons.length; i++) {
        var button = employeeUpdateButtons[i];
        button.addEventListener("click", showUpdateEmployeeForm);
    }

    var employeeDeleteButtons = document.getElementsByClassName("btn-employee-delete");

    for (var i = 0; i < employeeDeleteButtons.length; i++) {
        var button = employeeDeleteButtons[i];
        button.addEventListener("click", deleteEmployee);
    }
}

function clearEmployees() {
    dynamicEmployeeRows.innerHTML = "";
}

//Page load:
//selectEmployees();
