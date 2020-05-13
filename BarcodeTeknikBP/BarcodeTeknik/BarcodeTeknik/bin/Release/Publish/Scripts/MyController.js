var id1, id1Dtl;
var url1, urldtl1;
var url2, url2dtl;
var urlpopup = new Array();
var urlpopupdtl = new Array();
var urlddl;
var k = 0;
var kdtl = 0;
var tmp = [];
var tmpdtl = [];
var data = [];
var options=[];
var dropdownname = [];
var customurl;
var tampungkdbarang = new Array();
var tampungdescruang = new Array();
var row1 = new Array();
var row2 = new Array();
var urlddl2;
var uniqueArray = [];
var uniqueArray2 = [];
var loaddata = 10;
var loaddatadtl = 10;
var page1 = loaddata - loaddata;
var page1dtl = loaddatadtl - loaddatadtl;
var halaman = 1;
var halamandtl = 1;
var filter1 = '', filter2 = '', barcode1 = '', barcode2 = '', Key = '', Key2='';
var TotalSeluruh = '';


function initheader($scope, $http, $compile, $rootScope, DTOptionsBuilder) {
    if (document.getElementById('page') != null) {
        document.getElementById('page').value = halaman;
    }

    if (document.getElementById('txtsearch') != null) {
        var search = document.getElementById('txtsearch').value;
		
    }
    else {
        search = '';
    }
 
    $http({
        url: url1,
        method: "POST",
        data: {"filter":search},
        headers: { 'Content-Type': 'application/json' }
    }).then(function (response) {
       
        $scope.mastetampung = JSON.parse(response.data.d);

        if ($scope.activeId1 != null && $scope.activeId1 != '') {
            var filtered = $scope.mastetampung.filter(function (item) {
                var reg = new RegExp($scope.activeId1, 'g');
                return item[filter1].match(reg);
            });
            $scope.master = new Array();
            var jumlahdata;

            jumlahdata = (loaddata * halaman);
            if (jumlahdata < filtered.length) {
                jumlahdata = (loaddata * halaman);
            }
            else {
                jumlahdata = filtered.length;
            }
            uniqueArray = new Array();
            uniqueArray2 = new Array();
           
            for (var page = page1; page < jumlahdata; page++) {
                $scope.master.push(filtered[page]);
            }

            $.each(filtered, function (i, obj) {
                tampungkdbarang.push(obj[barcode1])
            });
            $.each(filtered, function (i, obj) {
                tampungdescruang.push(obj[barcode2])
            });
            uniqueArray = tampungkdbarang.filter(function (elem, pos) {
                return tampungkdbarang.indexOf(elem) == pos;
            });
            uniqueArray2 = tampungdescruang.filter(function (elem, pos) {
                return tampungdescruang.indexOf(elem) == pos;
            });
			TotalSeluruh = filtered.length;
            document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
        }
        else {
            $scope.master = new Array();
            var jumlahdata;


            jumlahdata = (loaddata * halaman);
            if (jumlahdata < $scope.mastetampung.length) {
                jumlahdata = (loaddata * halaman);
            }
            else {
                jumlahdata = $scope.mastetampung.length;
               
            }

            for (var page = page1; page < jumlahdata; page++) {
                $scope.master.push($scope.mastetampung[page]);
              
            }
            $.each($scope.mastetampung, function (i, obj) {
                tampungkdbarang.push(obj[barcode1])
            });
            $.each($scope.mastetampung, function (i, obj) {
                tampungdescruang.push(obj[barcode2])
            });
            uniqueArray = tampungkdbarang.filter(function (elem, pos) {
                  return tampungkdbarang.indexOf(elem) == pos;
            });
            uniqueArray2 = tampungdescruang.filter(function (elem, pos) {
                 return tampungdescruang.indexOf(elem) == pos;
            });
			 TotalSeluruh = $scope.mastetampung.length;
            document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
        }
        
          $scope.getrow = function checkone(row) {
            var isChecked = document.getElementById('checkBox_' + row).checked;
            var tmpdata;
            var tmpdata2;
           
             if (document.getElementById(barcode1+'_' + row) != null) {
                 tmpdata = document.getElementById(barcode1+'_' + row).value;
                 tmpdata2 = document.getElementById(barcode2+'_' + row).value;
                 if (isChecked) {
                     row1.push(tmpdata);
                     row2.push(tmpdata2);
                 }
                 else {
                    var index3 = row1.indexOf(tmpdata);
                    var index4 = row2.indexOf(tmpdata2);
                    row1.splice(index3, 1);
                    if (barcode1 == barcode2) {
                        row2.splice(index4, 1);
                    }
                    
                 }
            }

        };
         
        $scope.FormEdit1 = function click1(row) {
            var i = 0;
            var j = 0;
            var k = 0;
            var flag = 0;
            var a = document.getElementsByClassName('control_' + row);
            var b = document.getElementsByClassName('hidewhenclick_' + row);

            $("#FormInput").show();
            $("#Save").hide();
            $("#update").show();
            $("#Cancel").show();
            $(".disableform").attr('disabled', 'disabled');
            if (document.getElementById('NewForm') != null) {
                document.getElementById('NewForm').style.display = 'none';
            }
          
            for (i = 0; i < a.length; i++) {
                if (document.getElementById(id1[i]) != null) {
                    document.getElementById(id1[i]).value = a[i].value;
                }
                else if (document.getElementsByName(id1[i])[0] != null) {
                    if (document.getElementsByName(id1[i])[0].type == 'radio') {
                        if (a[i].type == 'radio') {
                            if (a[i].checked == true) {
                                $("input[name='" + id1[i] + "'][value='" + a[i].value + "']").prop('checked', true);
                            }
                            else {
                                $("input[name='" + id1[i] + "'][value='" + a[i].value + "']").prop('checked', false);
                            }
                        }
                        else {
                            $("input[name='" + id1[i] + "'][value='" + a[i].value + "']").prop('checked', true);
                        }

                    }
                    else if(document.getElementsByName(id1[i])[0].type == 'checkbox') {
                        if (a[i].type == 'checkbox') {
                            if (a[i].checked == true) {
                                $("input[name='" + id1[i] + "']").prop('checked', true);
                                $("input[name='" + id1[i] + "']").prop('value', a[i].value);
                            }
                            else {
                                $("input[name='" + id1[i] + "']").prop('checked', false);
                                $("input[name='" + id1[i] + "']").prop('value', a[i].value);
                            }
                        }
                        else {
                            $("input[name='" + id1[i] + "']").prop('checked', true);
                            $("input[name='" + id1[i] + "']").prop('value', a[i].value);
                        }

                    }

                }
            }
          };

        $rootScope.$on("Callsave1", function () {
            $scope.save1();
        });
       
        $scope.nextprev = function init1(val) {
            if (val == 'next') {
               
             
                page1 += 10;
               
                if ($scope.activeId1 != null && $scope.activeId1 != '') {
                    var filtered;
                    if ($scope.activeId2 != '' && $scope.activeId2 != null) {
                        filtered = $scope.mastetampung.filter(function (item) {
                            var reg = new RegExp($scope.activeId1, 'g');
                           
                            return item[filter1].match(reg) && item[filter2].match($scope.activeId2);
                        });
                       }
                    else
                    {
                        filtered = $scope.mastetampung.filter(function (item) {
                            var reg = new RegExp($scope.activeId1, 'g');
                            return item[filter1].match(reg);
                        });
                    }
                  
                    if (page1 < filtered.length) {
                        $('#loading').show();
                        halaman += 1;

                        $scope.master = new Array();
                        var jumlahdata;

                        jumlahdata = (loaddata * halaman);
                        if (jumlahdata < filtered.length) {
                            jumlahdata = (loaddata * halaman);
                        }
                        else {
                            jumlahdata = filtered.length;
                        }

                        for (var page = page1; page < jumlahdata; page++) {
                            $scope.master.push(filtered[page]);
                        }
                        document.getElementById('page').value = halaman;
                    }
                    else {
                        $('#loading').hide();
                        page1 -= 10;
                    }
					TotalSeluruh = filtered.length;
					document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                }
                else {
                    if (page1 < $scope.mastetampung.length) {
                        halaman += 1;
                        $scope.master = new Array();
                        var jumlahdata;


                        jumlahdata = (loaddata * halaman);
                        if (jumlahdata < $scope.mastetampung.length) {
                            jumlahdata = (loaddata * halaman);
                        }
                        else {
                            jumlahdata = $scope.mastetampung.length;
                        }
                        for (var page = page1; page < jumlahdata; page++) {
                            $scope.master.push($scope.mastetampung[page]);
                        }
                        document.getElementById('page').value = halaman;
                    }
                    else {
                        $('#loading').hide();
                        page1 -= 10;
                    }
					TotalSeluruh = $scope.mastetampung.length;
					document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                }
             
            }
            else if (val == 'prev') {
                
                if (halaman != 1) {
                    $('#loading').show();
                    halaman -= 1;
                    page1 -= 10;


                    //delete $scope.master;
                    if ($scope.activeId1 != null && $scope.activeId1 != '') {
                        var filtered;
                        if ($scope.activeId2 != '' && $scope.activeId2 != null) {
                            filtered = $scope.mastetampung.filter(function (item) {
                                var reg = new RegExp($scope.activeId1, 'g');
                                return item[filter1].match(reg) && item.kodebarang.match($scope.activeId2);
                            });
                        }
                        else {
                            filtered = $scope.mastetampung.filter(function (item) {
                                var reg = new RegExp($scope.activeId1, 'g');
                                return item[filter1].match(reg);
                            });
                        }

                        $scope.master = new Array();
                        var jumlahdata;
                       
                            jumlahdata = (loaddata * halaman);
                        
                        for (var page = page1; page < jumlahdata; page++) {
                            $scope.master.push(filtered[page]);
                        }
						TotalSeluruh = filtered.length;
					    document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                    }
                    else {
                        $scope.master = new Array();
                        var jumlahdata;
                       
                       
                            jumlahdata = (loaddata * halaman);
                        
                        for (var page = page1; page < jumlahdata; page++) {
                            $scope.master.push($scope.mastetampung[page]);
                        }
						TotalSeluruh = $scope.mastetampung.length;
					    document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                    }
                    document.getElementById('page').value = halaman;
                }
            
                  
            }
           
          };
		   $scope.getcheckedfalse = function checkedfalse(index1) {
            var a = false;
            if (row1.length > 0) {
                index = row1.indexOf($scope.master[index1][barcode1]);
                if (index != -1) {
                   
                    a = true;
                }
                else {

                    a = false;
                }
            }
            return a;
        };
          
        $scope.search1 = function search1(row) {
            $('#loading').show();
            txtsearch = document.getElementById('txtsearch').value;
            if (txtsearch.length >= 3) {
                tampungkdbarang = new Array();
                tampungdescruang = new Array();

                if (document.getElementById('ddlfilter') != null) {
                    var a = $('#ddlfilter :selected').text();
                    if (a != 'select') {

                        $scope.activeId1 = a;

                        $http({
                            url: url1,
                            method: "POST",
                            data: { "filter": txtsearch },
                            headers: { 'Content-Type': 'application/json' }
                        }).then(function (response) {

                            $scope.mastetampung = JSON.parse(response.data.d);
                            var filtered;
                            if ($scope.activeId2 != '' && $scope.activeId2 != null) {
                                filtered = $scope.mastetampung.filter(function (item) {
                                    var reg = new RegExp($scope.activeId1, 'g');
                                    return item[filter1].match(reg) && item[filter2].match($scope.activeId2);
                                });
                            }
                            else {
                                filtered = $scope.mastetampung.filter(function (item) {
                                    var reg = new RegExp($scope.activeId1, 'g');
                                    return item[filter1].match(reg);
                                });
                            }
                            uniqueArray = new Array();
                            uniqueArray2 = new Array();
                            $.each(filtered, function (i, obj) {
                                tampungkdbarang.push(obj[barcode1])
                            });
                            $.each(filtered, function (i, obj) {
                                tampungdescruang.push(obj[barcode2])
                            });
                            uniqueArray = tampungkdbarang.filter(function (elem, pos) {
                                return tampungkdbarang.indexOf(elem) == pos;
                            });
                            uniqueArray2 = tampungdescruang.filter(function (elem, pos) {
                                return tampungdescruang.indexOf(elem) == pos;
                            });

                            $scope.master = new Array();
                            var jumlahdata;

                            jumlahdata = (loaddata * halaman);
                            if (jumlahdata < $scope.mastetampung.length) {
                                jumlahdata = (loaddata * halaman);
                            }
                            else {
                                jumlahdata = filtered.length;
                            }
                            for (var page = page1; page < jumlahdata; page++) {
                                $scope.master.push(filtered[page]);

                            }
							TotalSeluruh = filtered.length;
							document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                            document.getElementById('page').value = halaman;

                            if ($scope.master.length==0) {
                                $('#loading').hide();
                            }
                        });
                       
                    }
                }
                else {
                    $scope.activeId1 = txtsearch;

                    $http({
                        url: url1,
                        method: "POST",
                        data: { "filter": txtsearch },
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function (response) {

                        $scope.mastetampung = JSON.parse(response.data.d);

                        $scope.master = new Array();
                        var jumlahdata;

                        jumlahdata = (loaddata * halaman);
                        if (jumlahdata < $scope.mastetampung.length) {
                            jumlahdata = (loaddata * halaman);
                        }
                        else {
                            jumlahdata = $scope.mastetampung.length;
                        }
                        for (var page = page1; page < jumlahdata; page++) {
                            $scope.master.push($scope.mastetampung[page]);
                        }
						TotalSeluruh = $scope.mastetampung.length;
						document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                        document.getElementById('page').value = halaman;

                        if ($scope.mastetampung.length > 0) {
                            uniqueArray = new Array();
                            uniqueArray2 = new Array();
                            $.each($scope.mastetampung, function (i, obj) {
                                tampungkdbarang.push(obj[barcode1])
                            });
                            $.each($scope.mastetampung, function (i, obj) {
                                tampungdescruang.push(obj[barcode2])
                            });
                            uniqueArray = tampungkdbarang.filter(function (elem, pos) {
                                return tampungkdbarang.indexOf(elem) == pos;
                            });
                            uniqueArray2 = tampungdescruang.filter(function (elem, pos) {
                                return tampungdescruang.indexOf(elem) == pos;
                            });
                        }
                        else {
                            
                                $('#loading').hide();
                            
                        }
                    });
                }
              
            }
            else {
                $('#loading').hide();
                alert('Minimal 3 karakter!');
            }
            
        };

        $rootScope.$on("Callupdateform", function (event,notif) {
            $scope.updateform(notif);
        });
        $scope.closeEdit1 = function click2(row) {
            var a = document.getElementsByClassName('control_' + row);
            var b = document.getElementsByClassName('hidewhenclick_' + row);
            for (var i = 0; i < a.length; i++) {
                a[i].style.display = 'none';
                a[i].value = tmp[i];
            }
            for (var j = 0; j < b.length; j++) {
                b[j].style.display = '';
            }
           
        };
        $scope.filteradd = function filter() {
            tampungkdbarang = new Array();
            tampungdescruang = new Array();
           

            document.getElementById('page').value = halaman;

            var a = $('#ddlfilter :selected').text();
            if (a != 'select') {
                
                $scope.activeId1 = a;
            }
            var b = $('#ddlfilter2 :selected').text();
            if (b != 'select') {
                $scope.activeId2 =b;
               
            } else {
               
                $scope.activeId2 = '';
            }
            if (document.getElementById('txtsearch') != null) {
                var search = document.getElementById('txtsearch').value;
            }
            else {
                search = '';
            }
          
            $http({
                url: url1,
                method: "POST",
                data: { "filter": search },
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                if (document.getElementById('Detail') != null) {
                    angular.element('#angulardiv').append(angular.element('#Detail'));
                    document.getElementById('Detail').style.display = 'none';
                    angular.element('#trdetail').remove();
                    angular.element('.expand').val('+');
                }
             
                $scope.mastetampung = JSON.parse(response.data.d);
                var filtered;
                if ($scope.activeId2 != '' && $scope.activeId2 != null) {
                    filtered = $scope.mastetampung.filter(function (item) {
                        var reg = new RegExp($scope.activeId1, 'g');
                        return item[filter1].match(reg) && item[filter2].match($scope.activeId2);
                    });
                }
                else {
                    filtered = $scope.mastetampung.filter(function (item) {
                        var reg = new RegExp($scope.activeId1, 'g');
                        return item[filter1].match(reg);
                    });
                }
                uniqueArray = new Array();
                uniqueArray2 = new Array();

                $scope.master = new Array();
                jumlahdata = (loaddata * halaman);
                if (jumlahdata < filtered.length) {
                    jumlahdata = (loaddata * halaman);
                }
                else {
                    jumlahdata = filtered.length;
                }
                for (var page = page1; page < jumlahdata; page++) {
                    $scope.master.push(filtered[page]);
                  
                }
				TotalSeluruh = filtered.length;
				document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                $.each(filtered, function (i, obj) {
                    tampungkdbarang.push(obj[barcode1])
                });
                $.each(filtered, function (i, obj) {
                    tampungdescruang.push(obj[barcode2])
                });
                uniqueArray = tampungkdbarang.filter(function (elem, pos) {
                    return tampungkdbarang.indexOf(elem) == pos;
                });
                uniqueArray2 = tampungdescruang.filter(function (elem, pos) {
                    return tampungdescruang.indexOf(elem) == pos;
                });
              });
        };
        $scope.pushAray = function push1(val, val2) {
           

            tampungkdbarang.push(val);
         
            if (val2 != '' && val2 != null) {
                tampungdescruang.push(val2);
            }
            uniqueArray = tampungkdbarang.filter(function (elem, pos) {
                return tampungkdbarang.indexOf(elem) == pos;
            });
            uniqueArray2 = tampungdescruang.filter(function (elem, pos) {
                return tampungdescruang.indexOf(elem) == pos;
            });
           
        };
        $scope.Close1 = function click3(row) {
           
            var data = "{";
            for (var i = 0; i < id1.length; i++) {
                if (i == id1.length - 1) {
                    var object;
                    if (document.getElementById(id1[i] + '_' + row) != null) {
                        object = document.getElementById(id1[i] + '_' + row).value;
                    }
                 
                    data += id1[i].replace(/'/g, "") + ':' + '\'' + object + '\'' + ',' + "tipe:'4'}";
                } else {
                    var object;
                    if (document.getElementById(id1[i] + '_' + row) != null) {
                        object = document.getElementById(id1[i] + '_' + row).value;
                    }
                  
                    data += id1[i].replace(/'/g, "") + ':' + '\'' + object + '\'' + ',';
                }
            }
            swal({
                title: "Informasi",
                text: "Apakah anda yakin? Pastikan lebih barang sudah dikembalikan!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Close Wo",
                cancelButtonText: "Cancel",
                closeOnConfirm: true,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        if (document.getElementById(id1[2] + '_' + row).value != '' && document.getElementById(id1[3] + '_' + row).value != '' && document.getElementById(id1[7] + '_' + row).value != '' && document.getElementById(id1[8] + '_' + row).value != '' && document.getElementById(id1[9] + '_' + row).value != '' && document.getElementById(id1[10] + '_' + row).value != '' && document.getElementById(id1[11] + '_' + row).value != '') {
                            $('#loading').show();
                            $http({
                                url: url2,
                                method: "POST",
                                data: data,
                                headers: { 'Content-Type': 'application/json' }
                            }).then(function (response) {


                                var index;
                                var index2;


                                var val = JSON.parse(response.data.d);
                                var p = val['WO'];
                                var u = val['Contract'];
                                if (p != '' && u != '')
                                {
                                    index = $scope.master.findIndex(obj => obj[Key] === val['WO'] && obj[Key2] === val['Contract']);


                                    index2 = $scope.mastetampung.findIndex(obj => obj[Key] === val['WO'] && obj[Key2] === val['Contract']);


                                    $scope.master.splice(index, 1);
                                    $scope.mastetampung.splice(index2, 1);

                                    if ($scope.activeId1 != null && $scope.activeId1 != '') {
                                        if ($scope.activeId2 != null && $scope.activeId2 != '') {
                                            var filtered = $scope.mastetampung.filter(function (item) {
                                                var reg = new RegExp($scope.activeId1, 'g');
                                                return item[filter1].match(reg) && item[filter2].match($scope.activeId2);
                                            });
                                        }
                                        else {
                                            var filtered = $scope.mastetampung.filter(function (item) {
                                                var reg = new RegExp($scope.activeId1, 'g');
                                                return item[filter1].match(reg);
                                            });
                                        }

                                        $scope.master = new Array();
                                        var jumlahdata;

                                        jumlahdata = (loaddata * halaman);
                                        if (jumlahdata < filtered.length) {
                                            jumlahdata = (loaddata * halaman);
                                        }
                                        else {
                                            jumlahdata = filtered.length;
                                        }

                                        for (var page = page1; page < jumlahdata; page++) {
                                            $scope.master.push(filtered[page]);
                                        }
                                        TotalSeluruh = filtered.length;
                                        document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                                    }
                                    else {
                                        $scope.master = new Array();
                                        var jumlahdata;


                                        jumlahdata = (loaddata * halaman);
                                        if (jumlahdata < $scope.mastetampung.length) {
                                            jumlahdata = (loaddata * halaman);
                                        }
                                        else {
                                            jumlahdata = $scope.mastetampung.length;
                                        }
                                        for (var page = page1; page < jumlahdata; page++) {
                                            $scope.master.push($scope.mastetampung[page]);
                                        }
                                        TotalSeluruh = $scope.mastetampung.length;
                                        document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                                    }

                                    $rootScope.$emit("Callreloaddtl", {});
                                }
                                else {
                                    $('#loading').hide();
                                    alert('Harap cek status WO IFS (Released) jika ya lanjutkan cek Qty LK apakah mencukupi');
                                      
                                }
                            });
                        }
                        else {
                            $('#loading').hide();
							 alert('Harap mengisi penyebab, solusi dan NIK dahulu!');
                           
                        }
                    }
                });
           
        };
        $scope.delete1 = function click4(row) {
           
            var data = "{";
              for (var i = 0; i < id1.length; i++) {
                if (i == id1.length - 1) {
                    var object;
                    if (document.getElementById(id1[i] + '_' + row) != null) {
                        object = document.getElementById(id1[i]+'_'+row).value;
                    }
                    else if (document.getElementsByName(id1[i] + '_' + row)[0] != null) {
                        if (document.getElementsByName(id1[i] + '_' + row)[0].type == 'checkbox') {
                            object = $("input[name='" + id1[i] + '_' + row + "']").val();
                        }
                        else if (document.getElementsByName(id1[i] + '_' + row)[0].type == 'radio')
                        {
                            object = $("input[name='" + id1[i] + '_' + row + "']:checked").val();
                        }
                        else {
                            object = document.getElementsByName(id1[i] + '_' + row)[0].value;
                        }
                    }
                    data += id1[i].replace(/'/g, "") + ':' + '\'' + object + '\'' + ',' + "tipe:'3'}";
                } else {
                    var object;
                    if (document.getElementById(id1[i] + '_' + row) != null) {
                        object = document.getElementById(id1[i] + '_' + row).value;
                    }
                    else if (document.getElementsByName(id1[i] + '_' + row)[0] != null) {
                        if (document.getElementsByName(id1[i] + '_' + row)[0].type == 'checkbox') {
                            object = $("input[name='" + id1[i] + '_' + row + "']:checked").val();
                        }
                        else if (document.getElementsByName(id1[i] + '_' + row)[0].type == 'radio') {
                            object = $("input[name='" + id1[i] + '_' + row + "']:checked").val();
                        }
                        else {
                            object = document.getElementsByName(id1[i] + '_' + row)[0].value;
                        }
                    }
                    data += id1[i].replace(/'/g, "") + ':' + '\'' + object + '\'' + ',';
                }
            }
			  swal({
                  title: "Informasi",
                  text: "Apakah anda yakin?",
                  type: "warning",
                  showCancelButton: true,
                  confirmButtonColor: "#DD6B55",
                  confirmButtonText: "Ok",
                  cancelButtonText: "Cancel",
                  closeOnConfirm: true,
                  closeOnCancel: true
              },
                  function (isConfirm) {
                      if (isConfirm) {
            $http({
                url: url2,
                method: "POST",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                if (!response.data.d.match(/err/g)) {
                    //swal({
                    //    title: "Informasi!",
                    //    text: response.data.d,
                    //    type: "success"
                    //});
                 

                    var index;
                    var index2;


                    var val = response.data.d;
                   
                    index = $scope.master.findIndex(obj => obj[Key] === val);
                    
                  
                    index2 = $scope.mastetampung.findIndex(obj => obj[Key] === val);
                        
                   
                    $scope.master.splice(index, 1);
                    $scope.mastetampung.splice(index2, 1);

                    var index3 = tampungkdbarang.indexOf(response.data.d);

                    var index4 = uniqueArray.indexOf(response.data.d);
                   
                    tampungkdbarang.splice(index3, 1);
                    tampungdescruang.splice(index3, 1);
                    uniqueArray.splice(index4, 1);
                    uniqueArray2.splice(index4, 1);

                    if ($scope.activeId1 != null && $scope.activeId1 != '') {
                        if ($scope.activeId2 != null && $scope.activeId2 != '') {
                            var filtered = $scope.mastetampung.filter(function (item) {
                                var reg = new RegExp($scope.activeId1, 'g');
                                return item[filter1].match(reg) && item[filter2].match($scope.activeId2);
                            });
                        }
                        else {
                            var filtered = $scope.mastetampung.filter(function (item) {
                                var reg = new RegExp($scope.activeId1, 'g');
                                return item[filter1].match(reg);
                            });
                        }

                        $scope.master = new Array();
                        var jumlahdata;

                        jumlahdata = (loaddata * halaman);
                        if (jumlahdata < filtered.length) {
                            jumlahdata = (loaddata * halaman);
                        }
                        else {
                            jumlahdata = filtered.length;
                        }

                        for (var page = page1; page < jumlahdata; page++) {
                            $scope.master.push(filtered[page]);
                        }
						 TotalSeluruh = filtered.length;
					     document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                    }
                    else {
                        $scope.master = new Array();
                        var jumlahdata;


                        jumlahdata = (loaddata * halaman);
                        if (jumlahdata < $scope.mastetampung.length) {
                            jumlahdata = (loaddata * halaman);
                        }
                        else {
                            jumlahdata = $scope.mastetampung.length;
                        }
                        for (var page = page1; page < jumlahdata; page++) {
                            $scope.master.push($scope.mastetampung[page]);
                        }
						 TotalSeluruh = $scope.mastetampung.length;
					     document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                    }
                }
                else {
                    swal({
                        title: "Informasi!",
                        text: response.data.d,
                        type: "warning"
                    });
                }
            });
					  }
				  });
        };
        $scope.updateform = function clickform1(notif) {
            var data = "{";
            for (var i = 0; i < id1.length; i++) {
                if (i == id1.length - 1) {
                    var object;
                    if (document.getElementById(id1[i]) != null) {
                        object = document.getElementById(id1[i]).value;
                    }
                    else if(document.getElementsByName(id1[i])[0] != null) {
                        if (document.getElementsByName(id1[i])[0].type == 'checkbox') {
                            object = $("input[name='" + id1[i] + "']").val();
                        }
                        else if (document.getElementsByName(id1[i])[0].type == 'radio') {
                            object = $("input[name='" + id1[i] + "']:checked").val();
                        }
                    }
                    data += id1[i].replace(/'/g, "") + ':' + '\'' + object + '\'' + ',' + "tipe:'2'}";
                } else {
                    var object;
                    if (document.getElementById(id1[i]) != null) {
                        object = document.getElementById(id1[i]).value;
                    }
                    else if (document.getElementsByName(id1[i])[0] != null) {
                        if (document.getElementsByName(id1[i])[0].type == 'checkbox') {
                            object = $("input[name='" + id1[i] + "']").val();
                        }
                        else if (document.getElementsByName(id1[i])[0].type == 'radio') {
                            object = $("input[name='" + id1[i] + "']:checked").val();
                        }
                    }
                    data += id1[i].replace(/'/g, "") + ':' + '\'' + object + '\'' + ',';
                }
            }
            if (notif != '' && notif!=null) {
                swal({
                    title: "Informasi",
                    text: "Apakah anda yakin?"+notif+"",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    cancelButtonText: "Cancel",
                    closeOnConfirm: true,
                    closeOnCancel: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            $http({
                                url: url2,
                                method: "POST",
                                data: data,
                                headers: { 'Content-Type': 'application/json' }
                            }).then(function (response) {
                                var index;
                                var index2;


                                var val = JSON.parse(response.data.d)[Key];

                                index = $scope.master.findIndex(obj => obj[Key] === val);


                                index2 = $scope.mastetampung.findIndex(obj => obj[Key] === val);

                                $scope.master[index] = '';
                                $scope.mastetampung[index2] = '';

                                var index3 = tampungkdbarang.indexOf(JSON.parse(response.data.d)[barcode1]);

                                var index4 = uniqueArray.indexOf(JSON.parse(response.data.d)[barcode1]);


                                tampungkdbarang.splice(index3, 1);
                                tampungdescruang.splice(index3, 1);
                                uniqueArray.splice(index4, 1);
                                uniqueArray2.splice(index4, 1);

                                $scope.master[index] = JSON.parse(response.data.d);

                                $scope.mastetampung[index2] = JSON.parse(response.data.d);

                                tampungkdbarang[index3] = JSON.parse(response.data.d)[barcode1];
                                uniqueArray[index4] = JSON.parse(response.data.d)[barcode1];

                                if (barcode1 != barcode2) {
                                    tampungdescruang[index3] = JSON.parse(response.data.d)[barcode2];
                                    uniqueArray2[index4] = JSON.parse(response.data.d)[barcode2];
                                }
                                else {
                                    tampungdescruang[index3] = JSON.parse(response.data.d)[barcode1];
                                    uniqueArray2[index4] = JSON.parse(response.data.d)[barcode1];
                                }

                                if ($scope.activeId1 != null && $scope.activeId1 != '') {

                                    if ($scope.activeId2 != null && $scope.activeId2 != '') {
                                        var filtered = $scope.mastetampung.filter(function (item) {
                                            var reg = new RegExp($scope.activeId1, 'g');
                                            return item[filter1].match(reg) && item[filter2].match($scope.activeId2);
                                        });
                                    }
                                    else {
                                        var filtered = $scope.mastetampung.filter(function (item) {
                                            var reg = new RegExp($scope.activeId1, 'g');
                                            return item[filter1].match(reg);
                                        });
                                    }
                                    $scope.master = new Array();
                                    var jumlahdata;

                                    jumlahdata = (loaddata * halaman);
                                    if (jumlahdata < filtered.length) {
                                        jumlahdata = (loaddata * halaman);
                                    }
                                    else {
                                        jumlahdata = filtered.length;
                                    }

                                    for (var page = page1; page < jumlahdata; page++) {
                                        $scope.master.push(filtered[page]);
                                    }
                                    TotalSeluruh = filtered.length;
                                    document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                                }
                                else {
                                    $scope.master = new Array();
                                    var jumlahdata;


                                    jumlahdata = (loaddata * halaman);
                                    if (jumlahdata < $scope.mastetampung.length) {
                                        jumlahdata = (loaddata * halaman);
                                    }
                                    else {
                                        jumlahdata = $scope.mastetampung.length;
                                    }
                                    for (var page = page1; page < jumlahdata; page++) {
                                        $scope.master.push($scope.mastetampung[page]);
                                    }
                                    TotalSeluruh = $scope.mastetampung.length;
                                    document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                                }
                            });
                        }
                    });
            }
            else {
                swal({
                    title: "Informasi",
                    text: "Apakah anda yakin?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    cancelButtonText: "Cancel",
                    closeOnConfirm: true,
                    closeOnCancel: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            $http({
                                url: url2,
                                method: "POST",
                                data: data,
                                headers: { 'Content-Type': 'application/json' }
                            }).then(function (response) {
                                var index;
                                var index2;


                                var val = JSON.parse(response.data.d)[Key];

                                index = $scope.master.findIndex(obj => obj[Key] === val);


                                index2 = $scope.mastetampung.findIndex(obj => obj[Key] === val);

                                $scope.master[index] = '';
                                $scope.mastetampung[index2] = '';

                                var index3 = tampungkdbarang.indexOf(JSON.parse(response.data.d)[barcode1]);

                                var index4 = uniqueArray.indexOf(JSON.parse(response.data.d)[barcode1]);


                                tampungkdbarang.splice(index3, 1);
                                tampungdescruang.splice(index3, 1);
                                uniqueArray.splice(index4, 1);
                                uniqueArray2.splice(index4, 1);

                                $scope.master[index] = JSON.parse(response.data.d);

                                $scope.mastetampung[index2] = JSON.parse(response.data.d);

                                tampungkdbarang[index3] = JSON.parse(response.data.d)[barcode1];
                                uniqueArray[index4] = JSON.parse(response.data.d)[barcode1];

                                if (barcode1 != barcode2) {
                                    tampungdescruang[index3] = JSON.parse(response.data.d)[barcode2];
                                    uniqueArray2[index4] = JSON.parse(response.data.d)[barcode2];
                                }
                                else {
                                    tampungdescruang[index3] = JSON.parse(response.data.d)[barcode1];
                                    uniqueArray2[index4] = JSON.parse(response.data.d)[barcode1];
                                }

                                if ($scope.activeId1 != null && $scope.activeId1 != '') {
                                    var filtered = $scope.mastetampung.filter(function (item) {
                                        var reg = new RegExp($scope.activeId1, 'g');
                                        return item[filter1].match(reg);
                                    });
                                    $scope.master = new Array();
                                    var jumlahdata;

                                    jumlahdata = (loaddata * halaman);
                                    if (jumlahdata < filtered.length) {
                                        jumlahdata = (loaddata * halaman);
                                    }
                                    else {
                                        jumlahdata = filtered.length;
                                    }

                                    for (var page = page1; page < jumlahdata; page++) {
                                        $scope.master.push(filtered[page]);
                                    }
                                    TotalSeluruh = filtered.length;
                                    document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                                }
                                else {
                                    $scope.master = new Array();
                                    var jumlahdata;


                                    jumlahdata = (loaddata * halaman);
                                    if (jumlahdata < $scope.mastetampung.length) {
                                        jumlahdata = (loaddata * halaman);
                                    }
                                    else {
                                        jumlahdata = $scope.mastetampung.length;
                                    }
                                    for (var page = page1; page < jumlahdata; page++) {
                                        $scope.master.push($scope.mastetampung[page]);
                                    }
                                    TotalSeluruh = $scope.mastetampung.length;
                                    document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;
                                }
                            });
                        }
                    });
            }
        };
       
        $scope.save1 = function click5() { 
            var data = "{";
            for (var i = 0; i < id1.length; i++) {
                if (i == id1.length - 1) {
                    var object;
                    if (document.getElementById(id1[i]) != null) {
                         object = document.getElementById(id1[i]).value;
                    }
                    else if (document.getElementsByName(id1[i])[0] != null) {
                        if (document.getElementsByName(id1[i])[0].type == 'checkbox') {
                            object = $("input[name='" + id1[i] + "']").val();
                        }
                        else if (document.getElementsByName(id1[i])[0].type == 'radio') {
                            object = $("input[name='" + id1[i] + "']:checked").val();
                        }
                    }
                    data += id1[i].replace(/'/g, "") + ':' + '\'' + object + '\'' + ',' + "tipe:'1'}";
                } else {
                    var object;
                    if (document.getElementById(id1[i]) != null) {
                        object = document.getElementById(id1[i]).value;
                    }
                    else if (document.getElementsByName(id1[i])[0] != null) {
                        if (document.getElementsByName(id1[i])[0].type == 'checkbox') {
                            object = $("input[name='" + id1[i] + "']").val();
                        }
                        else if (document.getElementsByName(id1[i])[0].type == 'radio') {
                            object = $("input[name='" + id1[i] + "']:checked").val();
                        }
                    }
                    data += id1[i].replace(/'/g, "") + ':' + '\'' + object + '\'' + ',';
                }
            }
            $http({
                url: url2,
                method: "POST",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                $scope.mastetampung = new Array();
                $scope.master = new Array();

                $scope.mastetampung.push(JSON.parse(response.data.d));
                $scope.master.push(JSON.parse(response.data.d));

                $scope.pushAray(JSON.parse(response.data.d)[barcode1], JSON.parse(response.data.d)[barcode2]);

                $scope.activeId1 = $scope.mastetampung[0][filter1];

                if ($scope.activeId1 != null && $scope.activeId1 != '') {

                    if ($scope.activeId2 != null && $scope.activeId2 != '') {
                        var filtered = $scope.mastetampung.filter(function (item) {
                            var reg = new RegExp($scope.activeId1, 'g');
                            return item[filter1].match(reg) && item[filter2].match($scope.activeId2);
                        });
                    }
                    else {
                        var filtered = $scope.mastetampung.filter(function (item) {
                            var reg = new RegExp($scope.activeId1, 'g');
                            return item[filter1].match(reg);
                        });
                    }
                
                    $scope.master = new Array();
                    var jumlahdata;

                    jumlahdata = (loaddata * halaman);
                    if (jumlahdata < filtered.length) {
                        jumlahdata = (loaddata * halaman);
                    }
                    else {
                        jumlahdata = filtered.length;
                    }

                    for (var page = page1; page < jumlahdata; page++) {
                        $scope.master.push(filtered[page]);
                    }
                    TotalSeluruh = filtered.length;
					document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;

                    $scope.remove1();
                }
                else {
                    $scope.master = new Array();
                    var jumlahdata;


                    jumlahdata = (loaddata * halaman);
                    if (jumlahdata < $scope.mastetampung.length) {
                        jumlahdata = (loaddata * halaman);
                    }
                    else {
                        jumlahdata = $scope.mastetampung.length;
                    }
                    for (var page = page1; page < jumlahdata; page++) {
                        $scope.master.push($scope.mastetampung[page]);
                    }

                    TotalSeluruh = $scope.mastetampung.length;
					document.getElementById('tableinfo').innerHTML = 'Showing 1 to ' + $scope.master.length + ' of ' + TotalSeluruh;

                    $scope.remove1();

                  
                }
                });
        };
       
        $scope.remove1 = function click6() {
            if (document.getElementById('newtrheader') != null) {
                document.getElementById('newtrheader').remove();
            }
        };
        $scope.showtabledtl = function click8(row, colspan1, valId, filtercol) {
            if (angular.element('#showdtl_' + row).val() != '-') {

                angular.element('#angulardiv').append(angular.element('#Detail'));
                document.getElementById('Detail').style.display = 'none';
                angular.element('#trdetail').remove();
                angular.element('.expand').val('+');

                var tr = document.getElementById(row);
                var tr1 = document.createElement('tr');
                tr1.id = 'trdetail';
                var td = document.createElement('td');
                td.colSpan = colspan1;
                var div = document.getElementById('Detail');
                td.append(div);
                tr1.append(td);

                tr.parentNode.insertBefore(tr1, tr.nextSibling);
                angular.element('#showdtl_' + row).val('-');
                div.style.display = 'inline';
            
               
                $rootScope.activeId = angular.element('#' + valId + row).val();

                $rootScope.$emit('Callinit', {});

            }
            else {
                angular.element('#angulardiv').append(angular.element('#Detail'));
                document.getElementById('Detail').style.display = 'none';
                angular.element('#trdetail').remove();
                angular.element('#showdtl_' + row).val('+');

            }

        };
        $scope.showmodal1 = function click7(URL, object1, object2) {
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

        };
        
    });

}
app.controller('MyController', function ($scope, $http, $compile, $controller, $rootScope, DTOptionsBuilder) {
    if (urlddl != null) {
        $http({
            url: urlddl,
            method: "POST",
            data: {},
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $scope.masterdll = JSON.parse(response.data.d);
            $scope.myddl = $scope.masterdll[0]['filter'];
            $scope.activeId1 = $scope.myddl
          
            if (urlddl2 != null) {
            $http({
                url: urlddl2,
                method: "POST",
                data: {},
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                $scope.masterdll2 = JSON.parse(response.data.d);
                 $scope.activeId2 = $scope.myddl2; 
              });
            }
            initheader($scope, $http, $compile, $rootScope, DTOptionsBuilder);  
        });
    } else {
        initheader($scope, $http, $compile, $rootScope, DTOptionsBuilder);
      
    }
    
});
app.controller('MyControllerDTL', function ($scope, $http, $compile, $controller, $rootScope, DTOptionsBuilder) {
    if (document.getElementById('pagedtl') != null) {
        document.getElementById('pagedtl').value = halamandtl;
    }
    if (document.getElementById('txtsearchdtl') != null) {
        var search = document.getElementById('txtsearchdtl').value;

    }
    else {
        search = '';
    }
   
        $http({
            url: urldtl1,
            method: "POST",
            data: { "filter": search },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $scope.mastetampungdtl = JSON.parse(response.data.d);

            if ($scope.activeId1 != null && $scope.activeId1 != '') {
                var filtered = $scope.mastetampungdtl.filter(function (item) {
                    var reg = new RegExp($scope.activeId1, 'g');
                    return item[filter1].match(reg);
                });
                $scope.masterdtl = new Array();
                var jumlahdata;

                jumlahdata = (loaddatadtl * halamandtl);
                if (jumlahdata < filtered.length) {
                    jumlahdata = (loaddatadtl * halamandtl);
                }
                else {
                    jumlahdata = filtered.length;
                }
              
                for (var page = page1dtl; page < jumlahdata; page++) {
                    $scope.masterdtl.push(filtered[page]);
                }

                TotalSeluruh = filtered.length;
                document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
            }
            else {
                $scope.masterdtl = new Array();
                var jumlahdata;


                jumlahdata = (loaddatadtl * halamandtl);
                if (jumlahdata < $scope.mastetampungdtl.length) {
                    jumlahdata = (loaddatadtl * halamandtl);
                }
                else {
                    jumlahdata = $scope.mastetampungdtl.length;

                }

                for (var page = page1dtl; page < jumlahdata; page++) {
                    $scope.masterdtl.push($scope.mastetampungdtl[page]);

                }
             
                TotalSeluruh = $scope.mastetampungdtl.length;
                document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
            }
            $scope.nextprevdtl = function init1(val) {
                if (val == 'next') {


                    page1dtl += 10;

                    if ($scope.activeId1 != null && $scope.activeId1 != '') {
                        var filtered;
                        if ($scope.activeId2 != '' && $scope.activeId2 != null) {
                            filtered = $scope.mastetampungdtl.filter(function (item) {
                                var reg = new RegExp($scope.activeId1, 'g');

                                return item[filter1].match(reg) && item[filter2].match($scope.activeId2);
                            });
                        }
                        else {
                            filtered = $scope.mastetampungdtl.filter(function (item) {
                                var reg = new RegExp($scope.activeId1, 'g');
                                return item[filter1].match(reg);
                            });
                        }

                        if (page1dtl < filtered.length) {
                            $('#loading').show();
                            halamandtl += 1;

                            $scope.masterdtl = new Array();
                            var jumlahdata;

                            jumlahdata = (loaddatadtl * halamandtl);
                            if (jumlahdata < filtered.length) {
                                jumlahdata = (loaddatadtl * halamandtl);
                            }
                            else {
                                jumlahdata = filtered.length;
                            }

                            for (var page = page1dtl; page < jumlahdata; page++) {
                                $scope.masterdtl.push(filtered[page]);
                            }
                            document.getElementById('pagedtl').value = halamandtl;
                        }
                        else {
                            $('#loading').hide();
                            page1dtl -= 10;
                        }
                        TotalSeluruh = filtered.length;
                        document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
                    }
                    else {
                        if (page1dtl < $scope.mastetampungdtl.length) {
                            $('#loading').show();
                            halamandtl += 1;
                            $scope.masterdtl = new Array();
                            var jumlahdata;


                            jumlahdata = (loaddatadtl * halamandtl);
                            if (jumlahdata < $scope.mastetampungdtl.length) {
                                jumlahdata = (loaddatadtl * halamandtl);
                            }
                            else {
                                jumlahdata = $scope.mastetampungdtl.length;
                            }
                            for (var page = page1dtl; page < jumlahdata; page++) {
                                $scope.masterdtl.push($scope.mastetampungdtl[page]);
                            }
                            document.getElementById('pagedtl').value = halamandtl;
                        }
                        else {
                            $('#loading').hide();
                            page1dtl -= 10;
                        }
                        TotalSeluruh = $scope.mastetampungdtl.length;
                        document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
                    }

                }
                else if (val == 'prev') {
                    if (halamandtl != 1) {
                        $('#loading').show();
                        halamandtl -= 1;
                        page1dtl -= 10;


                        //delete $scope.master;
                        if ($scope.activeId1 != null && $scope.activeId1 != '') {
                            var filtered;
                            if ($scope.activeId2 != '' && $scope.activeId2 != null) {
                                filtered = $scope.mastetampungdtl.filter(function (item) {
                                    var reg = new RegExp($scope.activeId1, 'g');
                                    return item[filter1].match(reg) && item.kodebarang.match($scope.activeId2);
                                });
                            }
                            else {
                                filtered = $scope.mastetampungdtl.filter(function (item) {
                                    var reg = new RegExp($scope.activeId1, 'g');
                                    return item[filter1].match(reg);
                                });
                            }

                            $scope.masterdtl = new Array();
                            var jumlahdata;

                            jumlahdata = (loaddatadtl * halamandtl);

                            for (var page = page1dtl; page < jumlahdata; page++) {
                                $scope.masterdtl.push(filtered[page]);
                            }
                            TotalSeluruh = filtered.length;
                            document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
                        }
                        else {
                            $scope.masterdtl = new Array();
                            var jumlahdata;


                            jumlahdata = (loaddatadtl * halamandtl);

                            for (var page = page1dtl; page < jumlahdata; page++) {
                                $scope.masterdtl.push($scope.mastetampungdtl[page]);
                            }
                            TotalSeluruh = $scope.mastetampungdtl.length;
                            document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
                        }
                        document.getElementById('pagedtl').value = halamandtl;
                    }


                }

            };
            $rootScope.$on("Callreloaddtl", function () {
                $scope.reloaddtl();
            });
            $scope.reloaddtl = function reloaddata() {
                $http({
                    url: urldtl1,
                    method: "POST",
                    data: { "filter": search },
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (response) {
                    $scope.mastetampungdtl = JSON.parse(response.data.d);

                    if ($scope.activeId1 != null && $scope.activeId1 != '') {
                        var filtered = $scope.mastetampungdtl.filter(function (item) {
                            var reg = new RegExp($scope.activeId1, 'g');
                            return item[filter1].match(reg);
                        });
                        $scope.masterdtl = new Array();
                        var jumlahdata;

                        jumlahdata = (loaddatadtl * halamandtl);
                        if (jumlahdata < filtered.length) {
                            jumlahdata = (loaddatadtl * halamandtl);
                        }
                        else {
                            jumlahdata = filtered.length;
                        }

                        for (var page = page1dtl; page < jumlahdata; page++) {
                            $scope.masterdtl.push(filtered[page]);
                        }

                       
                        TotalSeluruh = filtered.length;
                        document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
                    }
                    else {
                        $scope.masterdtl = new Array();
                        var jumlahdata;


                        jumlahdata = (loaddatadtl * halamandtl);
                        if (jumlahdata < $scope.mastetampungdtl.length) {
                            jumlahdata = (loaddatadtl * halamandtl);
                        }
                        else {
                            jumlahdata = $scope.mastetampungdtl.length;

                        }

                        for (var page = page1dtl; page < jumlahdata; page++) {
                            $scope.masterdtl.push($scope.mastetampungdtl[page]);

                        }
                    
                        TotalSeluruh = $scope.mastetampungdtl.length;
                        document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
                    }
                });
            };
            $scope.searchdtl1 = function search1(row) {
                $('#loading').show();
                txtsearch = document.getElementById('txtsearchdtl').value;
                if (txtsearch.length >= 3) {
                        $scope.activeId1 = txtsearch;

                        $http({
                            url: urldtl1,
                            method: "POST",
                            data: { "filter": txtsearch },
                            headers: { 'Content-Type': 'application/json' }
                        }).then(function (response) {

                            $scope.mastetampungdtl = JSON.parse(response.data.d);

                            $scope.masterdtl = new Array();
                            var jumlahdata;

                            jumlahdata = (loaddatadtl * halamandtl);
                            if (jumlahdata < $scope.mastetampungdtl.length) {
                                jumlahdata = (loaddatadtl * halamandtl);
                            }
                            else {
                                jumlahdata = $scope.mastetampungdtl.length;
                            }
                            for (var page = page1dtl; page < jumlahdata; page++) {
                                $scope.masterdtl.push($scope.mastetampungdtl[page]);
                            }
                            TotalSeluruh = $scope.mastetampungdtl.length;
                            document.getElementById('tableinfodtl').innerHTML = 'Showing 1 to ' + $scope.masterdtl.length + ' of ' + TotalSeluruh;
                            document.getElementById('pagedtl').value = halamandtl;
                            if ($scope.masterdtl.length == 0) {
                                $('#loading').hide();
                            }
                        });
                }
                else {
                    $('#loading').hide();
                    alert('Minimal 3 karakter!');
                }

            };
        });
      
});

app.controller('MyControllerForm1', function ($scope, $http, $compile, $rootScope) {
    $scope.saveForm = function clickform() {
        var a = document.getElementById('FormInput');
        a.style.display = 'none';
        if (document.getElementById('Detail') != null) {
            angular.element('#angulardiv').append(angular.element('#Detail'));
            document.getElementById('Detail').style.display = 'none';
            angular.element('#trdetail').remove();
            angular.element('.expand').val('+');
        }
        $rootScope.$emit("Callsave1", {});
      
    };
    $scope.updateform1 = function clickform2(notif) {
        var a = document.getElementById('FormInput');
        a.style.display = 'none';
        if (document.getElementById('Detail') != null) {
            angular.element('#angulardiv').append(angular.element('#Detail'));
            document.getElementById('Detail').style.display = 'none';
            angular.element('#trdetail').remove();
            angular.element('.expand').val('+');
        }
        $rootScope.$emit("Callupdateform", notif);
        if (document.getElementById('NewForm') != null) {
            document.getElementById('NewForm').style.display = '';
        }
        
        $(".disableform").removeAttr('disabled');
    };
    $scope.setchecked = function clickform3(element) {
        var a = false;

        var b = element;
            if (b.checked) {

                a = true;
                b.value = true;
            }
            else {

                a = false;
                b.value = false;
            }
        
        return a;
    };
});