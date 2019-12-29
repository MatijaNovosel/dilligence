<template>
  <div>
    <v-radio-group column>
      <v-radio v-for="answer in answers" :key="answer.id" :label="answer.content" color="primary" :value="answer.id">
      </v-radio>
    </v-radio-group>
  </div>
</template>

<script>

  export default {
    props: [ 'answers' ],
    data() {
      return {
        
      }
    }
  }
  
</script>