

$(function () {

    $('.date-picker').datetimepicker({
        format: 'YYYY/MM/DD'
    });
    $('select').chosen({ width: '100%', disable_search_threshold: 10 });

    if ($('.notify-number').html() != "0")
    {
        $('.notify-number').fadeIn('slow', function () {
            $(this).fadeOut('slow', function () {
                $(this).fadeIn('slow');
            })
        });
    }

    $('.menu-toggle').on('click', function () {
        $('.menu').toggleClass('overlay');

    })

    $('body').on('click', '.overlay', function () {
        $('.overlay').removeClass('overlay');

    })

    $('.all-task').on('click', function () {
        $('.task-panel').fadeIn();
    })

    $('.low-task').on('click', function () {


        $('.task-panel.normal').hide();
        $('.task-panel.high').hide();
        $('.task-panel.low').show();
    });

    $('.normal-task').on('click', function () {


        $('.task-panel.low').hide();
        $('.task-panel.high').hide();
        $('.task-panel.normal').show();
    });

    $('.high-task').on('click', function () {


        $('.task-panel.normal').hide();
        $('.task-panel.low').hide();
        $('.task-panel.high').show();
    })
}
);