import axios from 'axios';

export default {
  /**
   * Uploads a file, then connects it to a specific sidebar.
   * @param {FormData} files - Files to be uploaded.
   * @param {number} id - The sidebar id.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async uploadSidebar(files, sidebarId) {
    return await axios.post(`File/upload-sidebar/${sidebarId}`, files);
  },
  /**
   * Deletes a file.
   * @param {number} id - The file id.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async deleteFile(id) {
    return await axios.delete(`File/${id}`);
  },
  /**
   * Uploads a list of files contained within FormData.
   * @param {FormData} files - Files to be uploaded.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async upload(files) {
    return await axios.post('File/upload', files);
  }
}