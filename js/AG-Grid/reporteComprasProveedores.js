const columnDefs = [
    { suppressMovable: true, headerName: "Sucursal", field: "sucursal", rowGroup: true, hide: true },
    { suppressMovable: true, headerName: "Proveedor", field: "proveedor", rowGroup: true, hide: true },
    { suppressMovable: true, headerName: "Fecha", field: "fecha" },
    { suppressMovable: true, headerName: "Factura", field: "factura" },
    { suppressMovable: true, headerName: "Concepto", field: "concepto" },
    { suppressMovable: true, headerName: "Unidad", field: "unidad" },
    { suppressMovable: true, headerName: "Total", field: "total", aggFunc: myCustomSumFunction },
    { suppressMovable: true, headerName: "Moneda", field: "moneda" }
];

// specify the data
const rowData = [
    { sucursal: "Concretos Saltillo Facturable", proveedor: "Luis", fecha: "08/01/2022", factura: "10", concepto: "mantenimiento", unidad: "1", total: "115", moneda: "USD", fechaV: "08/10/2022" },
    { sucursal: "Concretos Saltillo Ventas General", proveedor: "Enrique", fecha: "08/02/2022", factura: "10", concepto: "holaxd", unidad: "1", total: "231", moneda: "MXN", fechaV: "09/10/2022" }
];

// let the grid know which columns and what data to use
const gridOptions = {
    columnDefs: columnDefs,
    rowData: rowData,
    maintainColumnOrder: true,
    cacheQuickFilter: true,
    defaultColDef: {
        flex: 1,
        minWidth: 100,
        resizable: true,
    },
    autoGroupColumnDef: {
        minWidth: 100,
    },
    suppressAggFuncInHeader: true
};

function roundToTwo(num) {
    return +(Math.round(num + "e+2") + "e-2");
}

function myCustomSumFunction(params) {
    var sum = 0;
    params.values.forEach(function (value) {
        let numero = value.toString();
        numero = numero.replace("$", "");
        numero = numero.replace(",", "");
        numero = numero.replace(",", "");
        sum += Number(numero);
    });
    return numberToCurrency(roundToTwo(sum));
}

function numberToCurrency(number) {
    let currency = Intl.NumberFormat('es-MX', { style: 'currency', currency: 'MXN' }).format(number);
    return currency;
}

// setup the grid after the page has finished loading
document.addEventListener('DOMContentLoaded', () => {
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
});