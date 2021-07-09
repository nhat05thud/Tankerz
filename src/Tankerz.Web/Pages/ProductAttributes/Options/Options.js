$(function () {
    var requestCate = $("#ProductAttribute_Id").val() != "" ? $("#ProductAttribute_Id").val() : 0;

    var l = abp.localization.getResource('Tankerz');
    var createModal = new abp.ModalManager(abp.appPath + 'ProductAttributes/Options/CreateModal?attributeid=' + requestCate);
    var editModal = new abp.ModalManager(abp.appPath + 'ProductAttributes/Options/EditModal');

    var dataTable = $('#main_table--page').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(tankerz.productAttributeOptions.productAttributeOption.getList, function() {
                return { attributeId: requestCate }
            }),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Value'),
                    data: "value"
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
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('DeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        tankerz.productGroups.productGroup
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