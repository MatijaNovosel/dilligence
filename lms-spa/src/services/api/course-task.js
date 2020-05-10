import axios from 'axios';

export default {
  async getCourseTasks(courseId) {
    return await axios.get('CourseTask/' + courseId);
  },
  async createCourseTask(payload) {
    return await axios.post('CourseTask', payload);
  }
}