<template>
  <div>
    <v-row no-gutters>
      <v-spacer />
      <v-col cols="3">
        <v-row>
          <v-img contain src="../../assets/tvz-logotype.svg"></v-img>
        </v-row>
      </v-col>
      <v-spacer />
    </v-row>
    <v-row justify="center" class="mt-5">
      <v-col cols="3">
        <v-row>
          <v-text-field label="Username"> </v-text-field>
        </v-row>
        <v-row>
          <v-text-field label="Password"> </v-text-field>
        </v-row>
        <v-row class="mt-5" align-content="center" align="center">
          <v-spacer />
          <v-btn color="primary">Sign in</v-btn>
          <v-spacer />
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>

export default { 
  data() {
    return {
      counter: 0,
      reverse: false
    }
  },
  methods: {
  }
};

</script>