declare class HelperPerDiem {
    static queryPerDiemReportStatus(query: any, term: any): any;
    static autocompleterOptionsToCostCenter(tableName: any, validCostCenters: any): {
        table: any;
        mapDisplayObject: any;
        getElementByIdQuery: any;
        customFilter: (query: any, term: any) => any;
    };
    static autocompleterOptionsToPerDiemLookups(tableName: any, onSelectedItem: any): {
        table: any;
        mapDisplayObject: any;
        getElementByIdQuery: any;
        onSelect: any;
        customFilter: (query: any, term: any) => any;
    };
}
