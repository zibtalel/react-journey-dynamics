///<reference path="../../../../Content/@types/index.d.ts" />
import { namespace } from "../../../../Content/ts/namespace";

export default class LookupEditModalViewModelExtension extends window.Main.ViewModels.LookupEditModalViewModel{
    
    extensionLookups: LookupType = {
        costCenters: { $tableName: "Main_CostCenter" }
    };
    
    async init(id, params){
        super.init(id,params);
        if(params.fullName === "Crm.PerDiem.Model.Lookups.ExpenseType" || params.fullName === "Crm.PerDiem.Model.Lookups.TimeEntryType"){
            super.isCustomHandledLookup = ko.observable(true);
        }
        
        await window.Helper.Lookup.getLocalizedArrayMaps(this.extensionLookups);
    }

    getCostCentersFromKeys(keys) {
        return this.extensionLookups.costCenters.$array
            .filter(x => keys.indexOf(x.Key) !== -1)
            .map(window.Helper.Lookup.mapLookupForSelect2Display);
    };
   
}
namespace("Main.ViewModels").LookupEditModalViewModel = LookupEditModalViewModelExtension;