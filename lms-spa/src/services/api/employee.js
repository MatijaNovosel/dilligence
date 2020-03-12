import axios from 'axios';

export default {
  async getEmployees() {
    return await axios.get('Employee');
  }
}