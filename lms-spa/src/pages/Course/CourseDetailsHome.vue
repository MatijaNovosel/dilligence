<template>
	<div>
		<div class="absolute-top-right">
			<q-btn size="sm" flat>Edit landing page</q-btn>
		</div>
		<div
			v-html="landingPage"
		>This is the landing page of the course, make sure to keep it pretty for the students!</div>
	</div>
</template>

<script>
import CourseService from "../../services/api/course";

export default {
	name: "CourseDetailsHome",
	created() {
		this.courseId = this.$route.params.id;
		this.getLandingPage();
	},
	methods: {
		getLandingPage() {
			CourseService.getLandingPage(this.courseId).then(({ data }) => {
				this.landingPage = data;
			});
		}
	},
	data() {
		return {
			courseId: null,
			landingPage: ""
		};
	}
};
</script>
