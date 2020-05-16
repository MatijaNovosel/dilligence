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
     * @param {...number} requestedPrivileges - Variable amount of numbers representing the requested privileges.
     * @description
     * Checks if the current user has the requested privileges, not contained within course specific privileges.
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
      return this
        .user
        .privileges
        .courses
        .some(course => course.id == courseId && course.privileges.some(privilege => requestedPrivileges.includes(privilege)));
    }
  }
}