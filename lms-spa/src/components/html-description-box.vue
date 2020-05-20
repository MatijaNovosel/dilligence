<template>
  <p
    style="word-wrap: break-word"
    class="text-subtitle2"
    :class="`${$q.dark.isActive ? 'dark-bg border-box-dark' : 'light-bg border-box-light'}`"
    v-html="html"
  />
</template>

<script>
export default {
  name: "html-description-box",
  props: ["html"],
  data() {
    return {};
  },
  methods: {}
};
</script>

<style lang="sass" scoped>
.dark-bg
  background-color: #2b2b2b
.light-bg
  background-color: rgb(239, 234, 234)
</style>