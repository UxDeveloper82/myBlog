var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#mypost").DataTable({
        ajax: {
            url: '/Panel/GetAll'
        },
        columns: [
            { data: "id","width": "15%"},
            { data: "title", "width": "15%" },
            { data: "body", "width": "15%" },
            { data: "description", "width": "15%" },
            { data: "category", "width": "15%" },
            { data: "tags", "width": "15%" },
            {
                data: "id",
                render: function (data) {
                    return `
                         <div class="text-center">
                               <a href="/Panel/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer">
                                     <i class="bi bi-pencil-square"></i> Edit
                              </a>
                              <a onclick=Delete("/Panel/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                  <i class="bi bi-trash-fill"></i> Delete
                              </a>
                          </div>
                         `;
                }, "width":"40%"
            },
        ]
    });
}