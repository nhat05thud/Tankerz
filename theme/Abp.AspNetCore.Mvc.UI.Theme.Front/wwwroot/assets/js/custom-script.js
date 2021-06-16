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