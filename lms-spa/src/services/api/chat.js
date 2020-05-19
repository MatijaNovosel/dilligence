import axios from 'axios';

export default {
  /**
   * Retrieves the details of a specific chat, including messages.
   * @param {number} id - The chat id.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getChatDetails(id) {
    return await axios.get("Chat/" + id);
  },
  /**
   * Sends a message to a specific chat.
   * @param {Object} payload - Message payload.
   * @param {string} payload.content - Contents of the message.
   * @param {number} payload.chatId - Id of the chat being sent to.
   * @param {number} payload.userId - Id of the user sending the message.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async sendMessage(payload) {
    return await axios.post("Chat", payload);
  },
  /**
   * Retrieves available users to chat with.
   * @param {number} id - Id of the user to retrieve available chats for.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async getAvailableUsers(id, searchText) {
    return await axios.get("Chat/available/" + id, { params: { searchText } });
  },
  /**
   * Creates a new chat instance.
   * @param {Object} payload - New chat payload.
   * @param {string} payload.firstParticipantId - User id of the first participant.
   * @param {number} payload.secondParticipantId - User id of the second participant.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async createNewChat(payload) {
    return await axios.post("Chat/new", payload);
  },
  /**
   * Deletes a message.
   * @param {number} id - Id of the message to be deleted.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async deleteMessage(id) {
    return await axios.delete("Chat/" + id);
  }
}