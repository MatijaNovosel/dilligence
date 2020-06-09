import ZAPOSLJENJE from '../constants/employment';
import ODJEL from '../constants/odjel';
import SMJER from '../constants/smjer';

export default async ({ Vue }) => {
  Vue.filter("zaposljenjeFilter", val => {
    return Object.keys(ZAPOSLJENJE).find(k => ZAPOSLJENJE[k] === val);
  });
  Vue.filter("odjelFilter", val => {
    return Object.keys(ODJEL).find(k => ODJEL[k] === val);
  });
  Vue.filter("smjerFilter", val => {
    return Object.keys(SMJER).find(k => SMJER[k] === val);
  });
  Vue.filter("countdownFilter", val => {
    /*

      @Input -> number, seconds to be converted into a string format 
      @Output -> string, hh:mm string format

    */
    const format = x => `0${Math.floor(x)}`.slice(-2);
    const hours = val / 3600;
    const minutes = (val % 3600) / 60;
    return [hours, minutes, val % 60]
      .map(format)
      .join(":");
  });
  Vue.filter("hoursMinutesFormat", val => {
    const format = x => `0${Math.floor(x)}`.slice(-2);
    const hours = val / 3600;
    const minutes = (val % 3600) / 60;
    return [hours, minutes]
      .map(format)
      .join(":");
  });
  Vue.filter("hoursMinutesToSecondsFilter", val => {
    /*

      @Input -> string, hh:mm format string to be converted into seconds
      @Output -> number, number of seconds converted from format string

    */
    const splitText = val.split(":");
    const [hours, minutes] = [new Number(splitText[0]), new Number(splitText[1])];
    return (hours * 3600 + minutes * 60);
  });
  Vue.filter("byteCountToReadableFormat", b => {
    var u = 0, s = 1024;
    while (b >= s || -b >= s) {
      b /= s;
      u++;
    }
    return (u ? b.toFixed(1) + ' ' : b) + ' KMGTPEZY'[u] + 'B';
  });
}
