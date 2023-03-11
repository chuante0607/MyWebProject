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

async function getData(url) {
    const response = await fetch(url);
    if (response.ok) {
        const result = await response.json();
        return result;
    }
    throw new Error('Network response was not ok.');
}
