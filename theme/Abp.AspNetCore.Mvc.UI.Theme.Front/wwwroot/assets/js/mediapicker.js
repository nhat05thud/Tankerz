Vue.component("file", {
    props: {
        jsonpoststring: String,
        ismultiple: Boolean
    },
    data: function () {
        return {
            isMulti: this.ismultiple,
            showModal: false,
            items: [],
            jsonPostString: this.jsonpoststring
        }
    },
    template: "#file-template",
    mounted: function () {
        if (this.jsonPostString != "") {
            this.items = JSON.parse(this.jsonPostString);
        }
        this.$emit('pass_data_to_parent', this.jsonPostString);
    },
    methods: {
        passDataToParent(data) {
            this.items = data;
            this.jsonPostString = data;
            this.$emit('pass_data_to_parent', JSON.stringify(data));
        },
        removeImage(index) {
            Vue.delete(this.items, index);
            var data = this.items;
            this.$emit('pass_data_to_parent', JSON.stringify(data));
        }
    }
})

// modal
Vue.component("modal", {
    props: {
        ismulti: Boolean,
        items: Array
    },
    data: function () {
        return {
            isMultiple: this.ismulti,
            isLoading: true,
            showAddModal: false,
            searchQuery: null,
            activeTabs: 'media',
            imageActive: false,
            statusText: "",
            currentFolder: {},
            //lstChooses: this.items,
            lstFolders: [],
            lstChooses: [],
            images: []
        }
    },
    mounted: function () {
        this.loadFolderData();
    },
    computed: {
        resultQuery() {
            if (this.searchQuery) {
                return this.images.filter((item) => {
                    return this.searchQuery.toLowerCase().split(' ').every(v => item.name.toLowerCase().includes(v))
                })
            } else {
                return this.images;
            }
        }
    },
    template: "#modal-template",
    methods: {
        loadData() {
            var $this = this;
            $this.isLoading = true;
            if (!_.isEmpty($this.currentFolder)) {
                tankerz.tankerzFiles.tankerzFile.getFilesInFolder($this.currentFolder.id).done(function (result) {
                    $this.images = result;
                }).then(function (result) {
                    _.forEach($this.items, function (o) {
                        // push constructor items to lstChoose <= trick to not get 2 way binding => (because emit close alway append item to lstChoose)
                        $this.lstChooses.push(o);
                        // end push
                        _.filter(result, function (e) {
                            if (o.id == e.id) {
                                return e.checked = true;
                            }
                        });
                    });
                    setTimeout(function () {
                        $this.isLoading = false;
                    }, 1000);
                });
            }
        },
        loadFolderData() {
            var $this = this;
            tankerz.tankerzFolders.tankerzFolder.getFolders()
                .done(function (result) {
                    if (result.length > 0) {
                        $this.lstFolders = result;
                        if (!localStorage.currentFolder) {
                            $this.currentFolder = result[0];
                        }
                        // load current folder from localStorage
                        else {
                            $this.currentFolder = JSON.parse(localStorage.currentFolder);
                        }
                        // end load current folder
                        $this.loadData();
                    }
                    else {
                        $this.statusText = "Tạo folder trước khi upload hình ảnh";
                        setTimeout(function () {
                            $this.isLoading = false;
                        }, 1000);
                    }
                })
                .then(function (result) { });
        },
        isActive(menuItem) {
            return this.activeTabs === menuItem
        },
        setActive(menuItem) {
            this.activeTabs = menuItem
        },
        toggleActive(event, obj) {
            var $event = event;
            var $this = this;
            if (this.isMultiple) {
                // path[1] = parent upto 1 level
                if (event.path[1].className == "img-wrapper") {
                    event.path[1].className = "img-wrapper active";
                    // push obj to new array
                    obj.checked = true;
                    this.lstChooses.push(obj);
                }
                else {
                    event.path[1].className = "img-wrapper";
                    // splice obj to new array
                    obj.checked = false;
                    Vue.delete(this.lstChooses, _.findKey(this.lstChooses, ['id', obj.id]));
                }
            }
            else {
                Vue.nextTick(function () {
                    $(".img-wrapper").removeClass("active");
                    $event.path[1].className = "img-wrapper active";
                    $this.lstChooses = [];
                    $this.lstChooses.push(obj);
                });
            }
        },
        sendDataToParent() {
            var data = [...new Set(this.lstChooses)];
            this.$emit('pass_data_to_parent', data);
            this.$emit('close');
        },
        changeFolder(item) {
            this.currentFolder = item;
            localStorage.currentFolder = JSON.stringify(this.currentFolder);
            this.loadData();
        }
    }
});
// upload
Vue.component("upload-file", {
    props: {
        currentFolder: Object
    },
    data: function () {
        return {
            isNotImageType: false,
            isProgress: false,
            isEnableUpload: true,
            isSuccessful: false,
            isFailure: false,
            folderId: 0,
            lstFiles: []
        }
    },
    watch: {
        currentFolder: function (newVal, oldVal) {
            this.folderId = newVal.id;
        }
    },
    template: "#upload-file-template",
    methods: {
        onChange(event) {
            var $this = this;
            $this.isNotImageType = false;
            var files = event.target.files || event.dataTransfer.files;
            if (!files.length) {
                return;
            }
            var data = new FormData();
            _.forEach(files, function (file) {
                if (file.type === "image/jpeg" || file.type === "image/png") {
                    $this.createImage(file);
                    data.append("files", file, file.name);
                }
                else {
                    $this.isNotImageType = true;
                    return;
                }
            });

            // this line right below will reset the 
            // input field so if you removed it you can re-add the same file
            event.target.value = ''
        },
        createImage(file) {
            var $this = this;
            var image = new Image();
            var reader = new FileReader();
            reader.onload = (e) => {
                image = e.target.result;
                var obj = {
                    edit: false, // edit inline
                    url: image,
                    name: file.name.replace(/\.[^/.]+$/, ""), // get name without extension
                    extension: "." + /[^.]+$/.exec(file.name)[0], // get extension without name
                    contenttype: image.match(/[^:]\w+\/[\w-+\d.]+(?=;|,)/)[0]
                };
                $this.lstFiles.push(obj);
            };
            reader.readAsDataURL(file);
        },
        toggleEdit: function (item) {
            var $this = this;
            var isEnable = true;
            _.forEach($this.lstFiles, function (item) {
                if (item.name === "") {
                    isEnable = false;
                }
            });
            item.edit = !item.edit;
            // Focus input field
            if (item.edit) {
                Vue.nextTick(function () {
                    $this.$refs.input[0].focus();
                });
            }
            $this.isEnableUpload = isEnable;
        },
        saveEdit: function (item) {
            var $this = this;
            if (item.name !== "") {
                $this.toggleEdit(item);
            }
            else {
                $this.isEnableUpload = false;
            }
        },
        removeFileUpload(index) {
            Vue.delete(this.lstFiles, index);
        },
        sendToServer: function () {
            var $this = this;
            $this.isProgress = true;
            var data = _.map($this.lstFiles, function (item) {
                var obj = {
                    name: item.name,
                    base64String: (/base64,(.+)/.exec(item.url)[1]),
                    extension: item.extension,
                    folderId: $this.folderId,
                    contenttype: item.contenttype
                }
                return obj;
            });
            tankerz.tankerzFiles.tankerzFile.uploadFiles(data).then(function (result) {
                if (result === true) {
                    $this.lstFiles = [];
                    $this.isProgress = false;
                    $this.isSuccessful = true;
                    $this.isFailure = false;
                    $this.$emit('reload_data');
                }
                else {
                    $this.lstFiles = [];
                    $this.isProgress = false;
                    $this.isSuccessful = false;
                    $this.isFailure = true;
                }
            });
        }
    }
});
// add folder
Vue.component("add-folder", {
    data: function () {
        return {
            isFailure: false,
            isSuccessful: false,
            folderName: ""
        }
    },
    mounted: function () {

    },
    template: "#add-folder-template",
    methods: {
        onChange() {
            this.isFailure = false;
            this.isSuccessful = false;
        },
        submit() {
            var $this = this;
            tankerz.tankerzFolders.tankerzFolder.createFolder(this.folderName)
                .done(function (result) {
                    if (result === true) {
                        $this.$emit('reload_data');
                        $this.isSuccessful = true;
                    }
                    else {
                        $this.isFailure = true;
                    }
                })
                .then(function (result) {
                    if (result === true) {
                        setTimeout(function () {
                            $this.$emit('close');
                        }, 2000);
                    }
                });
        }
    }
});









//==================================================================//
//==================================================================//
//==================================================================//
//==================================================================//
//==================================================================//
// vue lazy loading
Vue.use(VueLazyload, {
    preLoad: 1.3,
    error: '/assets/images/error.gif',
    loading: '/assets/images/loading.gif',
    attempt: 1
});
// vue lazy loading
if ($("#single-media-picker").length > 0) {
    new Vue({
        el: "#single-media-picker",
        data: {
            jsonPostString: ""
        },
        methods: {
            passDataToParent(data) {
                if (data != "") {
                    this.jsonPostString = JSON.stringify(JSON.parse(data));
                }
            },
        }
    });
}
if ($("#meta-media-picker").length > 0) {
    new Vue({
        el: "#meta-media-picker",
        data: {
            jsonPostString: ""
        },
        methods: {
            passDataToParent(data) {
                if (data != "") {
                    this.jsonPostString = JSON.stringify(JSON.parse(data));
                }
            },
        }
    });
}
if ($("#multiple-media-picker").length > 0) {
    new Vue({
        el: "#multiple-media-picker",
        data: {
            jsonPostString: ""
        },
        methods: {
            passDataToParent(data) {
                if (data != "") {
                    this.jsonPostString = JSON.stringify(JSON.parse(data));
                }
            },
        }
    });
}
if ($("#multiple-media-picker-product").length > 0) {
    new Vue({
        el: "#multiple-media-picker-product",
        data: {
            jsonPostString: ""
        },
        methods: {
            passDataToParent(data) {
                if (data != "") {
                    this.jsonPostString = JSON.stringify(JSON.parse(data));
                }
            },
        }
    });
}