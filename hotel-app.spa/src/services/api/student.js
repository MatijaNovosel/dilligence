import axios from 'axios';

export default {
  async getStudent(id) {
    return await axios.get(`Student/${id}`);
  }
}