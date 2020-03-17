<template>
  <div>
    <div class="code-container">
      <v-sheet class="code d-flex grey--text text--lighten-3 pa-4" 
               :color="background" 
               @mouseenter="showCopy = true" 
               @mouseleave="showCopy = false">
        <v-fade-transition>
          <v-btn v-if="showCopy" text icon class="gore-desno white--text" @click="copyCode">
            <v-icon>
              mdi-content-copy
            </v-icon>
          </v-btn>
        </v-fade-transition>
        <v-col cols="1" class="text-right">
          <v-flex v-for="(row, index) of rows"
                  :key="index">
            <span class="orange--text text--lighten-3 mr-3 code-row">{{ getRowText(index + 1) }}</span>
          </v-flex>
        </v-col>
        <v-col cols="11">
          <v-flex v-for="(row, index) of rows"
                  :key="index"
                  :class="getClass(index)"
                  @mouseenter="rowEntered(index)"
                  @mouseleave="rowLeft(index)">
            <span>{{ row }}</span>
          </v-flex>
        </v-col>
      </v-sheet>
    </div>
  </div>
</template>

<script>

  import copyToClipboard from 'copy-to-clipboard';
  import NotificationService from '../services/notification';

  export default {
    props: [ 'code' ],
    data() {
      return {
        color: 'grey',
        darkenDefault: 'darken-3',
        darkenSelected: 'darken-2',
        showCopy: false,
        hovered: -1,
        selected: []
      }
    },
    created() {
      //
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
  }
  
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

  .gore-desno {
    position: absolute;
    top: 5px;
    right: 5px;
    margin: 5px;
  }
</style>