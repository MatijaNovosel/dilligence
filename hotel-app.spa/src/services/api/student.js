import axios from 'axios';

export default {
  async getStudent(id) {
    return await axios.get(`Student/${id}`);
  },
  async getPretplata(studentId) {
    return await axios.get(`Student/pretplata/${studentId}`);
  },
  async subscribe(password, studentId, kolegijId) {
    return await axios.put('Pretplata', {
      password,
      studentId,
      kolegijId
    });
  }
}