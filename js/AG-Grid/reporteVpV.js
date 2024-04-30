const columnDefs = [
    
    { suppressMovable: true, field: 'sucursal', rowGroup: true, hide: true, sortable: false },
    { suppressMovable: true, field: 'vendedor', colId: 'vendedor', rowGroup: true, hide: true, sortable: false },
    { suppressMovable: true, field: 'cliente', colId: 'cliente', rowGroup: true, hide: true, sortable: false },
    {
        headerName: 'Ventas en unidades',
        marryChildren: true,
        children: [
            { suppressMovable: true, field: 'concretoU', headerName: 'Concreto', aggFunc: sumM3, comparator: compareNumber },
            { suppressMovable: true, field: 'blockU', headerName: 'Block', aggFunc: sumPiezas, comparator: compareNumber }
            
        ],
    },
    {
        headerName: 'Ventas en pesos',
        marryChildren: true,
        children: [
            { suppressMovable: true, field: 'concretoD', headerName: 'Concreto', aggFunc: myCustomSumFunction, comparator: compareNumber },
            { suppressMovable: true, field: 'blockD', headerName: 'Block', aggFunc: myCustomSumFunction, comparator: compareNumber }
        ],
    }
    
];

// specify the data
const rowData = [
    { sucursal: "100", vendedor: "Luis", cliente: "Sandoval", concretoU: 100, blockU: 100, concretoD: 200, blockD: 100 },
    { sucursal: "200", vendedor: "Enrique", cliente: "Sandoval", concretoU: 200, blockU: 0, concretoD: 200, blockD: 300}
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