// script.js
function getEmployees() {
    $.get("https://your-api-url/api/Employee", function (data) {
        displayEmployees(data);
    });
}

function displayEmployees(employees) {
    var employeeList = "<ul>";
    employees.forEach(function (employee) {
        employeeList += `<li>${employee.FirstName} ${employee.LastName}</li>`;
    });
    employeeList += "</ul>";

    $("#employees").html(employeeList);
}