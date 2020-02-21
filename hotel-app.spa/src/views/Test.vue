<template>
	<div>
		<v-row no-gutters justify="center">
			<v-col cols="12" class="text-center">
				<h1>Upload test</h1>
			</v-col>
			<v-col cols="12">
				<v-skeleton-loader class="mx-auto" type="card" max-width="40%" :loading="loading">
					<template v-for="content in sidebarContent">
						<file-cabinet
							:key="content.id"
							@doneUploading="getData"
							:content="content"
							headerColor="primary"
						/>
					</template>
				</v-skeleton-loader>
			</v-col>
		</v-row>
	</div>
</template>

<script>
import KolegijService from "../services/api-cqrs/kolegij";
import FileCabinet from "../components/FileCabinet";

export default {
	components: {
		FileCabinet
	},
	data() {
		return {
			sidebarContent: [],
			loading: null
		};
	},
	methods: {
		getData() {
      this.loading = true;
			KolegijService.getKolegijSidebar(147)
				.then(({ data }) => {
					data.forEach(sidebar =>
						sidebar.files.forEach(file => (file.downloading = false))
					);
					this.sidebarContent = data;
				})
				.finally(() => {
          this.loading = false;
        });
		},
		loadingFinished() {
			this.loading = false;
		}
	},
	created() {
		this.getData();
	}
};
</script>