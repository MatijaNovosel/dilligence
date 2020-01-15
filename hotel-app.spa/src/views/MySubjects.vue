<template>
  <div>
    <v-row justify="center">
      <v-col cols="6">
        <v-row>
          <template v-for="subject in subjects">
            <v-col cols="6" :key="subject.id + subject.naziv">
              <v-skeleton-loader class="mx-auto" type="card" :loading="cardsLoading">
                <subject-card :subject="subject" />
              </v-skeleton-loader>
            </v-col>
          </template>
        </v-row>
      </v-col>
      <v-col cols="6">
        <v-row>
          <v-col cols="12" class="display-1 text-center font-weight-light mb-n4">
            Vijesti
          </v-col>
          <v-col cols="12">
            <v-timeline dense>
              <v-timeline-item v-for="n in 4" :key="n" small fill-dot>
                <template v-slot:icon>
                  <v-avatar size="35" color="red" class="elevation-2">
                    <v-icon dark>
                      mdi-exclamation-thick
                    </v-icon>
                  </v-avatar>
                </template>
                <v-card outlined>
                  <v-card-title class="headline bg-header white--text"> Vijest </v-card-title>
                  <v-divider />
                  <v-card-text>Lorem ipsum dolor sit amet, no nam oblique veritus. Commune scaevola imperdiet nec ut, sed euismod convenire principes at. Est et nobis iisque percipit, an vim zril disputando voluptatibus, vix an salutandi sententiae.</v-card-text>
                </v-card>
              </v-timeline-item>
            </v-timeline>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>
  import KolegijService from '../services/api/kolegij';
  import SubjectCard from '../components/SubjectCard';
  import { mapGetters } from 'vuex';
  
  export default { 
    components: {
      SubjectCard
    },
    data() {
      return {
        subjects: [],
        cardsLoading: true
      }
    },
    created() {
      this.getData();
    },
    methods: {
      getData() {
        this.cardsLoading = true;
        KolegijService.getKolegijByPreplate(this.user.id)
        .then(({ data }) => {
          this.subjects = data.results;
        })
        .finally(() => {
          this.cardsLoading = false;
        });
      }
    },
    computed: {
      ...mapGetters([
        'user'
      ])
    }
  };
</script>

<style scoped>

  .bg-header {
    background-image: url("../assets/nav-bg.svg");
    background-position: bottom;
    background-size: cover;
  }

</style>