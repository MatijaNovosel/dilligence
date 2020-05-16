import axios from 'axios';

export default {
  async getCourseTasks(courseId, name, showOverdue, showActive) {
    return await axios.get('CourseTask/' + courseId, {
      params: {
        name,
        showOverdue,
        showActive
      }
    });
  },
  async createCourseTask(payload) {
    return await axios.post('CourseTask', payload);
  },
  async getTask(taskId) {
    return await axios.get('CourseTask/details/' + taskId);
  },
  async updateTask(payload) {
    return await axios.put('CourseTask', payload);
  }
}