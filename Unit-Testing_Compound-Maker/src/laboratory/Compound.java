package laboratory;

import java.util.ArrayList;
import java.util.Collections;

public class Compound {
	private String name;
	private double molecularMass;
	private String formula;
	private String hillFormula;
	private ArrayList<Element> elements;
	
	public Compound(String name, double molecularMass, String formula, String hillFormula, ArrayList<Element> elements) {
		this.name = name;
		this.molecularMass = molecularMass;
		this.formula = formula;
		this.elements = elements;
		this.hillFormula = hillFormula;
		
	}

	public Compound(String name, double molecularMass, String formula, ArrayList<Element> elements) {
		this.name = name;
		this.molecularMass = molecularMass;
		this.formula = formula;
		this.elements = elements;
		this.hillFormula = CreateHillFormula(formula);
	}
	
	
	private String CreateHillFormula(String formula) {
		String hillForm ="";
		
		ArrayList<String> alphabeticalElements = alphabeticalSymbols();
		
		for (int i = 0; i < alphabeticalElements.size(); i++) {
			String symbolString = alphabeticalElements.get(i);
			
			for (int j = formula.indexOf(symbolString); j < formula.length()-1; j++) {
		
				if (formula.contains(symbolString)) {	
				//	j= formula.indexOf(symbolString);
					boolean keepGoing = true;
					if(Character.isLowerCase(formula.charAt(j+1)) && j+1 <= formula.length()){
						j++;
					}
					if(symbolString.length() == 2 && formula.substring(formula.length() - 2).equals(symbolString)) {
						keepGoing = false;
						return hillForm += symbolString;
					}
					while(keepGoing && Character.isDigit(formula.charAt(++j)) ) {
						symbolString += "" + formula.charAt(j);
						
						if(j+1 >= formula.length()) {
							
							break;
						}
						//j++;

					}
					break;
					
				}
				
			}
			hillForm += symbolString;
			//System.out.println(symbolString);
		}
	
//		String cAtFront ="C";
//		if (formula.contains("C")) {
//			int maybeCarbon = formula.indexOf('C');
//			char afterC = formula.charAt(maybeCarbon +1);
//			if (Character.isUpperCase(afterC) || Character.isDigit(afterC)){
//			
//				
//				
//				
//				for(int i = 0; i < formula.length() ; i++) {
//					
//					if (formula.substring(i,i+1).equals("C")) {
//						if(formula.substring(i+1, i+2).)
//						while(Character.isDigit(formula.charAt(++i))) {
//							cAtFront += formula.substring(i,i+1);
//						}
//					}
//				}
//				
//			}
//		}


		return hillForm;
	}
	
	private ArrayList<String> alphabeticalSymbols(){
		ArrayList<String> alphabetizedSymbols = new ArrayList<String>();
		for (int i = 0; i < elements.size(); i++) {
			
			if(!(elements.get(i).getSymbol().equals("C") || elements.get(i).getSymbol().equals("H"))) {
				alphabetizedSymbols.add(elements.get(i).getSymbol());
			}
		}
		
		Collections.sort(alphabetizedSymbols);
		for(int i = 0; i < elements.size(); i++) {
			if(elements.get(i).getSymbol().equals("H")) {
				alphabetizedSymbols.add(0, elements.get(i).getSymbol());
			}
		}
		
		for(int i = 0; i < elements.size(); i++) {
			if(elements.get(i).getSymbol().equals("C")) {
				alphabetizedSymbols.add(0, elements.get(i).getSymbol());
			}
		}
		
		if(alphabetizedSymbols.get(0).equals("H")) {
			Collections.sort(alphabetizedSymbols);
		}
		
		return alphabetizedSymbols;
	}
	
	
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public double getMolecularMass() {
		return molecularMass;
	}

	public void setMolecularMass(double molecularMass) {
		this.molecularMass = molecularMass;
	}

	public String getFormula() {
		return formula;
	}

	public void setFormula(String formula) {
		this.formula = formula;
	}

	public String getHillFormula() {
		return hillFormula;
	}

	public void setHillFormula(String hillFormula) {
		this.hillFormula = hillFormula;
	}

	public ArrayList<Element> getElements() {
		return elements;
	}

	public void setElements(ArrayList<Element> elements) {
		this.elements = elements;
	}
	@Override
	public String toString() {
		return "Compound: Name =" + name + ", molecularMass =" + molecularMass + ", formula =" + formula + ", hillFormula ="
				+ hillFormula + ", elements =" + elements + "  ";
	}
}