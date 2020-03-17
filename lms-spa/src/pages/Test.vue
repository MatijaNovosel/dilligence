<template>
	<q-page>
		<q-btn @click="triggerEvent">CLICK HERE FOR EVENT</q-btn>
		<template v-for="content in sidebarContents">
			<FileCabinet :key="content.id" :content="content"></FileCabinet>
		</template>
	</q-page>
</template>

<script>
import FileCabinet from "../components/FileCabinet";
import KolegijService from "../services/api/kolegij";
import {
	HubConnectionBuilder,
	LogLevel,
	HttpTransportType
} from "@aspnet/signalr";
import apiConfig from "../api.config";

export default {
	name: "Test",
	components: {
		FileCabinet
	},
	created() {
		const connection = new HubConnectionBuilder()
			.withUrl("http://localhost:5000/vijesti-hub")
			.configureLogging(LogLevel.Information)
			.build();
		connection.start();
		connection.on("EVENT", () => {
			console.log("Kur-cina");
		});
		KolegijService.getKolegijSidebar(147).then(({ data }) => {
			this.sidebarContents = data;
		});
	},
	methods: {
		triggerEvent() {
			this.$axios.get("Vijest/147");
		}
	},
	data() {
		return {
			sidebarContents: null
		};
	}
};
</script>
