class DetailsPage {

    constructor() {
        this.state = {
            ingredient_id: "",
            ingredient: null,
            ingredientInventoryAdditions: [],
            ingredientInventorySubtractions: [],
            recipeIngredients: [],
            ingredients: [],
            styles: [],
            substituteIngredients: []
        };

        // instance variables that the app needs but are not part of the "state" of the application
        // server url needs to start with https, not http, the default for ASP.NET Core when launched from Visual Studio
        this.server = "https://localhost:7124/api"
        this.url = this.server + "/ingredients";

        // instance variables related to ui elements simplifies code in other places
        this.$ingredientId = document.querySelector('#customerId');
        this.$ingredientName = document.querySelector('#name');
        this.$ingredientVersion = document.querySelector('#version');
        this.$ingredientTypeId = document.querySelector('#ingredientTypeId');
        this.$ingredientOnHandQuantity = document.querySelector('#onHandQuantity');
        this.$ingredientUnitTypeId = document.querySelector('#unitTypeId');
        this.$ingredientUnitCost = document.querySelector('#unitCost');
        this.$ingredientReorderPoint = document.querySelector('#reorderPoint');
        this.$enterAdjustButton = document.querySelector('#enterAdjustBtn');
        this.$editButton = document.querySelector('#editBtn');
        this.$saveButton = document.querySelector('#saveBtn');
        this.$cancelButton = document.querySelector('#exitBtn');

        // call these methods to set up the page
        this.bindAllMethods();
        this.fetchStates();
        this.makeFieldsReadOnly(true);
        this.makeFieldsRequired(false);
        this.enableButtons("pageLoad");

    }
}

// instantiate the js app when the html page has finished loading
window.addEventListener("load", () => new DetailsPage());