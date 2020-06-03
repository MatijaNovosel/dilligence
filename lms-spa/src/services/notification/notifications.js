import store from "../../store/index";
import { Notify } from 'quasar'

export default {
  showSuccess(message) {
    if (store.getters.user.id != null && !store.getters.user.settings.popups) {
      return;
    }
    Notify.create({
      type: "positive",
      message
    });
  },
  showError(message) {
    if (store.getters.user.id != null && !store.getters.user.settings.popups) {
      return;
    }
    Notify.create({
      type: "negative",
      message
    });
  }
}