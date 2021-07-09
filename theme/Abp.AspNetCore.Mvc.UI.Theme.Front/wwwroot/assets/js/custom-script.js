$.fn.modal.Constructor.prototype._enforceFocus = function () {
    modal_this = this
    $(document).on('focusin', function (e) {
        if (modal_this.$element != null) {
            if (modal_this.$element[0] !== e.target && !modal_this.$element.has(e.target).length
                && !$(e.target.parentNode).hasClass('cke_dialog_ui_input_select')
                && !$(e.target.parentNode).hasClass('cke_dialog_ui_input_text')) {
                modal_this.$element.focus()
            }
        }
    })
};
$('.back-to-history').click(function (e) {
    abp.ui.block({ busy: true });
    window.location.href = $(this).data("href");
});

$('.submit-with-busy').click(function (e) {
    abp.ui.block({ busy: true });
    setTimeout(function () {
        if ($("input, textarea, select").hasClass("input-validation-error")) {
            abp.ui.unblock();
        }
    }, 500);
});
$('.js-tagify').each(function () {
    var tagify = $.HSCore.components.HSTagify.init($(this));
});
var sidebar = $('.js-navbar-vertical-aside').hsSideNav();
$('.js-nav-tooltip-link').tooltip({ boundary: 'window' })
$(".js-nav-tooltip-link").on("show.bs.tooltip", function (e) {
    if (!$("body").hasClass("navbar-vertical-aside-mini-mode")) {
        return false;
    }
});
$('.js-hs-unfold-invoker').each(function () {
    var unfold = new HSUnfold($(this)).init();
});
$("li.nav-item").each(function () {
    if ($(this).hasClass("active")) {
        var currentParent = $(this).parent();
        $(this).parents(".navbar-vertical-aside-has-menu").addClass("show").find(currentParent).addClass("show");
    }
});
function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
function updateSlug(element) {
    if ($(element).hasClass("fa-check")) {
        $(element).addClass("fa-pencil-alt").removeClass("fa-check");
        $(".unique-slug .form-group input[type='text']").prop("readonly", true);
    }
    else {
        $(element).removeClass("fa-pencil-alt").addClass("fa-check");
        $(".unique-slug .form-group input[type='text']").prop("readonly", false);
        $(".unique-slug .form-group input[type='text']").focus();
    }
}
function onUniqueNameChange(element) {
    $(".unique-slug .form-group input[type='text']").val(toSlug($(element).val()));
}
function toSlug(str) {
    // Chuyển hết sang chữ thường
    str = str.toLowerCase();

    // xóa dấu
    str = str
        .normalize('NFD') // chuyển chuỗi sang unicode tổ hợp
        .replace(/[\u0300-\u036f]/g, ''); // xóa các ký tự dấu sau khi tách tổ hợp

    // Thay ký tự đĐ
    str = str.replace(/[đĐ]/g, 'd');

    // Xóa ký tự đặc biệt
    str = str.replace(/([^0-9a-z-\s])/g, '');

    // Xóa khoảng trắng thay bằng ký tự -
    str = str.replace(/(\s+)/g, '-');

    // Xóa ký tự - liên tiếp
    str = str.replace(/-+/g, '-');

    // xóa phần dư - ở đầu & cuối
    str = str.replace(/^-+|-+$/g, '');

    // return
    return str;
}