const columnDefs = [
    { suppressMovable: true, headerName: "Sucursal", field: "sucursal", rowGroup: true, hide: true },
    { suppressMovable: true, headerName: "Nombre (Agente)", field: "nomAgente", rowGroup: true, hide: true },
    { suppressMovable: true, headerName: "Código", field: "codigo" },
    { suppressMovable: true, headerName: "Nombre (Cliente)", field: "nomCliente" },
    { suppressMovable: true, headerName: "Total Vencido", field: "totVencido", aggFunc: myCustomSumFunction, comparator: compareNumber  },
    { suppressMovable: true, headerName: "Total por Vencer", field: "totAVencer", aggFunc: myCustomSumFunction, comparator: compareNumber },
    { suppressMovable: true, headerName: "Total", field: "total", aggFunc: myCustomSumFunction, comparator: compareNumber }
    
];

// specify the data
const rowData = [
    { codigo: "100", nomCliente: "", nomAgente: "", totVencido: 100, totAVencer: 100, total: 200 },
    { codigo: "200", nomCliente: "", nomAgente: "", totVencido: 200, totAVencer: 0, total: 200 }
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
        resizable: false,
    },
    autoGroupColumnDef: {
        minWidth: 200,
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