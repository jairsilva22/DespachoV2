const columnDefs = [
    { suppressMovable: true, headerName: "Sucursal", field: "sucursal", rowGroup: true, hide: true },
    { suppressMovable: true, headerName: "Código Producto", field: "codigo", sortable: false },
    { suppressMovable: true, headerName: "Nombre (productos, servicios, paquetes)", field: "nombre", sortable: false },
    { suppressMovable: true, headerName: "Cantidad", field: "cantidad", aggFunc: sumM3, comparator: compareNumber },
    { suppressMovable: true, headerName: "Precio promedio unitario (antes de IVA)", field: "precioPromedio", aggFunc: myCustomSumFunction, comparator: compareNumber },
    { suppressMovable: true, headerName: "Descuento %", field: "descuento", aggFunc: sumDescuento, comparator: compareNumber },
    { suppressMovable: true, headerName: "Total", field: "total", aggFunc: myCustomSumFunction, comparator: compareNumber }

];

// specify the data
const rowData = [
    { sucursal: "Concretos Saltillo Facturable", codigo: "100", nombre: "Luis", cantidad: "10", precioPromedio: "100", descuento: "1", total: "99" },
    { sucursal: "Concretos Saltillo Facturable", codigo: "200", nombre: "Enri", cantidad: "10", precioPromedio: "200", descuento: "1", total: "199" }
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
        sortable: true,
        resizable: true,
    },
    autoGroupColumnDef: {
        minWidth: 100,
    },
    suppressAggFuncInHeader: true
};

function compareNumber(valueA, valueB) {
    let numero1 = valueA.toString();
    numero1 = numero1.replace("$", "");
    numero1 = numero1.replace(",", "");
    numero1 = numero1.replace(",", "");
    numero1 = numero1.replace("-", "0");
    numero1 = numero1.replace("PZA", "");
    numero1 = numero1.replace("M3", "");
    numero1 = numero1.replace("%", "");

    let numero2 = valueB.toString();
    numero2 = numero2.replace("$", "");
    numero2 = numero2.replace(",", "");
    numero2 = numero2.replace(",", "");
    numero2 = numero2.replace("-", "0");
    numero2 = numero2.replace("PZA", "");
    numero2 = numero2.replace("M3", "");
    numero2 = numero2.replace("%", "");

    numero1 = Number(numero1);
    numero2 = Number(numero2);

    if (numero1 == numero2) return 0;
    return (numero1 > numero2) ? 1 : -1;
}

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

function sumM3(params) {
    var sum = 0;
    params.values.forEach(function (value) {
        let numero = value.toString();
        numero = numero.replace("M3", "");
        numero = numero.replace(",", "");
        numero = numero.replace("PZA", "");
        sum += Number(numero);
    });
    return numberToCurrency(roundToTwo(sum)).replace("$", "");
}

function sumDescuento(params) {
    var sum = 0;
    var i = 0;
    params.values.forEach(function (value) {
        let numero = value.toString();
        numero = numero.replace("%", "");
        numero = numero.replace(",", "");
        sum += Number(numero);
        i++;
    });
    sum = sum / i;
    return roundToTwo(sum).toString() + "%";
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

function autoSizeAll(skipHeader) {
    const allColumnIds = [];
    gridOptions.columnApi.getColumns().forEach((column) => {
        allColumnIds.push(column.getId());
    });

    gridOptions.columnApi.autoSizeColumns(allColumnIds, skipHeader);
}