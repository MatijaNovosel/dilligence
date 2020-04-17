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
  Vue.filter("timeStampFilter", val => {
    return (new Date(Date.parse(val))).toString().slice(4, 24);
  });
  Vue.filter("countdownFilter", val => {
    // Input: 3600 (seconds)
    const format = x => `0${Math.floor(x)}`.slice(-2);
    const hours = val / 3600;
    const minutes = (val % 3600) / 60;
    return [hours, minutes, val % 60]
      .map(format)
      .join(":");
  });
  Vue.filter("hoursMinutesToSecondsFilter", val => {
    // Input: 04:45
    const splitText = val.split(":");
    const [hours, minutes] = [new Number(splitText[0]), new Number(splitText[1])];
    return (hours * 3600 + minutes * 60);
  });
}
