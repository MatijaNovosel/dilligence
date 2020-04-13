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
    const format = x => `0${Math.floor(x)}`.slice(-2);
    const hours = val / 3600;
    const minutes = (val % 3600) / 60;
    return [hours, minutes, val % 60]
      .map(format)
      .join(":");
  });
}
