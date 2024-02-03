
var today = new Date();
// get the current hour of the day note that we are using military standard during the calculations not AM/PM standard
var hours = today.getHours();
// get the current minute of the hour
var minutes = today.getMinutes();
// calculate the current minute of the day note that the day start from 12:00 AM at minute 0 and ends in 11:59 PM at minute 1439
count_min = hours * 60 + minutes;
// to log out to console what minute we are in the day right now 
console.log(count_min)
// for check-in:
// from 7:00 AM to 8:30 AM show on time check-in button and hide late check-in button
if (count_min >= 420 && count_min <= 510) {

    $("#ontime-in").show();
    $("#late-in").hide();

}
// from 8:31 AM to 2:00 PM show on time late check-in button and hide ontime check-in button
else if (count_min >= 511 && count_min <= 840) {

    $("#late-in").show();
    $("#ontime-in").hide();

}
// hide the two buttons for the rest of the day
else {
    $("#ontime-in").hide();
    $("#late-in").hide();
}
// for check-out:
// from 7:00 AM to 2:59 PM show early check-out button and hide on time check-out button
if (count_min >= 420 && count_min <= 899) {

    $("#early-out").show();
    $("#ontime-out").hide();

}
// from 3:00 PM to 5:00 PM show on time check-out button and hide early check-out button
else if (count_min >= 900 && count_min <= 1020) {

    $("#ontime-out").show();
    $("#early-out").hide();

}
// hide the two buttons for the rest of the day
else {
    $("#ontime-out").hide();
    $("#early-out").hide();
}