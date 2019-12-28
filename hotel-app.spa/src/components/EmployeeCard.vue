<template>
  <div>
    <v-card width="225" class="ma-3">
      <v-avatar size="135" class="ml-11 avatar-border">
        <v-img v-if="employeeData.ime + ' ' + employeeData.prezime == 'Željko Kovacevic'" src="../assets/TVZ/djelatnici/kova.png" />
        <v-img v-else-if="employeeData.ime + ' ' + employeeData.prezime == 'Tin Kramberger'" src="../assets/TVZ/djelatnici/tin.png" />
        <v-img v-else src="../assets/default-user.jpg" />
      </v-avatar>
      <v-btn icon class="gore-desno">
        <v-icon>
          mdi-dots-vertical
        </v-icon>
      </v-btn>
      <v-row justify="center" class="mt-3">
        <h4 class="caption font-weight-light"> 
          {{ employeeData.titulaIspred || '-' }} 
        </h4>
      </v-row>
      <v-row justify="center">
        <h4 class="subtitle-1 font-weight-light"> 
          {{ `${employeeData.ime} ${employeeData.prezime}` }} 
        </h4>
      </v-row>
      <v-row justify="center" class="mb-3">
        <h4 class="caption font-weight-light"> 
          {{ employeeData.titulaIza || '-' }} 
        </h4>
      </v-row>
      <v-divider />
      <v-list two-line>
        <v-list-item class="my-n4">
          <v-list-item-icon>
            <v-icon color="indigo" class="pt-3">
              mdi-email
            </v-icon>
          </v-list-item-icon>
          <v-list-item-content class="ml-n3">
            <v-list-item-title>
              <v-tooltip top>
                <template v-slot:activator="{ on }">
                  <span v-on="on"> {{ employeeData.email }} </span>
                </template>
                <span> {{ employeeData.email }} </span>
              </v-tooltip>
            </v-list-item-title>
            <v-list-item-subtitle>
              Email
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-card>
  </div>
</template>

<script>

export default { 
  props: [ 'employeeData' ],
  data() {
    return {
      
    }
  },
  methods: {
    
  }
};

</script>

<style scoped>
  .avatar-border {
    border: 1px solid #d1d1c2;
  }
  .gore-desno {
    position: absolute;
    right: 5px;
    top: 5px;
  }
</style>