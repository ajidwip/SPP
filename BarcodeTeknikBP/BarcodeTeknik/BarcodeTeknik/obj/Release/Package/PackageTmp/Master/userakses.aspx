<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="userakses.aspx.cs" Inherits="BarcodeTeknik.Master.userakses" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        function save1() {

            var object3 = document.getElementById('owner');

            if (object3.value != '') {
                var head = document.getElementsByClassName('cbhead');
                var rowhead = head.length - 1;

                var i = 0;
                //alert(rowhead);
                for (var j = 0; j <= rowhead; j++) {
                    var object = document.getElementById('cbhead_' + j);

                    if (object.checked) {
                        var object2 = document.getElementById('lblhead_' + j);
                        var data = "{user:'" + object3.value + "',menuid:'" + object2.innerHTML + "',row:'" + j + "',rowchild:'0'}";
                        $.ajax(
                            {
                                type: "POST",
                                data: data,
                                url: '<%=ResolveUrl("~/Master/userakses.aspx/save") %>',
                                contentType: "application/json",
                                dataType: "json",
                                success: function (response) {
                                    //  alert('Berhasil Grant User');
                                },
                                error: function (response) {
                                    alert("error");
                                }
                            });
                        var object1 = document.getElementsByClassName('cbchild' + j);
                        var rowchild = object1.length - 1;
                        for (i = 0; i < object1.length; i++) {
                            if (object1[i].checked) {
                                var object2 = document.getElementById('lblchild' + j + '_' + i);
                                var data = "{user:'" + object3.value + "',menuid:'" + object2.innerHTML + "',row:'" + j + "',rowchild:'" + i + "'}";
                                $.ajax(
                                    {
                                        type: "POST",
                                        data: data,
                                        url: '<%=ResolveUrl("~/Master/userakses.aspx/save") %>',
                                        contentType: "application/json",
                                        dataType: "json",
                                        success: function (response) {
                                            var a = response.d.split(',')
                                            if (a[0] == rowhead) {
                                                if (a[1] == rowchild) {
                                                    alert('Berhasil Grant User');
                                                }
                                            }
                                            //
                                        },
                                        error: function (response) {
                                            alert("error");
                                        }
                                    });
                            }
                            else {
                                var object2 = document.getElementById('lblchild' + j + '_' + i);
                                var data = "{user:'" + object3.value + "',menuid:'" + object2.innerHTML + "',row:'" + j + "',rowchild:'" + i + "'}";
                                $.ajax(
                                    {
                                        type: "POST",
                                        data: data,
                                        url: '<%=ResolveUrl("~/Master/userakses.aspx/delete") %>',
                                        contentType: "application/json",
                                        dataType: "json",
                                        success: function (response) {
                                            var a = response.d.split(',')
                                            if (a[0] == rowhead) {
                                                if (a[1] == rowchild) {
                                                    alert('Berhasil Grant User');
                                                }
                                            }
                                            //
                                        },
                                        error: function (response) {
                                            alert("error");
                                        }
                                    });
                                if (j == rowhead) {
                                    if (i == rowchild) {
                                        alert('Berhasil Grant User');
                                    }
                                }
                            }
                        }

                    }
                    else{
                        var object2 = document.getElementById('lblhead_' + j);
                        var data = "{user:'" + object3.value + "',menuid:'" + object2.innerHTML + "',row:'" + j + "',rowchild:'0'}";
                        $.ajax(
                            {
                                type: "POST",
                                data: data,
                                url: '<%=ResolveUrl("~/Master/userakses.aspx/delete") %>',
                                contentType: "application/json",
                                dataType: "json",
                                success: function (response) {
                                    //  alert('Berhasil Grant User');
                                },
                                error: function (response) {
                                    alert("error");
                                }
                            });
                        var object1 = document.getElementsByClassName('cbchild' + j);
                        var rowchild = object1.length - 1;
                        for (i = 0; i < object1.length; i++) {
                           
                                var object2 = document.getElementById('lblchild' + j + '_' + i);
                                var data = "{user:'" + object3.value + "',menuid:'" + object2.innerHTML + "',row:'" + j + "',rowchild:'" + i + "'}";
                                $.ajax(
                                    {
                                        type: "POST",
                                        data: data,
                                        url: '<%=ResolveUrl("~/Master/userakses.aspx/delete") %>',
                                        contentType: "application/json",
                                        dataType: "json",
                                        success: function (response) {
                                            var a = response.d.split(',')
                                            if (a[0] == rowhead) {
                                                if (a[1] == rowchild) {
                                                    alert('Berhasil Grant User');
                                                }
                                            }
                                            //
                                        },
                                        error: function (response) {
                                            alert("error");
                                        }
                                    });
                            
                          
                                if (j == rowhead) {
                                    if (i == rowchild) {
                                        alert('Berhasil Grant User');
                                    }
                                
                            }
                        }

                    }
               <%--     else {
                        var object1 = document.getElementsByClassName('cbchild' + j);
                        var rowchild = object1.length - 1;
                        for (i = 0; i < object1.length; i++) {
                            if (object1[i].checked) {
                                var object2 = document.getElementById('lblchild' + j + '_' + i);
                                var data = "{user:'" + object3.value + "',menuid:'" + object2.innerHTML + "',row:'" + j + "',rowchild:'" + i + "'}";
                                $.ajax(
                                    {
                                        type: "POST",
                                        data: data,
                                        url: '<%=ResolveUrl("~/Master/userakses.aspx/save") %>',
                                        contentType: "application/json",
                                        dataType: "json",
                                        success: function (response) {
                                            var a = response.d.split(',')
                                            if (a[0] == rowhead) {
                                                if (a[1] == rowchild) {
                                                    alert('Berhasil Grant User');
                                                }
                                            }
                                        },
                                        error: function (response) {
                                            alert("error");
                                        }
                                    });
                            }
                            else {
                                if (j == rowhead) {
                                    if (i == rowchild) {
                                        alert('Berhasil Grant User');
                                    }
                                }
                            }
                        }
                    }--%>
                }

            }
            else {
                object3.style.border = '1px solid #FF0000';
            }

        }

        function checked1(row) {
            var object = document.getElementById('cbhead_' + row);
            var object1 = document.getElementById('cbchild' + row + '_' + row);
            if (object.checked) {
                var object1 = document.getElementsByClassName('cbchild' + row);
                for (var i = 0; i < object1.length; i++) {
                    object1[i].checked = true;
                    object1[i].disabled  = false;
                }

            }
            else {
                var object1 = document.getElementsByClassName('cbchild' + row);
                for (var i = 0; i < object1.length; i++) {
                    object1[i].checked = false;
                    object1[i].disabled  = true;
                }
            }
        }

        function getmenuchecked() {
            var object3 = document.getElementById('owner');
            var data = "{Owner:'" + object3.value + "'}";
            $.ajax(
                {
                    type: "POST",
                    data: data,
                    url: '<%=ResolveUrl("~/Master/userakses.aspx/GetMenu") %>',
                    contentType: "application/json",
                    dataType: "json",
                    success: function (response) {
                        var cbhead2 = document.getElementsByClassName('cbhead');
                        for (var l = 0; l < cbhead2.length; l++) {
                            cbhead2[l].checked = false;
                            var cbchild2 = document.getElementsByClassName('cbchild' + l);
                            for (var m = 0; m < cbchild2.length; m++) {
                                cbchild2[m].checked = false;
                            }
                        }

                        var cbhead = document.getElementsByClassName('cbhead');
                        for (var j = 0; j < cbhead.length; j++) {
                            for (var i = 0; i < response.d.length; i++) {
                                var object = document.getElementById('cbhead_' + j);
                                var object1 = document.getElementById('lblhead_' + j);
                                if (response.d[i] == object1.innerHTML) {
                                    object.checked = true;
                                }
                                else {
                                    var cbchild = document.getElementsByClassName('cbchild' + j);
                                    for (var k = 0; k < cbchild.length; k++) {
                                        var objecta = document.getElementById('cbchild' + j + '_' + k);
                                        var objectb = document.getElementById('lblchild' + j + '_' + k);
                                        if (response.d[i] == objectb.innerHTML) {
                                            objecta.checked = true;
                                        }
                                    }
                                }
                            }
                        }

                    },
                    error: function (response) {
                        alert("error");
                    }
                });
        }
    </script>
       <input type="submit" style="margin-left:20px;margin-top:20px" class="btn btn-w-m btn-danger" value="Save" onclick="save1();" />
        <div style="background-color:white;overflow-y: scroll;height:600px;padding-top:10px;margin-top:20px;margin-bottom:50px">
           <label style="margin-left:38px" >User Id</label> <input id="popup6" type="submit" value='...' 
               onclick="showmodal('<%=ResolveUrl("~/Master/userakses.aspx/GetOwner") %>','owner','undefined');" 
               style="margin-left:10px" /><input type='text' id='owner' class="change1" onchange="getmenuchecked();" style='width:150px;margin-left:10px'/>
            <table>
                <tr>
                    <td>
                          <ul>
                                  <% 
                                      int row = 0;
                                      for (c = 0; c < dt.Rows.Count; c++)
                                      {
                                           row = c;
                                          row2 = row;
                                          %>
                                    <li style="list-style-type: none;">
                                       <input type="checkbox" class="cbhead" id="cbhead_<%Response.Write(row.ToString());%>" onclick="checked1(<%Response.Write(row.ToString());%>);"/> <%Response.Write(dt.Rows[c][0].ToString());%><label style="display:none" id="lblhead_<%Response.Write(row.ToString());%>"><%Response.Write(dt.Rows[c][1].ToString());%></label>
                                          <ul>
                                        <%  result = dt2.Select("[Parent]='" + dt.Rows[c][1] + "'");
                          
                                            for (b = 0; b < result.Length; b++)
                                            {
                                                   int row1 = b;
                                                %>
                        
                                            <li style="list-style-type: none;"><input type="checkbox" class="cbchild<%Response.Write(row.ToString());%>" id="cbchild<%Response.Write(row.ToString());%>_<%Response.Write(row1.ToString());%>"/> <%Response.Write(result[b][0]);%><label style="display:none" id="lblchild<%Response.Write(row.ToString());%>_<%Response.Write(row1.ToString());%>"><%Response.Write(result[b][3].ToString());%></label></li>
                        
                                        <%  }%>    
                                              </ul>
                                    </li>

                                    <%  } %>
                            </ul>
                    </td>
                  
                </tr>

            </table>
        </div>
  </asp:Content>
