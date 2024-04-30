class seleccionarClienteRenderer {
    // gets called once before the renderer is used
    init(params) {
        // create the cell
        var opc = `<button style="background-color: transparent; border-color: transparent;">
               </button>
            `;
        if (params.data.mod > 0) {
            opc = `<a href="javascript:mostrar(` + params.data.seleccionar + `, '` + params.data.nombre + `', '` + params.data.sucursal + `')" style="background-color: transparent; border-color: transparent;">
              <img src="./imagenes/arrow-right.png" style="width: 20px;"/> </a>
            `;
        } else {
            opc = `<a href="javascript:mostrar(` + params.data.seleccionar + `, '` + params.data.nombre + `', '` + params.data.sucursal + `')" style="background-color: transparent; border-color: transparent;">
              <img src="./imagenes/arrow-right.png" style="width: 20px;"/> </a>
            `;
        }

        this.eGui = document.createElement('div');
        this.eGui.innerHTML = opc;

        // get references to the elements we want
        this.eButton = this.eGui.querySelector('.btn-simple');
        this.eValue = this.eGui.querySelector('.my-value');

        // set value into cell
        //this.cellValue = " ";
        //this.eValue.innerHTML = this.cellValue;

        // add event listener to button
        //this.eventListener = () => alert(`${this.cellValue} medals won!`);
        //if (params.data.mod > 0) {
        //   this.eventListener = () => modificar(params.data.nombreadd, params.data.claveadd);
        // this.eButton.addEventListener('click', this.eventListener);
        // }

    }

    getGui() {
        return this.eGui;
    }

    // gets called whenever the cell refreshes
    refresh(params) {
        // set value into cell again
        this.cellValue = this.getValueToDisplay(params);
        this.eValue.innerHTML = this.cellValue;

        // return true to tell the grid we refreshed successfully
        return true;
    }

    // gets called when the cell is removed from the grid
    destroy() {
        // do cleanup, remove event listener from button
        if (this.eButton) {
            // check that the button element exists as destroy() can be called before getGui()
            this.eButton.removeEventListener('click', this.eventListener);
        }
    }

    getValueToDisplay(params) {
        return params.data.nombre ? params.data.nombre : "";
    }
}



