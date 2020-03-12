<template>
	<q-page class="q-pa-md">
		<q-table
			separator="vertical"
			dense
			title="Employees"
			:data="employees"
			:columns="columns"
			row-key="name"
		/>
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
				}
			],
			employees: null
		};
	}
};
</script>
