import ZAPOSLJENJE from '../constants/employment';
import ODJEL from '../constants/odjel';

export default async ({ Vue }) => {
  Vue.filter("zaposljenjeFilter", val => {
    return Object.keys(ZAPOSLJENJE).find(k => ZAPOSLJENJE[k] === val);
  });
  Vue.filter("odjelFilter", val => {
    return Object.keys(ODJEL).find(k => ODJEL[k] === val);
  });
}
