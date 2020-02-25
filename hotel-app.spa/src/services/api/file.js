import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async upload(file) {
    return await axios.post(`${ROUTING.baseRoute}File/Upload`, file, { headers: { 'content-type': 'multipart/form-data' } });
  },
  async uploadMultiple(files) {
    return await axios.post(`${ROUTING.baseRoute}File/UploadMultiple`, files, { headers: { 'content-type': 'multipart/form-data' } });
  },
  async uploadSidebar(files, id) {
    return await axios.post(`${ROUTING.baseRoute}File/uploadSidebar`, { files, id }, { headers: { 'content-type': 'multipart/form-data' } });
  },
  async deleteFile(id) {
    return await axios.delete(`${ROUTING.baseRoute}File/${id}`);
  },
  async uploadCQRS(files) {
    return await axios.post(`${ROUTING.baseRoute}File/upload`, { files }, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
  }
}