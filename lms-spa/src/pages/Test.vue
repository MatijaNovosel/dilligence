<template>
	<q-page>
		<template v-for="content in sidebarContents">
			<FileCabinet :key="content.id" :content="content"></FileCabinet>
		</template>
	</q-page>
</template>

<script>
import FileCabinet from "../components/FileCabinet";
import KolegijService from "../services/api/kolegij";

export default {
	name: "Test",
	components: {
		FileCabinet
	},
	created() {
		KolegijService.getKolegijSidebar(147).then(({ data }) => {
			this.sidebarContents = data;
		});
	},
	data() {
		return {
			sidebarContents: null
		};
	}
};
</script>
