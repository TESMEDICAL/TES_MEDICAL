
var table;

function TestDataTablesAdd(table, obj) {
   
    $(table).html('<div class="spinner-border spinner-border-sm"></div><span>Đang lấy dữ liệu</span>')


    $(table).html('<table id="ListTable" class="table table-striped table-bordered text-center" style="width:100%"></table >')
            table = $('#ListTable').DataTable({
                destroy: true,
                data: obj,
                columns: [
                    { data: 'stt', title: 'Số thứ tự' },
                    { data: 'hoTen', title: 'Họ tên' },
                    {
                        title: 'Thao tác',
                        data: null,
                        render: function (data) {
                            return `<a class="btn btn-info btn-sm active" title="Khám bệnh"> <i class="fas fa-stethoscope" aria-hidden="true"></i></a>
                            <a class="btn btn-info btn-sm active" title="Huỷ bỏ"> <i class="fas fa-close" aria-hidden="true"></i></a>`;
                        }
                    },
                    {
                        visible: false,
                        title: 'Ma ưu tiên',
                        data: null,
                        render: function (data) {
                            return data.uuTien;
                        }
                    },
                    {
                        visible: false,
                        title: 'Khong dau',
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