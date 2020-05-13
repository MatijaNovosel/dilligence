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
     * @example
     *  checkPrivileges(Privileges.CanInviteUsers, Privileges.CanViewSubjects);
     *  checkPrivileges(1, 2, 3, 4);
     */
    hasPrivileges(...requestedPrivileges) {
      return this
        .user
        .privileges
        .generalPrivileges
        .some(privilege => requestedPrivileges.includes(privilege));
    },
    hasCoursePrivileges(courseId, ...requestedPrivileges) {
      if (!this.user) {
        return false;
      }
      return this
        .user
        .privileges
        .courses
        .some(course => course.id == courseId && course.privileges.some(privilege => requestedPrivileges.includes(privilege)));
    }
  }
}