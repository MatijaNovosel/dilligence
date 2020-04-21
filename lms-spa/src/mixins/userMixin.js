import { mapGetters } from "vuex";
import Privileges from "../constants/privileges";

export default {
  created() {
    this.Privileges = Privileges;
  },
  data() {
    Privileges: null
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    /**
     * Checks if the current user has the requested privileges.
     * @param {...number} requestedPrivileges - Variable amount of numbers representing the requested privileges.
     * @return {boolean} A boolean indicating if the user has said privileges or not.
     */
    checkPrivileges(...requestedPrivileges) {
      return this.user.privileges.some(privilege => requestedPrivileges.includes(privilege));
    }
  }
}