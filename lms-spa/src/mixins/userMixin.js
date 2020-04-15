import { mapGetters } from "vuex";
import Privileges from "../constants/privileges";

export default {
  data() {
    Privileges
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    checkPrivileges(...requestedPrivileges) {
      return this.user.privileges.some(privilege => requestedPrivileges.contains(privilege));
    }
  }
}