package laboratory;
//
import java.util.ArrayList;
import java.util.Collections;
import javafx.beans.property.SimpleDoubleProperty;
import javafx.beans.property.SimpleStringProperty;

public class CompoundElement implements Comparable<CompoundElement>{
	private SimpleStringProperty name;
	private SimpleStringProperty formula;
	private SimpleDoubleProperty molecularMass;
	private SimpleStringProperty hillFormula;
	private ArrayList <Element> elements = new ArrayList<Element>();
	
	//CompoundElement Constructor
	public CompoundElement(String name, ArrayList <Element> elements) {
		this.name = new SimpleStringProperty(name);
		this.elements = elements;
		this.formula = new SimpleStringProperty(computeFormula(elements));
		this.hillFormula = new SimpleStringProperty(computeHillFormula());
		this.molecularMass = new SimpleDoubleProperty(computeMolecularMass());
	}
	
	public CompoundElement(String name, CompoundElement compound1, CompoundElement compound2) {
//		this(name, combineElementLists(compound1, compound2));
		this.name = new SimpleStringProperty(name);
		this.elements = combineElementLists(compound1, compound2);
		this.formula = new SimpleStringProperty(compound1.getFormula() + compound2.getFormula());
		this.hillFormula = new SimpleStringProperty(computeHillFormula());
		this.molecularMass = new SimpleDoubleProperty(computeMolecularMass());
	}
	
	private ArrayList<Element> combineElementLists(CompoundElement compound1, CompoundElement compound2) {
		ArrayList<Element> combined = new ArrayList<Element>();
		combined.addAll(compound1.getElements());
		combined.addAll(compound2.getElements());
		return combined;
	}
	
	//Method for finding the formula from the ArrayList elements
	private String computeFormula(ArrayList <Element> elements) {
		String compoundFormula = "";
		for (Element element : elements) {
			if (element.getNumAtoms()==1) {
				compoundFormula += element.getSymbol();
			}
			else{
				compoundFormula += element.getSymbol() + element.getNumAtoms();
			}
		}
		return compoundFormula;
	}
	
	
	//Method for finding the fill formula. It copies the ArrayList elements to
	//another arrayList called duplicateElements and loops through the elements
	//and looks for Carbon or Hydrogen and swaps them accordingly
	private String computeHillFormula() {
		ArrayList <Element> duplicateElements = new ArrayList<Element>(elements);
		String compoundHillFormula = "";
		for(int i = 0; i < duplicateElements.size(); i++){
			if (duplicateElements.get(i).getSymbol().equals("C")){
				Collections.swap(duplicateElements, 0, i);
				for(int j = 0; j < duplicateElements.size(); j++){
					if (duplicateElements.get(j).getSymbol().equals("H")){
						Collections.swap(duplicateElements, 1, j);
					}
				}
			}
		}
		compoundHillFormula = this.computeFormula(duplicateElements);
		return compoundHillFormula;
	}
	
	private double computeMolecularMass(){
		double mMass = 0;
		for (Element element : elements) {
			mMass += element.getAtomicMass() * element.getNumAtoms();
		}
		return mMass;
	}
	
	public void replaceElementsInCompound(ArrayList<Element> newSetOfElements) {
		this.elements = newSetOfElements;
		this.formula = new SimpleStringProperty(computeFormula(this.elements));
		this.hillFormula = new SimpleStringProperty(computeHillFormula());
		this.molecularMass = new SimpleDoubleProperty(computeMolecularMass());
	}
	
	public void addElementToCompound(Element newElement) {
		this.elements.add(newElement);
		this.formula = new SimpleStringProperty(computeFormula(this.getElements()));
		this.hillFormula = new SimpleStringProperty(computeHillFormula());
		this.molecularMass = new SimpleDoubleProperty(computeMolecularMass());
	}
	
	public void only_ReplaceElementsInCompound(ArrayList<Element> newSetOfElements) {
		this.elements = newSetOfElements;
	}
	
	public void only_AddElementToCompound(Element newElement) {
		this.elements.add(newElement);
	}
	
	public String getName() { return name.getValue();}
	
	public String getFormula() { return this.formula.getValue();}
	
	public String getHillFormula() { return this.hillFormula.getValue();}
	
	public Double getMolecularMass() { return molecularMass.getValue();}
	
	public ArrayList<Element> getElements() { return this.elements; }
	
	public void setFormula(String string) {	this.formula = new SimpleStringProperty(string);}
	
	public void setHillFormula(String string) {	this.hillFormula = new SimpleStringProperty(string);}

	public void setName(String newName){this.name = new SimpleStringProperty(newName);}

	public void setMolecularMass(Double molecularMass) {
		this.molecularMass = new SimpleDoubleProperty(molecularMass);
	}

	@Override
	public int compareTo (CompoundElement compound){
		return this.getName().compareTo(compound.getName());
	}	
	
	@Override
	public String toString() {
		return getFormula()  + "   "+ getName();
	}
	
	@Override
	public boolean equals(Object o) {
		if (o == null) {return false;}
		if (!(o instanceof CompoundElement)) {return false;}
		if (o == this) {return true;}
		CompoundElement otherCompound = (CompoundElement) o;
		return otherCompound.getName().equals(this.getName()) || otherCompound.getHillFormula().equals(this.getHillFormula());
	}
}