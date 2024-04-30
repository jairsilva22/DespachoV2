const columnDefs = [

    { suppressMovable: true, field: 'concepto', sortable: false },
    {
        headerName: 'Por Sucursales',
        marryChildren: true,
        children: [
            {
                headerName: 'Irapuato',
                marryChildren: true,
                children: [
                    { suppressMovable: true, field: 'concretoI', headerName: 'Concreto', comparator: compareNumber },
                    { suppressMovable: true, field: 'blockI', headerName: 'Block', comparator: compareNumber },
                    { suppressMovable: true, field: 'totalI', headerName: 'Total', comparator: compareNumber }
                ]
            },
            {
                headerName: 'Saltillo',
                marryChildren: true,
                children: [
                    { suppressMovable: true, field: 'concretoS', headerName: 'Concreto', comparator: compareNumber },
                    { suppressMovable: true, field: 'blockS', headerName: 'Block', comparator: compareNumber },
                    { suppressMovable: true, field: 'totalS', headerName: 'Total', comparator: compareNumber }
                ]
            },
        ],
    },
    {
        headerName: '',
        marryChildren: true,
        children: [
            {
                headerName: 'Por empresas',
                marryChildren: true,
                children: [
                    { suppressMovable: true, field: 'concretoE', headerName: 'Concreto', aggFunc: myCustomSumFunction, comparator: compareNumber },
                    { suppressMovable: true, field: 'blockE', headerName: 'Block', aggFunc: myCustomSumFunction, comparator: compareNumber },
                    { suppressMovable: true, field: 'grupoE', headerName: 'Grupo', comparator: compareNumber }
                ],
            }
        ],
    }

];

// specify the data
const rowData = [
    { concepto: "Saldo Inicial Efectivo", concretoI: "50", blockI: "0", totalI: 0, concretoS: 0, blockS: 0, totalS: 0, concretoE: 0, blockE: 0, grupoE: 0 },
    { concepto: "Ingresos Efectivo (+)", concretoI: "200", blockI: "0", totalI: 0, concretoS: 0, blockS: 0, totalS: 0, concretoE: 0, blockE: 0, grupoE: 0 },
    { concepto: "Traspasos Concreto <> Block", concretoI: "0", blockI: "0", totalI: 0, concretoS: 0, blockS: 0, totalS: 0, concretoE: 0, blockE: 0, grupoE: 0 },
    { concepto: "Egresos Efectivo (-)", concretoI: "150", blockI: "0", totalI: 0, concretoS: 0, blockS: 0, totalS: 0, concretoE: 0, blockE: 0, grupoE: 0 },
    { concepto: "Saldo Final Efectivo", concretoI: "100", blockI: "0", totalI: 0, concretoS: 0, blockS: 0, totalS: 0, concretoE: 0, blockE: 0, grupoE: 0 },
];

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
    animateRows: true,
    suppressAggFuncInHeader: true,
    rowClassRules: {
        'total-agente': 'data.concepto == "Saldo Final Efectivo" || data.concepto == "Saldo Final Bancos" || data.concepto == "Saldo Final Efvo y Bancos"'
    }
};

function compareNumber(valueA, valueB) {
    let numero1 = valueA.toString();
    numero1 = numero1.replace("$", "");
    numero1 = numero1.replace(",", "");
    numero1 = numero1.replace(",", "");
    numero1 = numero1.replace("-", "0");
    numero1 = numero1.replace("PZA", "");
    numero1 = numero1.replace("M3", "");

    let numero2 = valueB.toString();
    numero2 = numero2.replace("$", "");
    numero2 = numero2.replace(",", "");
    numero2 = numero2.replace(",", "");
    numero2 = numero2.replace("-", "0");
    numero2 = numero2.replace("PZA", "");
    numero2 = numero2.replace("M3", "");

    numero1 = Number(numero1);
    numero2 = Number(numero2);

    if (numero1 == numero2) return 0;
    return (numero1 > numero2) ? 1 : -1;
}

function myCustomSumFunction(params) {
    var sum = 0;
    params.values.forEach(function (value) {
        let numero = value.toString();
        numero = numero.replace("$", "");
        numero = numero.replace(",", "");
        numero = numero.replace(",", "");
        numero = numero.replace("-", "0");
        sum += Number(numero);
    });
    return numberToCurrency(roundToTwo(sum));
}

function sumPiezas(params) {
    var sum = 0;
    params.values.forEach(function (value) {
        let numero = value.toString();
        numero = numero.replace("PZA", "");
        numero = numero.replace("-", "0");
        sum += Number(numero);
    });
    return roundToTwo(sum).toString() + " PZA";
}

function sumM3(params) {
    var sum = 0;
    params.values.forEach(function (value) {
        let numero = value.toString();
        numero = numero.replace("M3", "");
        numero = numero.replace("-", "0");
        sum += Number(numero);
    });
    return roundToTwo(sum).toString() + " M3";
}

function roundToTwo(num) {
    return +(Math.round(num + "e+2") + "e-2");
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