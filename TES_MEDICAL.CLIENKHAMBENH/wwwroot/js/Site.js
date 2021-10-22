function TestDataTablesAdd(table, obj) {
   
    $('#ListPK').html('<div class="spinner-border spinner-border-sm"></div><span>Đang lấy dữ liệu</span>')

   
            $('#listLich').html('<table id="ListTable" class="table table-striped table-bordered text-center" style="width:100%"></table >')
            table = $('#ListTable').DataTable({
                destroy: true,
                data: result,
                columns: [
                    { data: 'TenBN', title: 'Tên bệnh nhân' },
                    { data: 'NgaySinh', title: 'Ngày sinh' },
                    { data: 'SDT', title: 'Số điện thoại' },
                    { data: 'NgayKham', title: 'Ngày khám' },
                    {
                        title: 'Thao tác',
                        data: null,
                        render: function (data) {
                            return `<a onclick =Edit('${data.MaPhieu}') class='btn btn-info btn-sm btn-sm active'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a> <a class='btn btn-info btn-sm active'> <i class='fa fa-trash'></i></a> <a onclick = Detail('${data.MaPhieu}')  class='btn btn-info btn-sm active' title = 'Thông tin' > <i class='fa fa-info-circle' aria-hidden='true'></i></a>`;
                        }
                    },
                    {
                        visible: false,
                        title: 'Khong dau',
                        data: null,
                        render: function (data) {
                            return accents_supr(data.TenBN);
                        }
                    }

                ],
                "order": [[3, "asc"]],
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
                },
                columnDefs: [
                    {
                        targets: 1,
                        render: $.fn.dataTable.render.moment("YYYY-MM-DDTHH:mm:ss.sssZ", 'DD/MM/YYYY')
                    },
                    {
                        targets: 3,
                        render: $.fn.dataTable.render.moment("YYYY-MM-DDTHH:mm:ss.sssZ", 'DD/MM/YYYY HH:mm')
                    }


                ]

            });

      

}