var object;
var tmp = [];
var Mandatory;
var flagsave = 0;
var iDiv;
var table1;
var urlpopup;
var change;
var cekclose=0;

function validateEmail(emailField) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test(emailField.value) == false) {
        alert('Invalid Email Address');
        emailField.style.border = "1px solid #FF0000";
        return false;
    } else {
        emailField.style.border = "1px solid #000000";
    }

    return true;

}
function cekmandatory(mandatory) {
    for (var i = 0; i < mandatory.length; i++) {
        var mandatory1 = mandatory[i];
        if (mandatory1.value == '') {
            mandatory1.style.border = "1px solid #FF0000";
            flagsave = 1;
        }
        else {
            mandatory1.style.border = "1px solid #000000";
            flagsave = 0;
        }
    }
}
function validate(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

function initdt() {
    $('.dataTables-example').DataTable({
        initComplete: function () {
            this.api().columns([0, 6]).every(function () {
                var column = this;
                var span = $('<span> ' + $(column.header()).text() + ' </span> &nbsp;').appendTo($('#group'))
                var select = $('<select><option value="">select</option></select>')
                    .appendTo($('#group'))
                    .on('change', function () {
                        column
                            .search(this.value)
                            .draw();
                    });
                var usedNames = [];
                column.data().unique().sort().each(function (d, j) {
                    var a = $(d).text().replace(/^\s+/g, '').split(/(\s+)/);
                    if (a.length > 1) {
                        if (a[2].match(/R./)) {
                            var b = a[2] + ' ' + a[4];
                            if (usedNames.indexOf(b) == -1) {

                                select.append('<option value="' + b + '"> ' + b + ' </option>')
                                usedNames.push(b);
                            }
                        }
                        else {
                            var c = a[2];
                            if (usedNames.indexOf(c) == -1) {

                                select.append('<option value="' + c + '"> ' + c + ' </option>')
                                usedNames.push(c);
                            }
                        }

                    }
                    else {
                        if (usedNames.indexOf(a[0]) == -1) {
                            select.append('<option value="' + a[0] + '"> ' + a[0] + ' </option>')
                            usedNames.push(a[0]);
                        }
                    }
                });
            });
        },
        "order": [[0, "desc"]],
        pageLength: 10,
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
            { extend: 'copy' },
            { extend: 'csv' },
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },

            {
                extend: 'print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]
    });
}
//$(document).ready(function () {

//    initdt();

//});
function pilih(isi,isi2, objecta,objectb) {
   
    document.getElementById(objecta).value = isi;
    if (objectb != 'undefined') {
        if (document.getElementById(objectb) != null) {
            document.getElementById(objectb).value = isi2;
        }
        
    }
   
    iDiv.remove();
  
    if (document.getElementById(objecta).className == 'change1') {
            $(".change1").trigger('change');
        }
    
    var elements = document.querySelectorAll('.col-sm-6');
    for (var i = 0; i < elements.length; i++) {
        elements[i].style.display = "none";

    }
    var elements2 = document.querySelectorAll('.col-sm-7');
    for (var i = 0; i < elements2.length; i++) {
        elements2[i].style.display = "none";

    }
}

function approve(URL, data) {
    $.ajax(
        {
            type: "POST",
            data: data,
            url: URL,
            contentType: "application/json",
            dataType: "json",
            success: function (response) {

                alert(response.d);

            },
            error: function (response) {
                alert("error");
            }
        });
}
function showmodal(URL, object1, object2) {
    $.ajax(
        {
            type: "POST",
            data: {},
            url: URL,
            contentType: "application/json",
            dataType: "json",
            success: function (response) {

                if (object1.constructor === Array) {
                    for (var i = 0; i < object1.length; i++) {
                        var head = object1[i].split('_');
                    }
                }
                else {
                    var head = object1.split('_');
                }
                if (object2.constructor === Array) {
                    for (var j = 0; j < object2.length; j++) {
                        if (object2[j] != 'undefined') {
                            var head2 = object2[j].split('_');
                        }
                    }
                }
                else {
                    var head2 = object2.split('_');
                }

                var isi = '';
                var isi2 = '';
                iDiv = document.createElement("div");
                iDiv.id = 'mpopupBox';
                iDiv.className = 'mpopup';
                var iDivsub1 = document.createElement("div");
                iDivsub1.className = 'mpopup-content';
                var iDivsub3 = document.createElement("div");
                iDivsub3.className = 'mpopup-head';
                var iDivsub2 = document.createElement("div");
                iDivsub2.className = 'mpopup-main';

                var iDivsub4 = document.createElement("div");
                iDivsub4.className = 'table-responsive';

                var iDivsub5 = document.createElement("div");
                iDivsub5.className = 'ibox-content';

                var span = document.createElement("span");
                span.className = 'close';
                span.innerHTML = 'X';

                table1 = document.createElement("table");
                table1.id = 'table1';
                table1.className = 'table table-striped table-bordered table-hover dataTables-example';
                var tbody = document.createElement("tbody");
                var thead = document.createElement("thead");
                var trhead = document.createElement("tr");

                var th = document.createElement("th");
                var th2 = document.createElement("th");
                var th3 = document.createElement("th");

                trhead.appendChild(th);
                th.innerHTML += head[0];

                if (object2 != 'undefined') {
                    trhead.appendChild(th2);
                    th2.innerHTML += head2[0];
                }
                trhead.appendChild(th3);
                th3.innerHTML += '';

                thead.appendChild(trhead);

                for (var i = 0; i < response.d.length; i++) {
                    var tr = document.createElement("tr");
                    var td2 = document.createElement("td");
                    var td = document.createElement("td");
                    var td3 = document.createElement("td");
                    isi = response.d[i].kode;
                    isi2 = response.d[i].desc;
                    td.innerHTML = response.d[i].kode;
                    if (response.d[i].desc != '') {
                        td3.innerHTML = response.d[i].desc;
                    }
                    td2.innerHTML += '<input type="submit" value="pilih" onclick="pilih(\'' + isi + '\',\'' + isi2 + '\',\'' + object1 + '\',\'' + object2 + '\')"/>';
                    tr.appendChild(td);

                    if (response.d[i].desc != '') {
                        tr.appendChild(td3);
                    }
                    tr.appendChild(td2);
                    tbody.appendChild(tr);
                }

                table1.appendChild(thead);
                table1.appendChild(tbody)
                iDivsub3.appendChild(span);
                iDivsub1.appendChild(iDivsub3);
                iDivsub1.appendChild(iDivsub2);
                iDivsub4.appendChild(table1);
                iDivsub5.appendChild(iDivsub4);
                iDivsub2.appendChild(iDivsub5);

                iDiv.appendChild(iDivsub1)

                document.body.appendChild(iDiv);
                iDiv.style.display = 'block';
                var table = $('#table1').DataTable();
                table.draw();

                var elements = document.querySelectorAll('.col-sm-6');
                for (var i = 0; i < elements.length; i++) {
                    elements[i].style.display = "block";

                }
                var elements2 = document.querySelectorAll('.col-sm-7');
                for (var i = 0; i < elements2.length; i++) {
                    elements2[i].style.display = "block";

                }

                span.onclick = function () {
                    var elements = document.querySelectorAll('.col-sm-6');
                    for (var i = 0; i < elements.length; i++) {
                        elements[i].style.display = "none";

                    }
                    var elements2 = document.querySelectorAll('.col-sm-7');
                    for (var i = 0; i < elements2.length; i++) {
                        elements2[i].style.display = "none";

                    }
                    // iDiv.style.display = "none";
                    iDiv.remove();
                }

            },
            error: function (response) {
                alert("error");
            }
        });

}
function generatebarcode(URL, data,status) {

    $.ajax(
        {
            type: "POST",
            data: data,
            url: URL,
            contentType: "application/json",
            dataType: "json",
            success: function (response) {
                if (status != 0) {
                    alert('Success');
                }
               
            },
            error: function (response) {
                alert("error");
            }
        });
}

function openbarcode(URL, data, status) {

    $.ajax(
        {
            type: "POST",
            data: data,
            url: URL,
            contentType: "application/json",
            dataType: "json",
            success: function (response) {
               // alert(response.d);
               window.open(response.d, '_blank', 'fullscreen=yes')

            },
            error: function (response) {
                alert("error");
            }
        });
}
function showpopupForm(object1, object2, filter, URL) {
    var filter1='';
    if (filter != '') {
        filter1 = document.getElementById(filter).value;
    }
    $.ajax(
        {
            type: "POST",
            data: "{'filter':'" + filter1+"'}",
            url: URL,
            contentType: "application/json",
            dataType: "json",
            success: function (response) {

                if (object1.constructor === Array) {
                    for (var i = 0; i < object1.length; i++) {
                        var head = object1[i].split('_');
                    }
                }
                else {
                    var head = object1.split('_');
                }
                if (object2.constructor === Array) {
                    for (var j = 0; j < object2.length; j++) {
                        if (object2[j] != 'undefined') {
                            var head2 = object2[j].split('_');
                        }
                    }
                }
                else {
                    var head2 = object2.split('_');
                }

                var isi = '';
                var isi2 = '';
                iDiv = document.createElement("div");
                iDiv.id = 'mpopupBox';
                iDiv.className = 'mpopup';
                var iDivsub1 = document.createElement("div");
                iDivsub1.className = 'mpopup-content';
                var iDivsub3 = document.createElement("div");
                iDivsub3.className = 'mpopup-head';
                var iDivsub2 = document.createElement("div");
                iDivsub2.className = 'mpopup-main';

                var iDivsub4 = document.createElement("div");
                iDivsub4.className = 'table-responsive';

                var iDivsub5 = document.createElement("div");
                iDivsub5.className = 'ibox-content';

                var span = document.createElement("span");
                span.className = 'close';
                span.innerHTML = 'X';

                table1 = document.createElement("table");
                table1.id = 'table1';
                table1.className = 'table table-striped table-bordered table-hover dataTables-example';
                var tbody = document.createElement("tbody");
                var thead = document.createElement("thead");
                var trhead = document.createElement("tr");

                var th = document.createElement("th");
                var th2 = document.createElement("th");
                var th3 = document.createElement("th");

                trhead.appendChild(th);
                th.innerHTML += head[0];

                if (object2 != 'undefined') {
                    trhead.appendChild(th2);
                    th2.innerHTML += head2[0];
                }
                trhead.appendChild(th3);
                th3.innerHTML += '';

                thead.appendChild(trhead);

                for (var i = 0; i < response.d.length; i++) {
                    var tr = document.createElement("tr");
                    var td2 = document.createElement("td");
                    var td = document.createElement("td");
                    var td3 = document.createElement("td");
                    isi = response.d[i].kode;
                    isi2 = response.d[i].desc;
                    td.innerHTML = response.d[i].kode;
                    if (response.d[i].desc != '') {
                        td3.innerHTML = response.d[i].desc;
                    }
                    td2.innerHTML += '<input type="submit" value="pilih" onclick="pilih(\'' + isi + '\',\'' + isi2 + '\',\'' + object1 + '\',\'' + object2 + '\')"/>';
                    tr.appendChild(td);

                    if (response.d[i].desc != '') {
                        tr.appendChild(td3);
                    }
                    tr.appendChild(td2);
                    tbody.appendChild(tr);
                }

                table1.appendChild(thead);
                table1.appendChild(tbody)
                iDivsub3.appendChild(span);
                iDivsub1.appendChild(iDivsub3);
                iDivsub1.appendChild(iDivsub2);
                iDivsub4.appendChild(table1);
                iDivsub5.appendChild(iDivsub4);
                iDivsub2.appendChild(iDivsub5);

                iDiv.appendChild(iDivsub1)

                document.body.appendChild(iDiv);
                iDiv.style.display = 'block';
                var table = $('#table1').DataTable();
                table.draw();

                var elements = document.querySelectorAll('.col-sm-6');
                for (var i = 0; i < elements.length; i++) {
                    elements[i].style.display = "block";

                }
                var elements2 = document.querySelectorAll('.col-sm-7');
                for (var i = 0; i < elements2.length; i++) {
                    elements2[i].style.display = "block";

                }
                span.onclick = function () {

                    var elements = document.querySelectorAll('.col-sm-6');
                    for (var i = 0; i < elements.length; i++) {
                        elements[i].style.display = "none";

                    }
                    var elements2 = document.querySelectorAll('.col-sm-7');
                    for (var i = 0; i < elements2.length; i++) {
                        elements2[i].style.display = "none";

                    }
                    // iDiv.style.display = "none";
                    iDiv.remove();
                }

            },
            error: function (response) {
                alert("error");
            }
        });


}