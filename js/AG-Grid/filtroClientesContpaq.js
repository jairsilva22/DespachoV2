const columnDefs = [
    { field: "seleccionar", headerName: "", width: 50, cellRenderer: seleccionarClienteRenderer },
    { field: "numcliente", headerName: "Código Cliente", width: 150 },
    { field: "nombre", width: 450 },
    { field: "sucursal", width: 270  }
];

// specify the data
const rowData = [
    { seleccionar: 1, nombre: "Luis", sucursal: "Saltillo" },
    { seleccionar: 2, nombre: "Enrique", sucursal: "Irapuato" }
];

// let the grid know which columns and what data to use
const gridOptions = {
    columnDefs: columnDefs,
    rowData: rowData,
    maintainColumnOrder: true
};

function onFilterTextBoxChanged() {
    gridOptions.api.setQuickFilter(
        document.getElementById('filter-text-box').value
    );
}

// setup the grid after the page has finished loading
document.addEventListener('DOMContentLoaded', () => {
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
});

