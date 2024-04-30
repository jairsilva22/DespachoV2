const columnDefs = [
    { suppressMovable: true, headerName: "Sucursal", field: "sucursal", rowGroup: true, hide: true },
    { suppressMovable: true, headerName: "Proveedor", field: "proveedor", rowGroup: true, hide: true },
    { suppressMovable: true, headerName: "Fecha", field: "fecha", sortable: false },
    { suppressMovable: true, headerName: "Factura", field: "factura", sortable: false },
    { suppressMovable: true, headerName: "Concepto", field: "concepto", sortable: false },
    { suppressMovable: true, headerName: "Subtotal", field: "subtotal", aggFunc: myCustomSumFunction, comparator: compareNumber },
    { suppressMovable: true, headerName: "IVA", field: "iva", aggFunc: myCustomSumFunction, comparator: compareNumber },
    { suppressMovable: true, headerName: "ISR Ret", field: "isr", aggFunc: myCustomSumFunction, comparator: compareNumber },
    { suppressMovable: true, headerName: "Total", field: "total", aggFunc: myCustomSumFunction, comparator: compareNumber },
    { suppressMovable: true, headerName: "Moneda", field: "moneda", sortable: false },
    { suppressMovable: true, headerName: "Fecha de vencimiento", field: "fechaV", aggFunc: maxDate, comparator: dateComparator }
];

// specify the data
const rowData = [
    { sucursal: "Concretos Saltillo Facturable", proveedor: "Luis", fecha: "08/01/2022", factura: "10", concepto: "mantenimiento", subtotal: "100", iva: "16", isr: "1", total: "115", moneda: "USD", fechaV: "08/10/2022" },
    { sucursal: "Concretos Saltillo Ventas General", proveedor: "Enrique", fecha: "08/02/2022", factura: "10", concepto: "holaxd", subtotal: "200", iva: "32", isr: "1", total: "231", moneda: "MXN", fechaV: "09/10/2022" }
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
        sortable: true
    },
    autoGroupColumnDef: {
        minWidth: 100,
    },
    suppressAggFuncInHeader: true
};

function roundToTwo(num) {
    return +(Math.round(num + "e+2") + "e-2");
}

function maxDate(params) {
    var sum = "00/00/0000";
    params.values.forEach(function (value) {
        let fecha = value.toString();
        const aux = monthToComparableNumber(sum);
        const fechaAux = monthToComparableNumber(fecha);
        if (fechaAux > aux) {
            sum = fecha;
        } else {
            sum = sum;
        }

    });
    return sum;
}

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
        sum += Number(numero);
    });
    return numberToCurrency(roundToTwo(sum));
}

function numberToCurrency(number) {
    let currency = Intl.NumberFormat('es-MX', { style: 'currency', currency: 'MXN' }).format(number);
    return currency;
}

function dateComparator(date1, date2) {
    const date1Number = monthToComparableNumber(date1);
    const date2Number = monthToComparableNumber(date2);

    if (date1Number === null && date2Number === null) {
        return 0;
    }
    if (date1Number === null) {
        return -1;
    }
    if (date2Number === null) {
        return 1;
    }

    return date1Number - date2Number;
}

function monthToComparableNumber(date) {
    if (date === undefined || date === null || date.length !== 10) {
        return null;
    }

    const yearNumber = Number.parseInt(date.substring(6, 10));
    const monthNumber = Number.parseInt(date.substring(3, 5));
    const dayNumber = Number.parseInt(date.substring(0, 2));

    return yearNumber * 10000 + monthNumber * 100 + dayNumber;
}

// setup the grid after the page has finished loading
document.addEventListener('DOMContentLoaded', () => {
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
});