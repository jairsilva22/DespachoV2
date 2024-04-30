const columnDefs = [
    { suppressMovable: true, field: "fecha" },
    { suppressMovable: true, field: "serie" },
    { suppressMovable: true, field: "folio" },
    { suppressMovable: true, field: "concepto" },
    { suppressMovable: true, field: "cargos" },
    { suppressMovable: true, field: "abonos" },
    { suppressMovable: true, field: "documento" },
    { suppressMovable: true, field: "vence" },
    { suppressMovable: true, field: "referencia" },
    { suppressMovable: true, field: "sucursal", rowGroup: true, hide: true }
];

// specify the data
const rowData = [
    { fecha: "Toyota", serie: "Celica", folio: 35000, concepto: "", cargos: "", abonos: "", documento: "", vence: "", referencia: "" },
    { fecha: "Ford", serie: "Mustang", folio: 10000, concepto: "", cargos: "", abonos: "", documento: "", vence: "", referencia: "" }
];

// let the grid know which columns and what data to use
const gridOptions = {
    columnDefs: columnDefs,
    rowData: rowData,
    defaultColDef: {
        resizable: true
    }
};

// setup the grid after the page has finished loading
document.addEventListener('DOMContentLoaded', () => {
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
});

function autoSizeAll(skipHeader) {
    const allColumnIds = [];
    gridOptions.columnApi.getColumns().forEach((column) => {
        allColumnIds.push(column.getId());
    });

    gridOptions.columnApi.autoSizeColumns(allColumnIds, skipHeader);
}

function sizeToFit() {
    gridOptions.api.sizeColumnsToFit();
}