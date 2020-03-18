<template>
	<div>
		<q-expansion-item label="Chat" class="chat">
			<q-card>
				<q-separator />
				<q-card-section class="q-py-sm q-px-none">
					<q-list dense>
						<q-item clickable v-for="n in 6" :key="n">
							<q-item-section avatar class="q-pl-md">
								<q-avatar size="30px">
									<img src="https://cdn.quasar.dev/img/avatar2.jpg" />
								</q-avatar>
							</q-item-section>
							<q-item-section>Ivica</q-item-section>
						</q-item>
					</q-list>
				</q-card-section>
			</q-card>
		</q-expansion-item>
	</div>
</template>

<script>
export default {
	name: "Footer",
	props: ["drawerTrigger"],
	data() {
		return {
			chat: null
		};
	},
	methods: {}
};
</script>

<style lang="sass">
.chat
  background-color: white
  width: 15%
  z-index: 200
  position: fixed
  bottom: 0
  right: 15px
  border-top: 1px solid rgba(0, 0, 0, 0.12)
  border-right: 1px solid rgba(0, 0, 0, 0.12)
  border-left: 1px solid rgba(0, 0, 0, 0.12)
  border-top-left-radius: 6px
  border-top-right-radius: 6px
  user-select: none
</style>
