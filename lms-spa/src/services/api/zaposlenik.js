export default {
  async getZaposlenik(id) {
    return await this.$axios.get(`Zaposlenik/${id}`);
  },
  async getZaposlenici(name, surname, odjel, vrstaZaposljenja, skip, take) {
    return await this.$axios.get('Zaposlenik', {
      params: {
        name,
        surname,
        odjel,
        vrstaZaposljenja,
        skip,
        take
      }
    });
  }
}