import axios from 'axios';

export default {
  async getPretplata(studentId) {
    return await axios.get(`Pretplata/${studentId}`);
  },

  async postPreplata(studentId, pretplataIDs) {
    return await axios.post('Pretplata', {
      studentId,
      pretplataIDs
    });
  }
}