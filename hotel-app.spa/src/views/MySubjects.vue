<template>
  <div>
    <template v-for="subject in subjects">
      <div :key="subject.id + subject.naziv">
        <p> {{ subject.naziv }} </p>
      </div>
    </template>
  </div>
</template>

<script>
  import KolegijService from '../services/api/kolegij';
  import { mapGetters } from 'vuex';
  
  export default { 
    data() {
      return {
        subscriptions: [],
        subjects: []
      }
    },
    created() {
      KolegijService.getKolegijByPreplate(this.user.id)
      .then((response) => {
        this.subjects = response.data.results;
      }); 
    },
    methods: {
      
    },
    computed: {
      ...mapGetters([
        'user'
      ])
    }
  };
</script>