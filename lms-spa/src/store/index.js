import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'

Vue.use(Vuex)

const Store = new Vuex.Store({
  modules: {},
  state: {
    user: {
      id: null,
      token: null,
      name: null,
      surname: null,
      JMBAG: null,
      privileges: {
        generalPrivileges: null,
        course: null
      },
      picture: null,
      settings: {
        darkMode: null,
        locale: null,
        popups: null
      }
    }
  },
  mutations: {
    SET_USER_DATA(state, user) {
      state.user = user;
    },
    ADD_NEW_PRIVELEGE(state, privelegeInfo) {
      state.user.privileges.courses.push({ id: privelegeInfo.courseId, privileges: privelegeInfo.privileges });
    },
    REMOVE_PRIVELEGE(state, courseId) {
      console.log(state.user);
      state.user.privileges.courses = state.user.privileges.courses.filter(x => x.id != courseId);
    },
    REMOVE_USER_DATA(state) {
      state.user = {
        id: null,
        token: null,
        name: null,
        surname: null,
        JMBAG: null,
        privileges: {
          generalPrivileges: null,
          course: null
        },
        picture: null,
        settings: {
          darkMode: null,
          locale: null,
          popups: null
        }
      };
    }
  },
  actions: {
    addNewPrivelege({ commit }, privelegeInfo) {
      commit('ADD_NEW_PRIVELEGE', privelegeInfo);
    },
    removePrivelege({ commit }, courseId) {
      commit('REMOVE_PRIVELEGE', courseId);
    },
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
  plugins: [createPersistedState()],
  strict: process.env.DEV
})

export default Store;
