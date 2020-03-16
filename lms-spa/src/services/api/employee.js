import axios from 'axios';

export default {
  async getEmployees(name, surname, odjelIds, zaposljenjeIds) {
    return await axios.get('Employee', {
      params: {
        name,
        surname,
        odjelIds,
        zaposljenjeIds
      }
    });
  }
}