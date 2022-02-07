/*
 * Author: Dennis Kamau
 * Date: 15 Sep 2021
 * Description:
 *      This is a js file for Accounts page
 **/

function renderAccountListComponents() {
    $('.daterange').daterangepicker({
        ranges: {
            Today: [moment(), moment()],
            Yesterday: [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        startDate: moment().subtract(29, 'days'),
        endDate: moment()
    }, function (start, end) {
        // eslint-disable-next-line no-alert
        $("#date_filter").html(`Summary <b>From: </b> ${start.format('DD-MMM-YYYY')} <b>To: </b> ${end.format('DD-MMM-YYYY')}`);
        //alert('You chose: ' + start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'))
    })    
}
