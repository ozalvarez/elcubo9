'use strict';
app.factory('utilService', ["cfpLoadingBar", "toastrService",
    function (lb, nf) {
        return {
            errorCallback: function (data, statusCode) {
                if (statusCode != 401) {
                    nf.error(data);
                }
            }
        };
    }
]);
