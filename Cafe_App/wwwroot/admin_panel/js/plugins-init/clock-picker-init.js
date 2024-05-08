(function($) {
    "use strict"

    // Clock pickers
    var input = $('#single-input').clockpicker({
        placement: 'bottom',
        align: 'left',
        autoclose: true,
        'default': 'now'
    });

    $('.clockpicker').clockpicker({
        donetext: 'Onayla',
    }).find('input').change(function () {
        console.log(this.value);
    });

    $('#check-minutes').click(function (e) {
        // Have to stop propagation here
        e.stopPropagation();
        input.clockpicker('show').clockpicker('toggleView', 'minutes');
    });

})(jQuery)