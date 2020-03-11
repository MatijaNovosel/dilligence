export default {
  async getKorisnik(id) {
    return await this.$axios.get(`Korisnik/${id}`);
  }
}