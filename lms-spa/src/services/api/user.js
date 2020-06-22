import axios from "axios";

export default {
  /**
   * Retrieves a list of users, filtered by their username.
   * @param {string} name - String to be filtered by.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async searchUser(name) {
    return await axios.get("User", {
      params: {
        name
      }
    });
  },
  /**
   * Retrieves a list of chats for a specific user.
   * @param {number} id - Id of the user.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getChats(id) {
    return await axios.get("User/chat/" + id);
  },
  /**
   * Retrieves a list of subscriptions for a specific user.
   * @param {number} id - Id of the user.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getSubscriptions(id) {
    return await axios.get(`User/subscription/${id}`);
  },
  /**
   * Subscribes a user to a course.
   * @param {string} password - Password of the course.
   * @param {number} userId - Id of the user.
   * @param {number} courseId - Id of the course.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async subscribe(password, userId, courseId) {
    return await axios.put('User', {
      password,
      userId,
      courseId
    });
  },
  /**
   * Unsubscribes a user from a course.
   * @param {number} userId - Id of the user.
   * @param {number} courseId - Id of the course.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async unsubscribe(userId, courseId) {
    return await axios.put('User/unsubscribe', {
      userId,
      courseId: courseId
    });
  },
  /**
   * Updates the settings of a user.
   * @param {number} userId - Id of the user.
   * @param {Object} settings - Updated settings.
   * @param {boolean} settings.darkMode - If the user has enabled dark mode.
   * @param {string} settings.locale - Selected localization.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async updateSettings(userId, settings) {
    return await axios.put('User/settings', {
      userId,
      ...settings
    });
  },
  async uploadPicture(userId, picture) {
    return await axios.put('User/image/' + userId, picture);
  },
  async updatePersonalInformation(payload) {
    return await axios.put('User/personal', payload);
  },
  async getUserDetails(userId) {
    return await axios.get('User/' + userId);
  },
  async getBlacklist(userId) {
    return await axios.get('User/blacklist/' + userId);
  },
  async updateBlacklist(payload) {
    return await axios.put('User/update-blacklist/', payload);
  },
  async getAll() {
    return await axios.get('User/all');
  },
  async updatePrivileges(payload) {
    return await axios.put('User/update-privileges', payload);
  },
  async updateGeneralPrivileges(payload) {
    return await axios.put('User/update-general', payload);
  },
  async updateCoursePrivileges(payload) {
    return await axios.put('User/update-specific', payload);
  }
};
