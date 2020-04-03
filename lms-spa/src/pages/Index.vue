<template>
  <q-page class="flex flex-center">
    <q-toggle v-model="dark"> Click </q-toggle>
  </q-page>
</template>

<script>
export default {
  name: 'Index',
  data() {
    return {
      dark: false
    }
  },
  watch: {
    dark() {
      this.$q.dark.toggle();
    }
  }
}
</script>
