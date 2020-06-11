import axios from 'axios';

export default {
  /**
   * Gets a list of user attempts.
   * @param {number} userId - Id of the user.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getAttempts(userId) {
    return await axios.get('Exam/' + userId);
  },
  /**
   * Retrieves the details of a user attempt.
   * @param {number} id - Id of the attempt.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getAttemptDetails(id) {
    return await axios.get('Exam/details/' + id);
  },
  /**
   * Updates the details of a user attempt.
   * @param {Object} payload - Attempt payload.
   * @param {number} payload.id - Id of the attempt.
   * @param {boolean} payload.terminated - If the attempt has been terminated by the user.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async updateAttemptCommand(payload) {
    return await axios.put('Exam/attempt', payload);
  },
  /**
   * Updates the details of an exam. 
   * @param {Object} payload - Exam payload.
   * @param {string} payload.name - Name of the exam.
   * @param {number} payload.timeNeeded - Time needed to solve the exam (in seconds).
   * @param {string} payload.dueDate - The date when the exam expires.
   * @param {Object[]} payload.questions - List of exam questions.
   * @param {string} payload.questions.title - Title of the question.
   * @param {string} payload.questions.content - Content of the question, HTML formatting.
   * @param {number} payload.questions.typeId - Question type id (1 for radio, 2 for checkbox).
   * @param {Object[]} payload.questions.answers - Question answers.
   * @param {string} payload.questions.answers.content - Content of the answer, pure plain text.
   * @param {boolean} payload.questions.answers.correct - Indication if the answer is correct.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async updateExam(payload) {
    return await axios.put('Exam/update', payload);
  },
  /**
   * Creates the foundations for an exam, created before entering the exam edit view.
   * @param {Object} payload - Exam payload.
   * @param {number} payload.createdById - Id of the user that created the exam.
   * @param {number} payload.coureId - Id of the course the exam is being created for.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async createExam(payload) {
    return await axios.post('Exam', payload);
  },
  async getUnfinishedExamDetails(examId) {
    return await axios.get('Exam/unfinished/' + examId);
  },
  async getUnfinishedExams(userId, courseId) {
    return await axios.get('Exam/unfinished', {
      params: {
        userId,
        courseId
      }
    });
  },
  async getFinishedExams(userId, courseId) {
    return await axios.get('Exam/finished', {
      params: {
        userId,
        courseId
      }
    })
  },
  async deleteExam(examId) {
    return await axios.delete('Exam', {
      params: {
        examId
      }
    });
  },
  async finalizeExam(payload, courseId) {
    return await axios.post('Exam/finalize', payload, { params: { courseId } })
  },
  async getExamPreview(examId) {
    return await axios.get('Exam/preview', { params: { examId } });
  }
}