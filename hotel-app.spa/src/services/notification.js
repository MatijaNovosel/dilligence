import Vue from 'vue'

// warn, error, success

export default {
  error(title, message) {
    Vue.notify({
      group: 'notification',
      type: 'error',
      title: title,
      text: message
    })
  },
  success(title, message) {
    Vue.notify({
      group: 'notification',
      type: 'success',
      title: title,
      text: message
    })
  },
  warning(title, message) {
    Vue.notify({
      group: 'notification',
      type: 'warn',
      title: title,
      text: message
    })
  }
}