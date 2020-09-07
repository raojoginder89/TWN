// String.prototype.format = function () {
//     // The string containing the format items (e.g. "{0}")
//     // will and always has to be the first argument.
//     let theString = arguments[0];

//     // start with the second argument (i = 1)
//     for (let i = 1; i < arguments.length; i++) {
//         // "gm" = RegEx options for Global search (more than one instance)
//         // and for Multiline search
//         const regEx = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
//         theString = theString.replace(regEx, arguments[i]);
//     }

//     return theString;
// };
