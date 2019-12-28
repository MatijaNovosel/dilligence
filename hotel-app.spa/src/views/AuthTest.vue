<template>
  <div>
      <v-row no-gutters>
        <v-col>
          <v-row class="mt-10" justify="center">
            <h1> Dobrodošli na intranet stranice Tehničkog veleučilišta u Zagrebu </h1>
          </v-row>
        </v-col>
      </v-row>
  </div>
</template>

<script>

import KorisnikService from '../services/api/korisnik';

export default { 
  data() {
    return {
      
    }
  },
  methods: {
  },
  created() {
    KorisnikService.getKorisnik(1)
    .then((response) => {
      console.log(response);  
    });
  }
};

</script>