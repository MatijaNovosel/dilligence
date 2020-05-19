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
  },
  async deleteTask(id, courseId) {
    return await axios.delete('CourseTask/' + id, { params: { courseId } });
  },
  async addNewSubmission(payload) {
    return await axios.post('CourseTask/new-attempt', payload);
  },
  async editSubmission(payload) {
    return await axios.put('CourseTask/edit-attempt', payload);
  },
  async deleteTask(id, courseId) {
    return await axios.delete('CourseTask/' + id, { params: { courseId } });
  },
  async getTaskAttempts(id, courseId) {
    return await axios.get('CourseTask/attempts/' + id, { params: { courseId } });
  },
  async getTaskAttemptDetails(taskAttemptId, courseId) {
    return await axios.get('CourseTask/attempts/details/' + taskAttemptId, { params: { courseId } })
  },
  async gradeAttempt(payload) {
    return await axios.post('CourseTask/grade-attempt', payload);
  }
}