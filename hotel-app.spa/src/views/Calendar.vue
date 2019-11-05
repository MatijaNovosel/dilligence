<template>
  <div>
    <v-row dense justify="center" class="mt-5">
      <v-col cols="3">
        <v-card max-width="400" class="mx-auto" outlined>
          <v-toolbar color="red darken-3" dark class="elevation-1">
            <v-toolbar-title> {{ this.today }} </v-toolbar-title>
            <div class="flex-grow-1"></div>
            <v-btn small color="green">
              <v-icon small class="mr-1">mdi-arrow-top-right-thick</v-icon>
              200 kn
            </v-btn>
            <v-btn small color="red" class="ml-3">
              <v-icon small class="mr-1">mdi-arrow-bottom-right-thick</v-icon>
              200 kn
            </v-btn>
          </v-toolbar>
          <v-list two-line>
            <template v-for="(item, index) in items">
              <v-list-item :key="item.title">
                <template v-slot:default="{ active, toggle }">
                  <v-btn x-small fab class="mr-4 elevation-1" dark color="red">
                    <v-icon>mdi-close</v-icon>
                  </v-btn>
                  <v-list-item-content>
                    <v-list-item-title v-text="item.title"></v-list-item-title>
                    <v-list-item-subtitle class="text--primary" v-text="item.headline"></v-list-item-subtitle>
                    <v-list-item-subtitle v-text="item.subtitle"></v-list-item-subtitle>
                  </v-list-item-content>
                  <v-list-item-action>
                    <v-list-item-action-text v-text="item.action"></v-list-item-action-text>
                    <v-icon color="grey lighten-1">
                      mdi-food
                    </v-icon>
                  </v-list-item-action>
                </template>
              </v-list-item>
              <v-divider v-if="index + 1 < items.length" :key="index" />
            </template>
          </v-list>
          <v-divider></v-divider>
          <v-card-actions class="my-2 ml-3">
              Total: <b class="ml-2"> 250kn </b>
              <div class="flex-grow-1"></div>
              <v-btn small dark color="info" class="ml-3">
                Save
              </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
      <v-col cols="6">
        <v-card class="mx-auto">
          <v-system-bar dark color="red darken-1" height="10" />
          <v-list-item>
            <v-list-item-avatar color="red darken-1">
              <v-icon color="white">
                mdi-calendar
              </v-icon>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline">Calendar</v-list-item-title>
              <v-list-item-subtitle>Daily spendings</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
          <v-divider class="mb-2" />
          <v-divider />
          <v-sheet height="400">
            <v-calendar ref="calendar" type="month"></v-calendar>
          </v-sheet>
          <v-card-actions>
            <div class="flex-grow-1"></div>
            <v-btn icon @click="$refs.calendar.prev()">
              <v-icon dark>mdi-chevron-left</v-icon>
            </v-btn>
            <v-btn icon @click="$refs.calendar.next()">
              <v-icon dark>mdi-chevron-right</v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
      <v-col cols="3">
        <v-card max-width="375" class="mx-auto" outlined>
          <v-system-bar dark color="pink" height="10" />
          <v-list-item>
            <v-list-item-avatar color="pink">
              <v-icon color="white">
                mdi-currency-usd
              </v-icon>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline">Žiro račun</v-list-item-title>
              <v-list-item-subtitle>Trenutačno stanje</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
          <v-img class="white--text" src="https://ak0.picdn.net/shutterstock/videos/1023423880/thumb/1.jpg" height="125">  
            <v-card-title class="fill-height justify-center display-1">
              {{ this.gyro }} kn
            </v-card-title> 
          </v-img>
          <v-card-actions>
            <div class="flex-grow-1"></div>
            <v-btn icon>
              <v-icon>mdi-eye</v-icon>
            </v-btn>
            <v-btn icon>
              <v-icon>mdi-share-variant</v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
        <v-card max-width="375" class="mx-auto mt-5" outlined>
          <v-system-bar dark color="red" height="10" />
          <v-list-item>
            <v-list-item-avatar color="red">
              <v-icon color="white">
                mdi-currency-usd
              </v-icon>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline">Tekući račun</v-list-item-title>
              <v-list-item-subtitle>Trenutačno stanje</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
          <v-img class="white--text" src="https://ak0.picdn.net/shutterstock/videos/1023423880/thumb/1.jpg" height="125">  
            <v-card-title class="fill-height justify-center display-1">
              {{ this.main }} kn
            </v-card-title> 
          </v-img>
          <v-card-actions>
            <div class="flex-grow-1"></div>
            <v-btn icon>
              <v-icon>mdi-eye</v-icon>
            </v-btn>
            <v-btn icon>
              <v-icon>mdi-share-variant</v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
        <v-card max-width="375" class="mx-auto mt-5" outlined>
          <v-system-bar dark color="orange" height="10" />
          <v-list-item>
            <v-list-item-avatar color="orange">
              <v-icon color="white">
                mdi-currency-usd
              </v-icon>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline">Tekući račun</v-list-item-title>
              <v-list-item-subtitle>Trenutačno stanje</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
          <v-img class="white--text" src="https://ak0.picdn.net/shutterstock/videos/1023423880/thumb/1.jpg" height="125">  
            <v-card-title class="fill-height justify-center display-1">
              {{ this.pocket }} kn
            </v-card-title> 
          </v-img>
          <v-card-actions>
            <div class="flex-grow-1"></div>
            <v-btn icon>
              <v-icon>mdi-eye</v-icon>
            </v-btn>
            <v-btn icon>
              <v-icon>mdi-share-variant</v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
        <v-card max-width="375" class="mx-auto mt-5" outlined>
          <v-system-bar dark color="red accent-3" height="10" />
          <v-list-item>
            <v-list-item-avatar color="red accent-3">
              <v-icon color="white">
                mdi-currency-usd
              </v-icon>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline">Ukupan iznos</v-list-item-title>
              <v-list-item-subtitle>Trenutačno stanje</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
          <v-img class="white--text" src="https://ak0.picdn.net/shutterstock/videos/1023423880/thumb/1.jpg" height="125">  
            <v-card-title class="fill-height justify-center display-1">
              {{ this.gyro + this.main + this.pocket }} kn
            </v-card-title> 
          </v-img>
          <v-card-actions>
            <div class="flex-grow-1"></div>
            <v-btn icon>
              <v-icon>mdi-eye</v-icon>
            </v-btn>
            <v-btn icon>
              <v-icon>mdi-share-variant</v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>

export default { 
  data() {
    return {
      gyro: 0,
      main: 0,
      pocket: 0,
      today: null,
      selected: [2],
      items: [{
        action: 'Food',
        headline: 'Menza',
        subtitle: "6.5kn",
      }, {
        action: 'Food',
        headline: 'Menza',
        subtitle: "6.5kn",
      }, {
        action: 'Food',
        headline: 'Menza',
        subtitle: "6.5kn",
      }, {
        action: 'Food',
        headline: 'Menza',
        subtitle: "6.5kn",
      }, {
        action: 'Food',
        headline: 'Menza',
        subtitle: "6.5kn",
      }, {
        action: 'Food',
        headline: 'Menza',
        subtitle: "6.5kn",
      }, {
        action: 'Food',
        headline: 'Menza',
        subtitle: "6.5kn",
      }, {
        action: 'Food',
        headline: 'Menza',
        subtitle: "6.5kn",
      }]
    }
  },
  methods: {

  },
  created() {
    this.today = new Date().toJSON().slice(0,10).replace(/-/g,'/');
  }
};

</script>