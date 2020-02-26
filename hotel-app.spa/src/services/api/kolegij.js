import axios from 'axios';

export default {
  async getKolegij(id) {
    return await axios.get(`Kolegij/${id}`);
  },
  async getKolegiji(smjerIDs, name, minECTS, maxECTS, isvu, skip, take) {
    return await axios.get('Kolegij', {
      params: {
        smjerIDs,
        name,
        minECTS,
        maxECTS,
        isvu,
        skip,
        take
      }
    });
  },
  async getKolegijNews(kolegijId, skip, take) {
    return await axios.get(`Kolegij/Vijesti/${kolegijId}`, {
      params: {
        skip,
        take
      }
    });
  },
  async getKolegijStudents(kolegijId, skip, take) {
    return await axios.get(`Kolegij/Students/${kolegijId}`, {
      params: {
        skip,
        take
      }
    })
  },
  async getKolegijByPreplate(studentId) {
    return await axios.get(`Kolegij/my/${studentId}`);
  },
  async getKolegijSidebarContent(kolegijId) {
    return await axios.get(`Kolegij/SidebarContent/${kolegijId}`);
  },
  async connectSidebarFile(sidebarContentId, fileIDs) {
    return await axios.post('Kolegij/ConnectSidebarFile', { 
      sidebarContentId,
      fileIDs 
    });
  }
}