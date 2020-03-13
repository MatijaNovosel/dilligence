<template>
	<q-page class="q-pa-md">
		<q-table
			:pagination.sync="pagination"
			:loading="loading"
			@update:pagination="optionsUpdated"
			separator="vertical"
			dense
			title="Employees"
			:rows-per-page-options="rowsPerPageOptions"
			:data="employees"
			:columns="columns"
		>
			<template v-slot:top>
				<div class="row full-width">
					<div class="col-12">
						<span class="text-weight-light text-h5">Employees</span>
					</div>
          <div class="col-12 q-py-sm">
            <q-separator />
          </div>
					<div class="col-12">
						<div class="row q-gutter-sm justify-center">
							<div class="col-5">
								<q-input v-model="searchData.name" dense label="Ime" clearable />
							</div>
							<div class="col-5">
								<q-input v-model="searchData.surname" dense label="Prezime" clearable />
							</div>
							<div class="col-3">
								<div class="bg-grey-2 q-pa-sm rounded-borders">
									Odjel:
									<q-option-group dense :options="odjelOptions" type="checkbox" v-model="odjel" />
								</div>
							</div>
							<div class="col-3">
								<div class="bg-grey-2 q-pa-sm rounded-borders">
									Vrsta zaposljenja:
									<q-option-group
										dense
										:options="employmentTypeOptions"
										type="checkbox"
										v-model="employmentType"
									/>
								</div>
							</div>
						</div>
					</div>
				</div>
			</template>
		</q-table>
	</q-page>
</template>

<script>
import EmployeeService from "../services/api/employee";

export default {
	name: "Employees",
	methods: {
		optionsUpdated(options) {
			this.getData();
		},
		getData() {
			this.loading = true;
			EmployeeService.getEmployees()
				.then(({ data }) => {
					this.employees = data;
				})
				.finally(() => {
					this.loading = false;
				});
		}
	},
	data() {
		return {
			employmentType: [],
			odjel: [],
			odjelOptions: [
				{ label: "INRO", value: 1 },
				{ label: "ELO", value: 2 },
				{ label: "GRA", value: 3 },
				{ label: "STRO", value: 4 }
			],
			employmentTypeOptions: [
				{ label: "Vanski suradnik", value: 1 },
				{ label: "Zaposlenik", value: 2 }
			],
			searchData: {
				name: null,
				surname: null,
				odjel: []
			},
			rowsPerPageOptions: [5, 10, 15],
			loading: false,
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
			employees: [],
			pagination: {
				page: 1,
				rowsPerPage: 15
			}
		};
	}
};
</script>
