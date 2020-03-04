import axios from 'axios';

export default {
  async getStudent(id) {
    return await axios.get(`Student/${id}`);
  },
  async getPretplata(studentId) {
    return await axios.get(`Student/pretplata/${studentId}`);
  },
  async postPreplata(studentId, pretplataIDs) {
    return await axios.post('Pretplata', {
      studentId,
      pretplataIDs
    });
  }
}