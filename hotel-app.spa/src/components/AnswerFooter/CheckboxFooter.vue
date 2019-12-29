<template>
  <div>
    <v-row dense no-gutters>
      <v-col v-for="answer in answers" :key="answer.id" cols="12" class="my-n4">
        <v-checkbox :label="answer.content" color="primary"> </v-checkbox>
      </v-col>
    </v-row>
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