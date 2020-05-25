function Grille() {
	var div = document.getElementById("battle");	

	grille = div.appendChild(document.createElement("table"));
	grille.className = "table table-bordered";

	for (i = 0; i < 10; i++) {
		tr = grille.appendChild(document.createElement("tr"));	
		for (j = 0; j < 10; j++) {
			this.macase = new Case(this,i,j);		
		}
	}
};

function Case(grille, numLigne,numColonne) {
	this.ligne = numLigne;
	this.colonne = numColonne;
	this.grille = grille;
	this.td = tr.appendChild(document.createElement("td"));
	this.td.onclick = function (c) { return function () { c.clique() }; }(this);
	this.creerCase('X');
}

Case.prototype.creerCase = function (symbol) {	
	suppElem(this.td);
	this.td.appendChild(document.createTextNode(symbol));
}

Case.prototype.clique = function () {
	this.creerCase(this.colonne);
}

function suppElem(elem) {
	while (elem.firstChild) elem.removeChild(elem.firstChild);
}