<template>
  <div>
    <div class="code-container">
      <div
        class="code text-grey q-pa-md row"
        :color="background"
        @mouseenter="showCopy = true"
        @mouseleave="showCopy = false"
      >
        <q-btn v-if="showCopy" text icon class="gore-desno text-white" @click="copyCode">
          <q-icon name="mdi-content-copy" />
        </q-btn>
        <div class="col-1">
          <div class="flex" v-for="(row, index) of rows" :key="index">
            <span class="text-orange q-mr-md code-row">{{ getRowText(index + 1) }}</span>
          </div>
        </div>
        <div class="col-11">
          <div
            class="flex"
            v-for="(row, index) of rows"
            :key="index"
            :class="getClass(index)"
            @mouseenter="rowEntered(index)"
            @mouseleave="rowLeft(index)"
          >
            <span>{{ row }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import copyToClipboard from "copy-to-clipboard";

export default {
  props: ["code"],
  data() {
    return {
      color: "grey",
      darkenDefault: "darken-3",
      darkenSelected: "darken-2",
      showCopy: false,
      hovered: -1,
      selected: []
    };
  },
  created() {
    //
  },
  methods: {
    getRowText(index) {
      return index <= 9 ? "0" + index : "" + index;
    },
    getClass(index) {
      const isDefault =
        index !== this.hovered && this.selected.indexOf(index) === -1;
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
      this.$q.notify({
        type: "positive",
        message: "Text successfully copied!"
      });
      copyToClipboard(this.code);
    }
  },
  computed: {
    rows() {
      return this.code.split("\n");
    },
    background() {
      return this.color + " " + this.darkenDefault;
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
  flex-wrap: nowrap;
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