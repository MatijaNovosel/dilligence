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
    checkPrivileges(...requestedPrivileges) {
      return this.user.privileges.some(privilege => requestedPrivileges.includes(privilege));
    }
  }
}