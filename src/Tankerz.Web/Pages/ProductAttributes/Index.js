$(function () {
    var l = abp.localization.getResource('Tankerz');

    var dataTable = $('#main_table--page').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(tankerz.productAttributes.productAttribute.getList),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    width: 150,
                    className: "text-center",
                    title: l('Priority'),
                    data: "displayOrder"
                },
                {
                    title: l('Actions'),
                    width: 110,
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        abp.ui.block({ busy: true });
                                        window.location.href = abp.appPath + 'ProductAttributes/Edit?id=' + data.record.id;
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('DeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        tankerz.blogCategories.blogCategory
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.success(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                }
            ]
        })
    );

    $('#CreateNew').click(function (e) {
        abp.ui.block({ busy: true });
        e.preventDefault();
        window.location.href = abp.appPath + 'ProductAttributes/Create';
    });
});