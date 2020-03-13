<template>
	<q-page class="q-pa-md">
		<!-- <q-table separator="vertical" dense title="Employees" :data="employees" :columns="columns" /> -->
	</q-page>
</template>

<script>
import EmployeeService from "../services/api/employee";

export default {
	name: "Employees",
	created() {
		EmployeeService.getEmployees().then(({ data }) => {
			this.employees = data;
		});
	},
	data() {
		return {
			columns: [
				{
					name: "ime",
					label: "Ime",
					align: "center",
					field: "ime"
				},
				{
					name: "prezime",
					align: "center",
					label: "Prezime",
					field: "prezime"
				},
				{
					name: "email",
					align: "center",
					label: "Email",
					field: "email"
				},
				{
					name: "odjelId",
					align: "center",
					label: "Odjel",
					field: "odjelId"
				},
				{
					name: "titulaIspred",
					align: "center",
					label: "Titula ispred",
					field: "titulaIspred"
				},
				{
					name: "titulaIza",
					align: "center",
					label: "Titula iza",
					field: "titulaIza"
				},
				{
					name: "vrstaZaposljenja",
					align: "center",
					label: "Vrsta zaposljenja",
					field: "vrstaZaposljenjaId"
				}
			],
			employees: null
		};
	}
};
</script>
