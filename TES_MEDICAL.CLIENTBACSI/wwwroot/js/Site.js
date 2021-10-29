
var table;
var dotnetObject;
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
                            return `<a class="btn btn-info btn-sm active" title="Khám bệnh" href="/KhamBenh/${data.maPK}"> <i class="fas fa-stethoscope" aria-hidden="true"></i></a>
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


function loadDatatableThuoc(id, obj, dotNetO) {
    dotnetObject = dotNetO;
    $(id).DataTable({
        destroy: true,
        data: obj,
        columns: [
            { data: 'tenThuoc', title: 'Tên thuốc' },
           
            {
                title: 'Thao tác',
                data: null,
                render: function (data) {
                    return `<button class="btn btn-success btn-sm" style="border-radius: 60px;" onclick="Hello(${data.maThuoc})"><i class="fas fa-plus"></i></button>`;
                }
            }
        ],
        columnDefs: [
            {
                targets: 0,
                className: 'col-8'
            },
            {
                targets: 1,
                className: 'col-4 text-center'
            },

        ],

        "order": [0, "asc"],
        "language": {
            "lengthMenu": "Hiển thị _MENU_ kết quả mỗi trang",
            "zeroRecords": "Không có kết quả",
            "info": "Trang: _PAGE_ / _PAGES_",
            "infoEmpty": "Không có thuốc",
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


function showModal(id) {
    $('#' + id).modal('show');
}

function hideModal(id) {
    $('body').removeAttr("style")
    $('body').removeClass("modal-open")

    $(".modal-backdrop").remove()
    $('#' + id).modal('hide');


}

//function ChangeModal(a, b) {
//    $('#' + a).modal('hide');
//    $('#' + b).modal('show');
//}
function Success(DotNet) {

    let timerInterval
    Swal.fire({
        title: 'Đăng ký  thành công',

        html: '<progress value="0" max="3" id="progressBar"></progress>',
        timer: 3000,
        timerProgressBar: true,
        didOpen: () => {
            Swal.showLoading()
            timerInterval = setInterval(() => {
                const content = Swal.getHtmlContainer()
                if (content) {
                    const b = content.querySelector('#progressBar')
                    if (b) {
                        b.value = 3 - Math.floor(Swal.getTimerLeft() / 1000)
                    }
                }
            }, 100)
        },
        willClose: () => {
            clearInterval(timerInterval)
            DotNet.invokeMethodAsync("RedirectTo", "index");
        }
    }).then((result) => {

        if (result.dismiss === Swal.DismissReason.timer) {
            DotNet.invokeMethodAsync("RedirectTo", "index");

        }
    })



}


function Fail() {
    Swal.fire({
        icon: 'error',
        title: 'Đăng ký thất bại',
        text: 'vui lòng kiểm tra lại hoặc liên hệ Admin',

    })
}

function SuccessNotifi(message) {
    Swal.fire({
        position: 'Center',
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 1500
    })
}
