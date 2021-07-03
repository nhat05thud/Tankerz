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