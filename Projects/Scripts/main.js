

$(function () {

    $('.date-picker').datetimepicker({
        format: 'YYYY/MM/DD'
    });
    $('select').chosen({ width: '100%', disable_search_threshold: 10 });
}
);