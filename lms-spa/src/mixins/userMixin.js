import { mapGetters, mapActions } from "vuex";
import Privileges from "../constants/privileges";
import UserService from "../services/api/user";

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
    ...mapActions(["setUserData"]),
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
      if (this.user.privileges.generalPrivileges == null) {
        return;
      }
      return this
        .user
        .privileges
        .generalPrivileges
        .some(privilege => requestedPrivileges.includes(privilege));
    },
    hasCoursePrivileges(courseId, ...requestedPrivileges) {
      if (this.user.privileges.courses == null) {
        return;
      }
      return this
        .user
        .privileges
        .courses
        .some(course => course.id == courseId && course.privileges.some(privilege => requestedPrivileges.includes(privilege)));
    },
    reinitPrivileges() {
      let user = { ...this.user };
      UserService.getUserDetails(this.user.id).then(({ data }) => {
        user.privileges = data.privileges;
        this.setUserData(user);
      });
    }
  }
}