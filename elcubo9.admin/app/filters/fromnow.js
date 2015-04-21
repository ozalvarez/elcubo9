app.filter('fromNow', function () {
    return function (date) {
        return moment.utc(date).utcOffset(-new Date().getTimezoneOffset()).fromNow();
    }
});
app.filter('format', function () {
    return function (date) {
        return moment.utc(date).utcOffset(-new Date().getTimezoneOffset()).format('MMMM Do YYYY, h:mm:ss a');
    }
});