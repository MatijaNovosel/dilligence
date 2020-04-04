<template>
  <q-page class="q-pa-md">
    <div class="row full-width">
      <div class="col-12">
        <span class="text-weight-light text-h5">Settings</span>
      </div>
      <div class="col-12 q-py-sm">
        <q-separator />
      </div>
			<div class="col-12 q-pb-md">
				<q-toggle size="sm" label="Dark mode" v-model="settings.darkMode"> </q-toggle>
			</div>
    </div>
  </q-page>
</template>

<script>
export default {
	name: "Settings",
	data() {
		return {
			settings: {
				darkMode: false
			}
		}
	}
};
</script>
