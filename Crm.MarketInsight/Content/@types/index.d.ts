///<reference path="../../../../Content/@types/index.d.ts"/>

import { CompanyDetailsMarketInsightsTabViewModel } from "../ts/CompanyDetailsMarketInsightsTabViewModel"
import { MarketInsightDetailsViewModel } from "../ts/MarketInsightDetailsViewModel"
import { MarketInsightDetailsProjectsTabViewModel } from "../ts/MarketInsightDetailsProjectsTabViewModel"
import { MarketInsightDetailsPotentialsTabViewModel } from "../ts/MarketInsightDetailsPotentialsTabViewModel"
import { MarketInsightEditModalViewModel } from "../ts/MarketInsightEditModalViewModel"

declare global {
	namespace Crm {
		namespace MarketInsight {
			namespace ViewModels {
				let CompanyDetailsMarketinsightsTabViewModel: CompanyDetailsMarketInsightsTabViewModel;
				let MarketInsightDetailsViewModel:  MarketInsightDetailsViewModel;
				let MarketInsightDetailsProjectsTabViewModel:  MarketInsightDetailsProjectsTabViewModel;
				let MarketInsightDetailsPotentialsTabViewModel:  MarketInsightDetailsPotentialsTabViewModel;
				let MarketInsightEditModalViewModel: MarketInsightEditModalViewModel;
			}
		}
	}
}
