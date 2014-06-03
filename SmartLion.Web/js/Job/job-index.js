$('.refresh').on('click', 'a', function (e) {
    var url = '/Job/List?PageIndex=1';
    $("#search").val("")
    $.get(url, function (data) {
        var htmlresponse = '';
        var htmlpagination = '';
        var firstindex = parseInt($(".previous").attr("first-index"));
        var lastindex = parseInt($(".next").attr("last-index"));
        var totalpages = parseInt($(".previous").attr("total-pages"));

        $.each(data.Result, function (i, job) {
            htmlresponse += '<tr>';
            htmlresponse += '<td><a href="/Job/Edit/' + job.Id + '"><span class="glyphicon glyphicon-edit"></span></a></td>';
            htmlresponse += '<td>' + job.Name + '</td>';
            htmlresponse += '<td><a href="#" class="delete" param="' + job.Id + '"><span class="glyphicon glyphicon-remove"></span></a></td>';
            htmlresponse += '</tr>';
        });
        $(".table #grid").html(htmlresponse);
        htmlpagination += '<li><a href="#" class="previous" first-index="1" total-pages="' + data.TotalPages + '">&laquo;</a></li>';
        for (var i = 1; i <= (data.TotalPages > 10 ? 10 : data.TotalPages); i++) {
            if (i == 1) {
                htmlpagination += '<li class="active"><span>' + i + '<span class="sr-only">(current)</span></span></li>';
            }
            else {
                htmlpagination += '<li><a href="#" class="index">' + i + '</a></li>';
            }
            lastindex = i;
        }
        htmlpagination += '<li><a href="#" class="next" last-index="' + lastindex + '">&raquo;</a></li>';
        $(".pagination").html(htmlpagination);
    });
    e.preventDefault();
    return false;
});

$("#btnsearch").on('click', function (e) {
    var url = '/Job/List?PageIndex=1';

    if ($("#search").val().length > 0) {
        url += '&Search=' + encodeURIComponent($("#search").val());
    }

    $.get(url, function (data) {
        var htmlresponse = '';
        var htmlpagination = '';
        var firstindex = parseInt($(".previous").attr("first-index"));
        var lastindex = parseInt($(".next").attr("last-index"));
        var totalpages = parseInt($(".previous").attr("total-pages"));

        $.each(data.Result, function (i, job) {
            htmlresponse += '<tr>';
            htmlresponse += '<td><a href="/Job/Edit/' + job.Id + '"><span class="glyphicon glyphicon-edit"></span></a></td>';
            htmlresponse += '<td>' + job.Name + '</td>';
            htmlresponse += '<td><a href="#" class="delete" param="' + job.Id + '"><span class="glyphicon glyphicon-remove"></span></a></td>';
            htmlresponse += '</tr>';
        });
        $(".table #grid").html(htmlresponse);
        htmlpagination += '<li><a href="#" class="previous" first-index="1" total-pages="' + data.TotalPages + '">&laquo;</a></li>';
        for (var i = 1; i <= (data.TotalPages > 10 ? 10 : data.TotalPages); i++) {
            if (i == 1) {
                htmlpagination += '<li class="active"><span>' + i + '<span class="sr-only">(current)</span></span></li>';
            }
            else {
                htmlpagination += '<li><a href="#" class="index">' + i + '</a></li>';
            }
            lastindex = i;
        }
        htmlpagination += '<li><a href="#" class="next" last-index="' + lastindex + '">&raquo;</a></li>';
        $(".pagination").html(htmlpagination);
    });
});

$('.pagination').on('click', '.previous', function (e) {
    var htmlpagination = '';
    var firstindex = (parseInt($(".previous").attr("first-index")) - 10);
    var lastindex;
    var totalpages = parseInt($(".previous").attr("total-pages"));
    if (firstindex > 0) {

        htmlpagination += '<li><a href="#" class="previous" first-index="' + firstindex + '" total-pages="' + totalpages + '">&laquo;</a></li>';
        for (var i = firstindex; i < firstindex + 10; i++) {
            htmlpagination += '<li><a href="#" class="index">' + i + '</a></li>';
            lastindex = i;
        }

        htmlpagination += '<li><a href="#" class="next" last-index="' + lastindex + '">&raquo;</a></li>';

        $(".pagination").html(htmlpagination);
    }
    e.preventDefault();
    return false;
});

$('.pagination').on('click', '.index', function (e) {
    var url = '/Job/List?PageIndex=' + $(this).text();
    var pageIndex = parseInt($(this).text());

    if ($("#search").val().length > 0) {
        url += '&Search=' + encodeURIComponent($("#search").val());
    }

    $.get(url, function (data) {
        var htmlresponse = '';
        var htmlpagination = '';
        var firstindex = parseInt($(".previous").attr("first-index"));
        var lastindex = parseInt($(".next").attr("last-index"));
        var totalpages = parseInt($(".previous").attr("total-pages"));

        $.each(data.Result, function (i, job) {
            htmlresponse += '<tr>';
            htmlresponse += '<td><a href="/Job/Edit/' + job.Id + '"><span class="glyphicon glyphicon-edit"></span></a></td>';
            htmlresponse += '<td>' + job.Name + '</td>';
            htmlresponse += '<td><a href="#" class="delete" param="' + job.Id + '"><span class="glyphicon glyphicon-remove"></span></a></td>';
            htmlresponse += '</tr>';
        });
        $(".table #grid").html(htmlresponse);
        if (data.TotalPages == totalpages) {
            htmlpagination += '<li><a href="#" class="previous" first-index="' + firstindex + '" total-pages="' + data.TotalPages + '">&laquo;</a></li>';
            for (var i = firstindex; i <= lastindex; i++) {
                if (i == pageIndex) {
                    htmlpagination += '<li class="active"><span>' + i + '<span class="sr-only">(current)</span></span></li>';
                }
                else {
                    htmlpagination += '<li><a href="#" class="index">' + i + '</a></li>';
                }
            }
            htmlpagination += '<li><a href="#" class="next" last-index="' + lastindex + '">&raquo;</a></li>';
        }
        else {
            htmlpagination += '<li><a href="#" class="previous" first-index="1" total-pages="' + data.TotalPages + '">&laquo;</a></li>';
            for (var i = 1; i <= (data.TotalPages > 10 ? 10 : data.TotalPages); i++) {
                if (i == 1) {
                    htmlpagination += '<li class="active"><span>' + i + '<span class="sr-only">(current)</span></span></li>';
                }
                else {
                    htmlpagination += '<li><a href="#" class="index">' + i + '</a></li>';
                }
                lastindex = i;
            }
            htmlpagination += '<li><a href="#" class="next" last-index="' + lastindex + '">&raquo;</a></li>';
        }
        $(".pagination").html(htmlpagination);
    });

    e.preventDefault();
    return false;
});

$('.pagination').on('click', '.next', function (e) {

    var totalpages = parseInt($(".previous").attr("total-pages"));
    if (parseInt($(".next").attr("last-index")) < totalpages) {
        var htmlpagination = '';
        var firstindex = (parseInt($(".next").attr("last-index")) + 1);
        var lastindex;

        htmlpagination += '<li><a href="#" class="previous" first-index="' + firstindex + '" total-pages="' + totalpages + '">&laquo;</a></li>';
        for (var i = (parseInt($(".next").attr("last-index")) + 1); i <= ((totalpages > ((parseInt($(".next").attr("last-index"))) + 10)) ? (parseInt($(".next").attr("last-index")) + 10) : totalpages); i++) {
            htmlpagination += '<li><a href="#" class="index">' + i + '</a></li>';
            lastindex = i;
        }
        htmlpagination += '<li><a href="#" class="next" last-index="' + lastindex + '">&raquo;</a></li>';

        $(".pagination").html(htmlpagination);
    }
    e.preventDefault();
    return false;
});

$("#grid").on('click', '.delete', function (e) {
    if (confirm("Are you sure,you want to delete?")) {
        $.ajax({
            type: 'POST',
            url: '/Job/Delete',
            dataType: 'json',
            data: { Id: parseInt($(this).attr("param")) },
            success: function () {
                $('.refresh a').click();
            },
            error: function () {
                alert('error');
            }
        });
    }
    e.preventDefault();
    return false;
});