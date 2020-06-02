import axios from 'axios';

export default {
  async getCourseTasks(courseId, name, showOverdue, showActive) {
    return await axios.get('CourseTask', {
      params: {
        courseId,
        name,
        showOverdue,
        showActive
      }
    });
  },
  async createCourseTask(payload, courseId) {
    return await axios.post('CourseTask', payload, { params: { courseId } });
  },
  async getTask(taskId, courseId) {
    return await axios.get('CourseTask/details/' + taskId, { params: { courseId } });
  },
  async updateTask(payload, courseId) {
    return await axios.put('CourseTask', payload, { params: { courseId } });
  },
  async deleteTask(id, courseId) {
    return await axios.delete('CourseTask/' + id, { params: { courseId } });
  },
  async addNewSubmission(payload, courseId) {
    return await axios.post('CourseTask/new-attempt', payload, { params: { courseId } });
  },
  async editSubmission(payload, courseId) {
    return await axios.put('CourseTask/edit-attempt', payload, { params: { courseId } });
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
  async gradeAttempt(payload, courseId) {
    return await axios.post('CourseTask/grade-attempt', payload, { params: { courseId } });
  }
}