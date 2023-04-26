$(() => {
    const addModal = new bootstrap.Modal($('#add-modal')[0]);
    const editModal = new bootstrap.Modal($('#edit-modal')[0]);


    function refreshTable() {
        $("tbody").empty();
        $.get('/home/getallpeople', function (people) {
            people.forEach(function (person) {
                $("tbody").append(`<tr>
            <td>${person.firstName}</td>
            <td>${person.lastName}</td>
            <td>${person.age}</td>   
             <td>   <button class="btn btn-primary" id="edit" data-edit-id="${person.Id}">Edit</button> </td>          
             <td>   <button class="btn btn-primary" id="delete" data-delete-id="${person.Id}">Delete</button> </td>          
       </tr>`)
            });
        });
    }

    refreshTable();

    $("#add-person").on('click', function () {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        addModal.show();
    });

    $("#save-person").on('click', function () {
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = $("#age").val();


        //const person = {
        //    firstName,
        //    lastName,
        //    age
        //};

        $.post('/home/addperson', { firstName, lastName, age }, function () {
            addModal.hide();
            refreshTable();
        });
    });

    $("tbody").on('click', '#edit', function () {
        $("#editFirstName").val('');
        $("#editLastName").val('');
        $("#editAge").val('');

        const id = $(this).data('edit-id')
        $.get('/home/getpersonbyid', {id}, function (person) {
            $("#editFirstName").val(person.firstName);
            $("#editLastName").val(person.lastName);
            $("#editAge").val(person.age);
            editModal.show();
        });
    })
})