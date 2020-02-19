import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async upload(file) {
    return await axios.post(`${ROUTING.baseRoute}File/Upload`, file);
  },
  async uploadMultiple(files) {
    return await axios.post(`${ROUTING.baseRoute}File/UploadMultiple`, files);
  },
  async deleteFile(id) {
    return await axios.delete(`${ROUTING.baseRoute}File/Delete/${id}`);
  },
  async uploadCQRS(files) {
    return await axios.post(`${ROUTING.baseRoute}File/upload`, {
      Files: files
    });
  }
}