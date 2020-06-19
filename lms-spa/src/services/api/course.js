import axios from 'axios';

export default {
  /**
   * Retrieves a list of courses, filtered by function parameters.
   * @param {number} userId - User id, sent in order to check for subscribed courses.
   * @param {number} specializationId - Specialization id.
   * @param {string} name - Course name.
   * @param {boolean} subscribed - Filter by subscribed courses.
   * @param {boolean} nonSubscribed - Filter by non subscribed courses.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async get(userId, specializationId, name, subscribed, nonSubscribed) {
    return await axios.get('Course', {
      params: {
        userId,
        specializationId,
        name,
        subscribed,
        nonSubscribed
      }
    });
  },
  /**
   * Retrieves the details of a specific course.
   * @param {number} id - Id of the requested course.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getCourse(id) {
    return await axios.get(`Course/${id}`);
  },
  /**
   * Retrieves a list of sidebars of a specific course.
   * @param {number} id - Id of the requested course.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getCourseSidebar(id) {
    return await axios.get(`Course/sidebar/${id}`);
  },
  /**
   * Retrieves a list of notifications for a specific course.
   * @param {number} courseId - Id of the requested course.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getNotifications(courseId, showArchived, showNonArchived, sortByNew) {
    return await axios.get(`Course/notifications/${courseId}`, {
      params: {
        showArchived,
        showNonArchived,
        sortByNew
      }
    });
  },
  async getCourseUsers(courseId, name, surname, username) {
    return await axios.get(`Course/users/${courseId}`, { params: { name, surname, username } });
  },
  async createNewSidebar(payload, courseId) {
    return await axios.post('Course/new-sidebar', payload, { params: { courseId } });
  },
  async deleteSidebar(sidebarId, courseId) {
    return await axios.delete('Course/delete-sidebar/' + sidebarId, { params: { courseId } });
  },
  async createNewDiscussion(payload, courseId) {
    return await axios.post('Course/new-discussion', payload, { params: { courseId } });
  },
  async getDiscussions(courseId, sortByNewer) {
    return await axios.get('Course/discussions', { params: { courseId, sortByNewer } });
  },
  async getLandingPage(courseId) {
    return await axios.get('Course/landing-page', { params: { courseId } });
  },
  async sendReply(payload) {
    return await axios.post('Course/reply', payload);
  },
  async getReplies(discussionId) {
    return await axios.get('Course/discussion-replies', { params: { discussionId } });
  },
  async deleteDiscussion(discussionId) {
    return await axios.delete('Course/discussion', { params: { discussionId } });
  },
  async updateLandingPage(payload, courseId) {
    return await axios.post('Course/landing-page', payload, { params: { courseId } });
  },
  async updatePassword(courseId, newPassword) {
    return await axios.put('Course/password', { courseId, newPassword }, { params: { courseId } });
  },
  async getUserGrades(courseId, userId) {
    return await axios.get('Course/grades/' + userId, { params: { courseId } });
  },
  async createNewCourse(payload) {
    return await axios.post('Course/new-course', payload);
  },
  async muteParticipant(payload) {
    return await axios.post('Course/mute', payload)
  },
  async kickParticipant(payload) {
    return await axios.post('Course/kick', payload);
  },
  async deleteCourse(id) {
    return await axios.delete('Course/delete-course/' + id);
  }
}