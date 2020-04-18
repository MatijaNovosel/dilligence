import axios from "axios";

export default {
  async searchKorisnik(name) {
    return await axios.get("Korisnik", {
      params: {
        name
      }
    });
  },
  async getChats(id) {
    return await axios.get("Korisnik/chat/" + id);
  },
  async getSubscriptions(id) {
    return await axios.get(`Korisnik/pretplata/${id}`);
  },
  async subscribe(password, userId, kolegijId) {
    return await axios.put('Korisnik', {
      password,
      userId,
      kolegijId
    });
  },
  async unsubscribe(userId, kolegijId) {
    return await axios.put('Korisnik/unsubscribe', {
      userId,
      kolegijId
    });
  },
  async updateSettings(userId, settings) {
    return await axios.put('Korisnik/settings', {
      userId,
      darkMode: settings.darkMode,
      locale: settings.locale
    });
  }
};
