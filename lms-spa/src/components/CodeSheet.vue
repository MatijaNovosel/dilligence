<template>
  <div>
    <div class="code-container">
      <div
        class="code text-grey q-pa-md row"
        :class="background"
        @mouseenter="showCopy = true"
        @mouseleave="showCopy = false"
      >
        <q-btn
          v-if="showCopy"
          flat
          dense
          round
          class="gore-desno"
          icon="mdi-content-copy"
          @click="copyCode"
        />
        <div class="col-1 text-right">
          <div v-for="n in rows.length" :key="n">
            <span class="text-amber-9 q-pr-md code-row">{{ getRowText(n) }}</span>
          </div>
        </div>
        <div class="col-11">
          <div
            class="flex text-grey-5"
            v-for="(row, index) of rows"
            :key="index"
            :class="getClass(index)"
            @mouseenter="rowEntered(index)"
            @mouseleave="rowLeft(index)"
          >
            <span class="q-pl-sm">{{ row }}</span>
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
      darkenDefault: "10",
      darkenSelected: "8",
      defaultClass: "bg-grey-10",
      selectedClass: "bg-grey-9",
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
        [this.defaultClass]: isDefault,
        [this.selectedClass]: !isDefault
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
      return `bg-${this.color}-${this.darkenDefault}`;
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