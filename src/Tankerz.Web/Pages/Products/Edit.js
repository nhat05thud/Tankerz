$(function () {
    var productId = $("#Product_Id").val() != "" ? $("#Product_Id").val() : 0;

    var l = abp.localization.getResource('Tankerz');
    var createModal = new abp.ModalManager(abp.appPath + 'Products/Attributes/CreateModal?productid=' + productId);
    var editModal = new abp.ModalManager(abp.appPath + 'Products/Attributes/EditModal');

    $('a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
        $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    });

    var dataTable = $('#main_table--page').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(tankerz.productWithMultipleAttributeOptions.productWithMultipleAttributeOption.getList, function () {
                return { productId: productId }
            }),
            columnDefs: [
                {
                    title: l('ProductAttributeName'),
                    data: "productAttributeName"
                },
                {
                    title: l('ProductAttributeOptionName'),
                    data: "productAttributeOptionName"
                },
                {
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
                                        editModal.open({ id: data.record.id });
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

    createModal.onResult(function () {
        abp.notify.success(
            l('SuccessfullyCreate')
        );
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        abp.notify.success(
            l('SuccessfullyEdit')
        );
        dataTable.ajax.reload();
    });

    $('#CreateNew').click(function (e) {
        abp.ui.block({ busy: true });
        e.preventDefault();
        createModal.open();
    });
});