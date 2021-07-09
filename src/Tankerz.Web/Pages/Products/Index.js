$(function () {
    var requestCate = getParameterByName("cateid") != null ? getParameterByName("cateid") : 0;
    var l = abp.localization.getResource('Tankerz');
    
    var dataTable = $('#main_table--page').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            pageLength: 25,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(tankerz.products.product.getList, function () {
                return { cateId: requestCate }
            }),
            columnDefs: [
                {
                    width: 100,
                    className: "text-center",
                    title: l('Image'),
                    data: "image",
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
                    width: 200,
                    className: "text-center",
                    title: l('CategoryName'),
                    data: "productCategoryName"
                },
                {
                    width: 150,
                    className: "text-center",
                    title: l('Priority'),
                    data: "displayOrder"
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
                    width: 180,
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
                                        abp.ui.block({ busy: true });
                                        window.location.href = abp.appPath + 'Products/Edit?id=' + data.record.id;
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('DeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        tankerz.products.product
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
        window.location.href = abp.appPath + 'Products/Create?cateid=' + requestCate;
    });
});