import axios from 'axios';

export default {
  async getAttempts(userId) {
    return await axios.get('Exam/' + userId);
  },
  async getAttemptDetails(id) {
    return await axios.get('Exam/details/' + id);
  },
  async updateAttemptCommand(payload) {
    return await axios.put('Exam/attempt', payload);
  },
  async createExam(payload) {
    return await axios.post('Exam/create', payload);
  }
}