
var table;

function TestDataTablesAdd(divTable, obj) {

    $(divTable).html('<div class="spinner-border spinner-border-sm"></div><span>Đang lấy dữ liệu</span>')


    $(divTable).html('<table id="ListTable" class="table table-striped table-bordered text-center" style="width:100%"></table >')
            table = $('#ListTable').DataTable({
                destroy: true,
                data: obj,
                columns: [
                    { data: 'stt', title: 'Số thứ tự' },
                    { data: 'hoTen', title: 'Họ tên' },
                    {
                        title:'Thao tác',
                        data: null,
                        render: function (data) {
                            return `<a class="btn btn-info btn-sm active" title="Khám bệnh"> <i class="fas fa-stethoscope" aria-hidden="true"></i></a>
                            <a class="btn btn-info btn-sm active" title="Huỷ bỏ"> <i class="fas fa-close" aria-hidden="true"></i></a>`;
                        }
                    },
                    {
                        visible: false,
                        title:'uuTien',
                        data: null,
                        render: function (data) {
                            return data.uuTien;
                        }
                    },
                    {
                        visible: false,
                        title:'Khongdau',
                        data: null,
                        render: function (data) {
                            return accents_supr(data.hoTen);
                        }
                    }


                ],

                "order": [[3, "asc"], [0, "asc"]],
                "language": {
                    "lengthMenu": "Hiển thị _MENU_ kết quả mỗi trang",
                    "zeroRecords": "Không có kết quả",
                    "info": "Trang: _PAGE_ / _PAGES_",
                    "infoEmpty": "Không có yêu cầu nào",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "search": "Tìm: ",
                    "paginate": {
                        "first": "Trang đầu",
                        "last": "Trang cuối",
                        "next": ">",
                        "previous": "<"
                    },
                }
               
               


                

            });


   

}
function AddPhieuKham(obj) {
    toastr.success("Có một bệnh nhân mới");
    console.log(accents_supr(obj.hoTen))
    table.row.add({
        'stt': obj.stt,
        'hoTen': obj.hoTen,
        'Thao tác': `<a onclick =Edit('${obj.maPK}') class='btn btn-info btn-sm btn-sm active'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a> <a class='btn btn-info btn-sm active'> <i class='fa fa-trash'></i></a> <a onclick = Detail('${obj.maPK}')  class='btn btn-info btn-sm active' title = 'Thông tin' > <i class='fa fa-info-circle' aria-hidden='true'></i></a>`,
        'uuTien': obj.uuTien,

        'Khongdau': accents_supr(obj.hoTen),

    }).draw(false);
    //$("#ListTable > tbody").append(`<tr>
    //    <td>${obj.stt}</td>
    //    <td>${obj.hoTen}</td>
    //    <td>${obj.uuTien}</td>
    //    <td>${accents_supr(obj.hoTen)}</td>
    //    <td><a class="btn btn-info btn-sm active" title="Khám bệnh"> <i class="fas fa-stethoscope" aria-hidden="true"></i></a><a class="btn btn-info btn-sm active" title="Huỷ bỏ"> <i class="fas fa-close" aria-hidden="true"></i></a></td></tr>`);
    //$('#ListTable').dataTable().fnClearTable();
    //$('#ListTable').DataTable({
    //    destroy: true,
    //    data: obj,
    //    columns: [
    //        { data: 'stt', title: 'Số thứ tự' },
    //        { data: 'hoTen', title: 'Họ tên' },
    //        {
    //            title: 'Thao tác',
    //            data: null,
    //            render: function (data) {
    //                return `<a class="btn btn-info btn-sm active" title="Khám bệnh"> <i class="fas fa-stethoscope" aria-hidden="true"></i></a>
    //                        <a class="btn btn-info btn-sm active" title="Huỷ bỏ"> <i class="fas fa-close" aria-hidden="true"></i></a>`;
    //            }
    //        },
    //        {
    //            visible: false,
    //            title: 'Ma uu tien',
    //            data: null,
    //            render: function (data) {
    //                return data.uuTien;
    //            }
    //        },
    //        {
    //            visible: false,
    //            title: 'Khong dau',
    //            data: null,
    //            render: function (data) {
    //                return accents_supr(data.hoTen);
    //            }
    //        }


    //    ],

    //    "order": [[3, "asc"], [0, "asc"]],
    //    "language": {
    //        "lengthMenu": "Hiển thị _MENU_ kết quả mỗi trang",
    //        "zeroRecords": "Không có kết quả",
    //        "info": "Trang: _PAGE_ / _PAGES_",
    //        "infoEmpty": "Không có yêu cầu nào",
    //        "infoFiltered": "(filtered from _MAX_ total records)",
    //        "search": "Tìm: ",
    //        "paginate": {
    //            "first": "Trang đầu",
    //            "last": "Trang cuối",
    //            "next": ">",
    //            "previous": "<"
    //        },
    //    }

    //});


}