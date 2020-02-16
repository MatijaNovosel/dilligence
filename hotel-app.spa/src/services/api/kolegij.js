import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async getKolegij(id) {
    return await axios.get(`${ROUTING.baseRoute}Kolegij/${id}`);
  },
  async getKolegiji(smjerIDs, name, minECTS, maxECTS, isvu, skip, take) {
    return await axios.get(`${ROUTING.baseRoute}Kolegij`, {
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
    return await axios.get(`${ROUTING.baseRoute}Kolegij/Vijesti/${kolegijId}`, {
      params: {
        skip,
        take
      }
    });
  },
  async getKolegijStudents(kolegijId, skip, take) {
    return await axios.get(`${ROUTING.baseRoute}Kolegij/Students/${kolegijId}`, {
      params: {
        skip,
        take
      }
    })
  },
  async getKolegijByPreplate(studentId) {
    return await axios.get(`${ROUTING.baseRoute}Kolegij/my/${studentId}`);
  },
  async getKolegijSidebarContent(kolegijId) {
    return await axios.get(`${ROUTING.baseRoute}Kolegij/SidebarContent/${kolegijId}`);
  },
  async connectSidebarFile(sidebarContentId, fileIDs) {
    return await axios.post(`${ROUTING.baseRoute}Kolegij/ConnectSidebarFile`, { 
      sidebarContentId,
      fileIDs 
    });
  }
}