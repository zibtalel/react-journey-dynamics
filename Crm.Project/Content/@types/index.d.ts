///<reference path="../../../../Content/@types/index.d.ts"/>

import { CompanyDetailsPotentialsTabViewModel } from "../ts/CompanyDetailsPotentialsTabViewModel";
import { CompanyDetailsProjectsTabViewModel } from "../ts/CompanyDetailsProjectsTabViewModel";

declare global {
	namespace Crm {
		namespace Project {
			namespace ViewModels {
				let CompanyDetailsPotentialsTabViewModel: CompanyDetailsPotentialsTabViewModel;
				let CompanyDetailsProjectsTabViewModel: CompanyDetailsProjectsTabViewModel;
				let PotentialListIndexViewModel: any
				let ProjectListIndexViewModel: any
			}
		}
	}
}

