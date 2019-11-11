import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import Cookies from 'js-cookie'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
  state: {
    user: {
      id: null,
      username: null,
      token: null
    },
    decodedToken: null
  },
  mutations: {
    SET_USER_DATA(state, user) {
      state.user = user;
    },
    SET_DECODED_TOKEN(state, token) {
      state.decodedToken = token;
    },
    REMOVE_USER_DATA(state) {
      state.user = {
        id: null,
        username: null,
        token: null  
      };
    }
  },
  actions: {
    setUserData({ commit }, user) {
      commit('SET_USER_DATA', user);
    },
    setDecodedToken({ commit }, token) {
      commit('SET_DECODED_TOKEN', token);  
    },
    removeUserData({ commit }) {
      commit('REMOVE_USER_DATA');  
    }
  },
  getters: {
    user: state => state.user,
    decodedToken: state => state.decodedToken
  },
  strict: debug,
  plugins: [
    createPersistedState({
      getState: (key) => Cookies.getJSON(key),
      setState: (key, state) => Cookies.set(key, state, { expires: 3, secure: true })
    })
  ]
})