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
    /*

      @Input -> variable amount of parameters, represents requested privileges e.g. f(1, 2, 3, 4)
      @Output -> boolean, does the user have the requested privileges

    */
    checkPrivileges(...requestedPrivileges) {
      return this.user.privileges.some(privilege => requestedPrivileges.includes(privilege));
    }
  }
}