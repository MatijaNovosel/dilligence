<template>
  <div>
    
  </div>
</template>

<script>
  import PretplataService from '../services/api/pretplata';
  import { mapGetters } from 'vuex';
  
  export default { 
    data() {
      return {
        
      }
    },
    created() {
      PretplataService.getPretplata().then((response) => {
        console.log(response);
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