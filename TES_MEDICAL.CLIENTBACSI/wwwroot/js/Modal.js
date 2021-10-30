function showModal(id) {
    $('#' + id).modal('show');
}

function hideModal(id) {
    $('body').removeAttr("style")
    $('body').removeClass("modal-open")

    $(".modal-backdrop").remove()
    $('#'+id).modal('hide');
 
    
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
            DotNet.invokeMethodAsync( "RedirectTo", "index");
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
