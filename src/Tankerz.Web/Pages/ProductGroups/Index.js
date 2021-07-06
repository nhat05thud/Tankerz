﻿$(function () {
    var l = abp.localization.getResource('Tankerz');
    var createModal = new abp.ModalManager(abp.appPath + 'ProductGroups/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'ProductGroups/EditModal');

    var dataTable = $('#ProductGroupsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(tankerz.productGroups.productGroup.getList),
            columnDefs: [
                {
                    width: 70,
                    title: l('Image'),
                    data: "images",
                    render: function (data) {
                        if (data != null) {
                            return "<img src=" + JSON.parse(data)[0].imageSmallUrl + " alt=" + JSON.parse(data)[0].name + " width='60px' />";
                        }
                        else {
                            return "<img src=\"https://via.placeholder.com/60x60\" alt=\"no image\" width='60px' />";
                        }
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    width: 150,
                    className: "text-center",
                    title: l('Priority'),
                    data: "priority"
                },
                {
                    width: 150,
                    className: "text-center",
                    title: l('IsPublish'),
                    data: "isPublish",
                    render: function (data) {
                        return data ? "<i style=\"color:green\" class=\"fas fa-check-circle\"></i>" : "<i style=\"color:red\" class=\"fas fa-times-circle\"></i>";
                    }
                },
                {
                    width: 150,
                    className: "text-center",
                    title: l('IsShowOnMenu'),
                    data: "isShowOnMenu",
                    render: function (data) {
                        return data ? "<i style=\"color:green\" class=\"fas fa-check-circle\"></i>" : "<i style=\"color:red\" class=\"fas fa-times-circle\"></i>";
                    }
                },
                {
                    width: 150,
                    className: "text-center",
                    title: l('IsShowOnHomePage'),
                    data: "isShowOnHomePage",
                    render: function (data) {
                        return data ? "<i style=\"color:green\" class=\"fas fa-check-circle\"></i>" : "<i style=\"color:red\" class=\"fas fa-times-circle\"></i>";
                    }
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
                                        abp.ui.block();
                                        editModal.open({ id: data.record.id });

                                        abp.ui.unblock();
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'productGroupDeletionConfirmationMessage',
                                            data.record.name
                                        );
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
        e.preventDefault();
        createModal.open();
    });
});