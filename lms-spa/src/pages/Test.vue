<template>
	<q-page>
		<FileCabinet :content="content"></FileCabinet>
	</q-page>
</template>

<script>
import FileCabinet from "../components/FileCabinet";

export default {
	name: "Test",
	components: {
		FileCabinet
	},
	data() {
		return {
			content: {
				naslov: "Datoteke",
				files: [
					{
            id: 1,
            naziv: "Datoteka1.sql",
            contentType: "text"
					},
					{
            id: 2,
            naziv: "Datoteka2.png",
            contentType: "image/png"
					},
					{
            id: 3,
            naziv: "Datoteka2.png",
            contentType: "image/png"
					},
					{
            id: 4,
            naziv: "Datoteka2.png",
            contentType: "image/png"
					},
					{
            id: 5,
            naziv: "Datoteka2.png",
            contentType: "image/png"
					}
				]
			}
		};
	}
};
</script>
