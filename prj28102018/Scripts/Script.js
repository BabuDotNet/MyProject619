/// <reference path="jtable/jquery.jtable.js" />
$(document).ready(function () {
    $('#MarksTable').jtable({
        title: 'Marks Detail',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'RollNumber ASC',

        actions: {
            listAction: '/Home/GetEmpDetails',
            //createAction: '/Marks/Create',
            //updateAction: '/Marks/Edit',
            //deleteAction: '/Marks/Delete'
        },
        fields: {
            EmployeeID: {
                key: true,
                list: false
            },
            EmpName: {
                title: 'Emp Name',
                width: '15%'
            },
            EmpPhone: {
                title: 'Emp Phone',
                width: '45%'
            },
            EmailID: {
                title: 'Email ID',
                width: '15%'
            },
            EmpImg: {
                title: 'Img Url',
                width: '15%',
                sorting: false
            },
            EmpAdrs: {
                title: 'Address',
                width: '15%',
            }
        }
    });
    $('#MarksTable').jtable('load');
});
