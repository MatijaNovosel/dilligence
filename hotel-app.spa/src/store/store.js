import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
  state: {
    user: {
      id: null,
      token: null,
      name: null,
      surname: null,
      JMBAG: null
    }
  },
  mutations: {
    SET_USER_DATA(state, user) {
      state.user = user;
    },
    REMOVE_USER_DATA(state) {
      state.user = {
        id: null,
        token: null,
        name: null,
        surname: null,
        JMBAG: null
      };
    }
  },
  actions: {
    setUserData({ commit }, user) {
      commit('SET_USER_DATA', user);
    },
    removeUserData({ commit }) {
      commit('REMOVE_USER_DATA');  
    }
  },
  getters: {
    user: state => state.user
  },
  strict: debug,
  plugins: [
    createPersistedState()
  ]
});