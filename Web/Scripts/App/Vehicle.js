$(document).ready(function () {
    var carquery = new CarQuery();
    carquery.init($('input#Year').val(), $('input#Make').val(), $('input#Model').val());
    carquery.initYearMakeModel('Year', 'Make', 'Model');
});
