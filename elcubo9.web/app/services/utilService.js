'use strict';
app.factory('utilService', ["toastrService",
    function (nf) {
        return {
            errorCallback: function (data,statusCode) {
                if (statusCode != 401) {
                    nf.error(data);
                }
            }
        };
    }
]);
