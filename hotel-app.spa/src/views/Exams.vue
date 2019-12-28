<template>
  <div>
    <v-fade-transition>
      <div class="code-container">
        <v-sheet class="code d-flex grey--text text--lighten-3 pa-4" 
                 :color="background" 
                 @mouseenter="showCopy = true" 
                 @mouseleave="showCopy = false">
          <v-fade-transition>
            <v-btn v-if="showCopy" text icon class="copy white--text" @click="copyCode">
              <v-icon>
                mdi-content-copy
              </v-icon>
            </v-btn>
          </v-fade-transition>
          <v-layout column>
            <v-flex v-for="(row, index) of rows"
                    :key="index"
                    :class="getClass(index)"
                    @mouseenter="rowEntered(index)"
                    @mouseleave="rowLeft(index)"
                    @click="rowClicked(index)">
              <span class="orange--text text--lighten-3 mr-3 code-row">{{ getRowText(index + 1) }}</span>
              <span>{{ row }}</span>
            </v-flex>
          </v-layout>
        </v-sheet>
      </div>
    </v-fade-transition>
  </div>
</template>

<script>

import copyToClipboard from 'copy-to-clipboard';
import NotificationService from '../services/notification';

export default { 
  data() {
    return { 
      code: `var numbers = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ];\nnumbers.sort((a, b) => { \n  if(a > b) return 1; \n  else return -1;\n});`,
      color: 'grey',
      darkenDefault: 'darken-3',
      darkenSelected: 'darken-2',
      showCopy: false,
      hovered: -1,
      selected: []
    }
  },
  methods: {
    getRowText(index) {
      return (index <= 9 ? '0' + index : '' + index);
    },
    getClass(index) {
      const isDefault = (index !== this.hovered) && (this.selected.indexOf(index) === -1);
      return {
        [this.color]: true,
        [this.darkenDefault]: isDefault,
        [this.darkenSelected]: !isDefault
      };
    },
    rowEntered(index) {
      this.hovered = index;
    },
    rowLeft() {
      this.hovered = -1;
    },
    rowClicked(index) {
      const i = this.selected.indexOf(index);
      if (i === -1) {
        this.selected.push(index);
      } else {
        this.selected.splice(i, 1);
      }
    },
    copyCode() {
      NotificationService.success("Notification", "Text successfully copied!");
      copyToClipboard(this.code);
    }
  },
  computed: {
    rows() {
      return this.code.split('\n');  
    },
    background() {
      return this.color + ' ' + this.darkenDefault;
    }
  }
};

</script>

<style scoped>
  .code-container {
    position: relative;
  }

  .code {
    position: static;
    border-radius: 12px;
    overflow-x: auto;
    letter-spacing: 0.5px;
    word-spacing: 1px;
    font-family: "Inconsolata", monospace;
    white-space: pre;
    font-weight: 300;
    font-size: 15px;
  }

  .code-row {
    letter-spacing: 0.8px;
  }

  .copy {
    position: absolute;
    top: 0;
    right: 0;
    margin: 5px;
  }
</style>