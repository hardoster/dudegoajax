$(document).ready(function () {

    var table = $('#tbDetails').DataTable({
        pageLength: 5,
        language: {
            // Configuración del idioma de la tabla
        },
    });
    
        var table2 = $('#TableInvoicesSearch').DataTable({
            language: {
                // Configuración del idioma de la tabla
            },
        });

    //PARA TABLA DE CLIENTES
    var table3 = $('#TableCustomers').DataTable({
        pageLength: 3,
        language: {
            // Configuración del idioma de la tabla
        },
    });

    //PARA TABLA DE Invoicedetails
    var table3 = $('#TbInvDt').DataTable({
        pageLength: 3,
        language: {
            // Configuración del idioma de la tabla
        },
    });


    //PARA TABLA DE vehiculos
    var table3 = $('#TableCars').DataTable({
        pageLength: 3,
        language: {
            // Configuración del idioma de la tabla
        },
    });




   

    $('#addButton').on('click', function () {
        var dayDuration = $('#DayDuration').val();
        var priceDay = $('#PriceDay').val();
        var rentalDeposit = $('#RentalDeposit').val();
        var idCar = document.querySelector('#IdCarOPT').value;
        var idInvoice = $('#idInvoiceInput').val();

        // Validación de datos antes de agregar la fila a la tabla
        if (dayDuration && priceDay && rentalDeposit && idCar && idInvoice) {
            table.row.add([
                dayDuration,
                priceDay,
                rentalDeposit,
                idCar,
                idInvoice
            ]).draw(false);
        } else {
            alert('Por favor, complete todos los campos.');
        }
    });









    $('#submit-button').on('click', function (event) {

        var dataToSend = [];
        $('#tbDetails tbody tr').each(function () {
            var row = $(this);

            var rowData = {
                IdInvoice: parseFloat(row.find('td:eq(4)').text()),
                IdCar: parseFloat(row.find('td:eq(3)').text()), // Si IdCar es un string, no uses parseFloat
                DayDuration: parseFloat(row.find('td:eq(0)').text()), // Parsea los valores a números
                PriceDay: parseFloat(row.find('td:eq(1)').text()),
                RentalDeposit: parseFloat(row.find('td:eq(2)').text())
            };
            dataToSend.push(rowData);
        });

        console.log(dataToSend);

        // Realiza la solicitud AJAX
        $.ajax({
            url: '/MasterDetails/ListDetail78',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataToSend), // Convierte el array en JSON antes de enviarlo
            success: function (data) {
                console.log('Datos enviados con éxito:', data);
                // Aquí puedes manejar la respuesta del servidor si es necesario
                window.location.href = 'https://localhost:44315';

            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                // Aquí puedes manejar cualquier error ocurrido durante la solicitud AJAX
            }
        });
    });

    













    
   /* $('#submit-button').on('click', function (event) {
        event.preventDefault(); // Previene el envío del formulario por defecto

        var dataToSend = [];
        $('#tbDetails tbody tr').each(function () {
            var row = $(this);

            var rowData = {
                diasArrendamiento: row.find('td:eq(0)').text(),
                PrecioDia: row.find('td:eq(1)').text(),
                Deposito: row.find('td:eq(2)').text(),
                Placa: row.find('td:eq(3)').text(),
                Factura: row.find('td:eq(4)').text()
            };
            dataToSend.push(rowData);
        });

        // Muestra el JSON en la consola
        document.querySelector('#jSONinvoiceDetails').value = dataToSend;

        var imprim = document.querySelector('#jSONinvoiceDetails').value;
        console.log(dataToSend)
        console.log(imprim);
        // No envía el formulario
    });
    */

   /*
    $('#submit-button').on('click', function (event) {
        event.preventDefault(); // Previene el envío del formulario por defecto

        var dataToSend = [];
        $('#tbDetails tbody tr').each(function () {
            var row = $(this);

            var rowData = {
                DayDuration: row.find('td:eq(0)').text(),
                PriceDay: row.find('td:eq(1)').text(),
                RentalDeposit: row.find('td:eq(2)').text(),
                IdCar: row.find('td:eq(3)').text(),
                IdInvoice: row.find('td:eq(4)').text()
            };
            dataToSend.push(rowData);
        });

        // Asigna los datos a un campo oculto para ser enviados con el formulario
        $('#jSONinvoiceDetails').val(JSON.stringify(dataToSend));
        console.log(dataToSend); // Para depuración

        // Envía el formulario
        $('#invoiceForm').submit();
    });
    */






});
