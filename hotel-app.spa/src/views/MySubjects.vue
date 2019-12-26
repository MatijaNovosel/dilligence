<template>
  <div>
    <template v-for="subscription in subscriptions">
      <div :key="subscription">
        {{ subscription }}
      </div>
    </template>
  </div>
</template>

<script>
  import PretplataService from '../services/api/pretplata';
  import { mapGetters } from 'vuex';
  
  export default { 
    data() {
      return {
        subscriptions: []
      }
    },
    created() {
      PretplataService.getPretplata(this.user.id)
      .then((response) => {
        this.subscriptions = response.data;
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