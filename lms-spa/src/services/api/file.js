import axios from 'axios';

export default {
  async uploadSidebar(files, id) {
    return await axios.post(`File/uploadSidebar/${id}`, files);
  },
  async deleteFile(id) {
    return await axios.delete(`File/${id}`);
  },
  async upload(files) {
    return await axios.post('File/upload', files);
  }
}