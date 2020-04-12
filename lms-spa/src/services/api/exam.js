import axios from 'axios';

export default {
  async getExamDetails(id) {
    return await axios.get('Exam/' + id);
  },
  async getAttempts(userId) {
    return await axios.get('Exam', { params: { userId } });
  }
}