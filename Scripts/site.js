var color = ['primary', 'success', 'warning', 'danger', 'info', 'secondary', 'primary', 'success', 'warning', 'danger', 'info', 'secondary'];

function wAlert(title) {
    Swal.fire({
        title,
        icon: 'warning',
        confirmButtonText: '確認',
    })
}

function eAlert(title) {
    Swal.fire({
        title,
        icon: 'error',
        confirmButtonText: '確認',
        allowEnterKey: false,
        allowOutsideClick: false,
    })
}

function sAlert(title) {
    Swal.fire({
        title,
        icon: 'success',
        confirmButtonText: '確認',
        allowEnterKey: false,
        allowOutsideClick: false,
    })
}

