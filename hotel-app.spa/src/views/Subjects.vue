<template>
  <div>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-2" justify="center">
          <v-chip-group v-model="chipSelection" mandatory multiple column active-class="white--text blue darken-2">
            <v-chip v-for="item in tags" :key="item.value">
              {{ item.name }}
            </v-chip>
          </v-chip-group>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-5">
          <v-card width="75%" class="mx-auto" :loading="loading">
            <v-toolbar dark flat color="primary">
              <v-icon dark class="mr-2">
                mdi-text-subject
              </v-icon>
              <v-toolbar-title> Subjects </v-toolbar-title>
            </v-toolbar>
            <v-list three-line subheader>
              <v-divider></v-divider>
              <template v-for="(item, i) in subjects">
                <v-list-item :key="item.id" router :to="{ name: 'subject-details', params: { id: item.id }}">
                  <v-list-item-avatar size="50" :color="randColor()" class="justify-center">
                    <span class="white--text headline"> {{ acronym(item.naziv) }} </span>
                  </v-list-item-avatar>
                  <v-list-item-content>
                    <v-list-item-title v-html="item.naziv"></v-list-item-title>
                    <v-list-item-subtitle><b>ECTS: </b>{{ item.ects }}</v-list-item-subtitle>
                    <v-list-item-subtitle><b>ISVU: </b>{{ item.isvu }}</v-list-item-subtitle>
                  </v-list-item-content>
                  <v-list-item-action class="mt-8 mr-5">
                    <v-icon>mdi-eye</v-icon>
                  </v-list-item-action>
                </v-list-item>
                <v-divider :key="i + subjects.length + 1" v-if="i < subjects.length - 1" />
              </template>
            </v-list>
          </v-card>
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import KolegijService from '../services/api/kolegij'; 
import { SmjerInformation } from '../constants/smjerInformation';

export default { 
  data() {
    return {
      subjects: [],
      totalSubjects: 0,
      loading: true,
      chipSelection: [ SmjerInformation.inf ],
      replacementSubjects: [],
      tags: []
    }
  },
  mounted() {
    this.getData();
    for(let prop in SmjerInformation) {
      this.tags.push({
        "name": prop,
        "value": SmjerInformation[prop]
      });
    }
  },
  methods: {
    getData() {
      this.loading = true;
      KolegijService.getKolegijBySmjerID(this.chipSelection, 0, null)
      .then(({ data }) => {
        this.subjects = data.results;
        this.replacementSubjects = data.results;
        this.totalSubjects = data.total;
      }).finally(() => {
        this.loading = false;
      });
    },
    acronym: function(s) {
        var words, acronym, nextWord;
        words = s.split(' ');
        acronym = "";
        var index = 0;
        while(index < words.length) {
          nextWord = words[index];
          acronym = acronym + nextWord.charAt(0);
          index++;
        }
        return acronym.slice(0, 2).toUpperCase();
    },
    randInt: function(min, max) {
      return Math.floor(Math.random() * (max - min + 1) ) + min;
    },
    randColor: function() {
      var color = `rgb(${this.randInt(0, 255)}, ${this.randInt(0, 255)}, ${this.randInt(0, 255)})`;
      return color;
    },
    buildPath: function(name) {
      return require(`../assets/TVZ/subjects/${name}.png`);
    },
    showInfo: function(item) {
      this.selectedItem = item;
      this.dialog = !this.dialog;
    }
  },
  watch: {
    chipSelection: function() {
      this.getData();
    }
  }
};

</script>