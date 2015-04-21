app.filter('fromNow', function () {
    return function (date) {
        return moment.utc(date).utcOffset(-new Date().getTimezoneOffset()).locale("es").fromNow();
    }
});
app.filter('format', function () {
    return function (date) {
        return moment.utc(date).utcOffset(-new Date().getTimezoneOffset()).locale("es").format('MMMM Do YYYY, h:mm:ss a');
    }
});