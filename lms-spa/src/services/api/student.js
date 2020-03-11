export default {
  async getStudent(id) {
    return await this.$axios.get(`Student/${id}`);
  },
  async getPretplata(studentId) {
    return await this.$axios.get(`Student/pretplata/${studentId}`);
  },
  async subscribe(password, studentId, kolegijId) {
    return await this.$axios.put('Student', {
      password,
      studentId,
      kolegijId
    });
  }
}