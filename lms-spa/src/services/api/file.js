export default {
  async uploadSidebar(files, id) {
    return await this.$axios.post(`File/uploadSidebar/${id}`, files);
  },
  async deleteFile(id) {
    return await this.$axios.delete(`File/${id}`);
  },
  async upload(files) {
    return await this.$axios.post('File/upload', files);
  }
}