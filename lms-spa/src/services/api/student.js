import axios from 'axios';

export default {
  async getStudent(id) {
    return await axios.get(`Student/${id}`);
  },
  async getSubscriptions(id) {
    return await axios.get(`Student/pretplata/${id}`);
  },
  async subscribe(password, studentId, kolegijId) {
    return await axios.put('Student', {
      password,
      studentId,
      kolegijId
    });
  }
}