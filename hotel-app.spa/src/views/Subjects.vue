<template>
	<div>
		<v-row no-gutters justify="center">
			<v-col cols="5">
				<v-row class="mt-2" justify="center">
					<v-chip-group
						v-model="searchData.smjerIDs"
						mandatory
						multiple
						column
						active-class="white--text blue darken-2"
					>
						<v-chip v-for="item in tags" :key="item.value">{{ item.name }}</v-chip>
					</v-chip-group>
				</v-row>
			</v-col>
		</v-row>
		<v-row no-gutters>
			<v-col>
				<v-row class="mt-5">
					<v-card width="75%" class="mx-auto" :loading="loading">
						<v-toolbar dark flat dense color="primary">
							<v-icon dark class="mr-2">mdi-text-subject</v-icon>
							<v-toolbar-title>Kolegiji</v-toolbar-title>
							<v-spacer></v-spacer>
							<v-btn icon @click="searchEnabled = !searchEnabled">
								<v-icon>mdi-filter-menu</v-icon>
							</v-btn>
							<v-btn icon @click="resetForm">
								<v-icon>mdi-filter-remove</v-icon>
							</v-btn>
							<v-btn icon @click="getData" class="mr-4">
								<v-icon>mdi-magnify</v-icon>
							</v-btn>
						</v-toolbar>
						<v-row class="search-bg" justify="center" v-show="searchEnabled">
							<v-col>
								<v-text-field label="Ime kolegija" v-model="searchData.name"></v-text-field>
							</v-col>
							<v-col>
								<v-text-field label="ISVU" v-model="searchData.ISVU"></v-text-field>
							</v-col>
							<v-col>
								<v-range-slider
									v-model="searchData.ECTS"
									label="ECTS"
									min="1"
									max="6"
									class="mt-6"
									thumb-label="always"
									thumb-size="24"
								></v-range-slider>
							</v-col>
						</v-row>
						<v-list three-line subheader>
							<v-divider />
							<template v-for="(item, i) in subjects">
								<v-list-item :key="item.naziv + item.id">
									<v-list-item-avatar
										@click="redirectToKolegijDetails(item)"
										size="50"
										color="primary"
										class="justify-center"
									>
										<span class="white--text headline">{{ acronym(item.naziv) }}</span>
									</v-list-item-avatar>
									<v-list-item-content>
										<v-list-item-title v-html="item.naziv"></v-list-item-title>
										<v-list-item-subtitle>
											<b>ECTS:</b>
											{{ item.ects }}
										</v-list-item-subtitle>
										<v-list-item-subtitle>
											<b>Smjer:</b>
											{{ item.smjer }}
										</v-list-item-subtitle>
									</v-list-item-content>
									<v-list-item-action class="mt-8 mr-7" v-if="subscriptions.includes(item.id)">
										<v-icon color="primary">mdi-bookmark-check</v-icon>
									</v-list-item-action>
									<v-list-item-action class="mt-7 mr-5" v-else>
										<v-btn icon @click="subscriptionDialog = true">
											<v-icon color="grey">mdi-bookmark-check</v-icon>
										</v-btn>
									</v-list-item-action>
								</v-list-item>
								<v-divider :key="i" v-if="i < subjects.length - 1" />
							</template>
						</v-list>
					</v-card>
				</v-row>
			</v-col>
		</v-row>
		<v-dialog v-model="subscriptionDialog" width="50%" persistent>
			<v-system-bar height="30" dark color="primary">
				<v-spacer />
				<v-btn icon @click="resetDialog">
					<v-icon dark>mdi-close</v-icon>
				</v-btn>
			</v-system-bar>
			<v-card>
				<v-card-text>
					<v-row class="justify-center pt-5">
						To subscribe to this course, you need to know the password!
					</v-row>
          <v-row class="justify-center">
            <v-col cols="6">
              <v-text-field dense type="password" v-model="password" solo label="Password" />
            </v-col>
          </v-row>
          <v-row class="justify-center pb-5">
						<v-btn color="primary" @click="subscribe">
              Confirm
            </v-btn>
					</v-row>
				</v-card-text>
			</v-card>
		</v-dialog>
	</div>
</template>

<script>
import KolegijService from "../services/api/kolegij";
import StudentService from "../services/api/student";
import { Smjer } from "../constants/Smjer";
import { acronym } from "../helpers/helpers.js";
import { mapGetters } from "vuex";

export default {
	data() {
		return {
			password: null,
			subscriptions: [],
			subjects: [],
			totalSubjects: 0,
			loading: null,
			subscriptionDialog: false,
			tags: [],
			searchEnabled: true,
			searchData: {
				smjerIDs: [Smjer["Informatika"] - 1],
				name: null,
				ECTS: [1, 6],
				ISVU: null
			}
		};
	},
	created() {
		for (let prop in Smjer) {
			this.tags.push({
				name: prop,
				value: Smjer[prop]
			});
		}
		this.getData();
	},
	methods: {
    acronym,
    subscribe(kolegijId) {
      
    },
		getData() {
			this.loading = true;
			KolegijService.get({
				smjerIds: this.searchData.smjerIDs.map(x => x + 1),
				name: this.searchData.name,
				minEcts: this.searchData.ECTS[0],
				maxEcts: this.searchData.ECTS[1],
				isvu: this.ISVU
			})
				.then(({ data }) => {
					[this.subjects, this.totalSubjects] = [data.results, data.total];
				})
				.finally(() => {
					this.loading = false;
				});
			StudentService.getPretplata(this.user.id).then(({ data }) => {
				this.subscriptions = data;
			});
		},
		buildPath(name) {
			return require(`../assets/TVZ/subjects/${name}.png`);
		},
		showInfo(item) {
			this.selectedItem = item;
			this.dialog = !this.dialog;
		},
		redirectToKolegijDetails(item) {
			this.$router.push({
				name: "subject-details",
				params: { id: item.id }
			});
		},
		resetForm() {
			this.searchData = {
				smjerIDs: [Smjer["Informatika"] - 1],
				name: null,
				ECTS: [1, 6],
				ISVU: null
			};
			this.getData();
		},
		resetDialog() {
			this.password = null;
			this.subscriptionDialog = false;
		}
	},
	computed: {
		...mapGetters(["user"])
	}
};
</script>

<style scoped>
.v-avatar:hover {
	cursor: pointer;
	background-color: #292826 !important;
}
.search-bg {
	background-color: #f5f2f2 !important;
	margin-left: 0px;
	margin-right: 0px;
}
</style>