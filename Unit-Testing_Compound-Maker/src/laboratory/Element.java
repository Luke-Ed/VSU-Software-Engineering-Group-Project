package laboratory;
//

import javafx.beans.property.SimpleDoubleProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleStringProperty;


public class Element implements Comparable<Element>{
//	private int atomicNumber;
	private SimpleIntegerProperty atomicNumber;// = new SimpleIntegerProperty();
//	private String name;
	private SimpleStringProperty name;// = new SimpleStringProperty();
//	private String symbol;
	private SimpleStringProperty symbol;// = new SimpleStringProperty();
//	private double atomicMass;
	private SimpleDoubleProperty atomicMass;// = new SimpleDoubleProperty();
//	private State state;
//	private SimpleIntegerProperty state;// = new SimpleIntegerProperty();
//	private Group group;
	private SimpleStringProperty state;
	private SimpleStringProperty group;// = new SimpleIntegerProperty();
		
	private SimpleIntegerProperty numAtoms;
	
	
	
	
	
	public Element(Integer atomicNumber, String name, String symbol,
			Double atomicMass, Integer state, Integer group, Integer numAtoms) {
		this.atomicNumber = new SimpleIntegerProperty(atomicNumber);
		this.name = new SimpleStringProperty(name);
		this.symbol = new SimpleStringProperty(symbol);
		this.atomicMass = new SimpleDoubleProperty(atomicMass);
		this.state = new SimpleStringProperty(State.values()[state -1].getState());
		this.group = new SimpleStringProperty(Group.values()[group -1].getGroup());
		this.numAtoms = new SimpleIntegerProperty(numAtoms);
	}
	
	
	public Element(Integer atomicNumber, String name, String symbol,
						Double atomicMass, Integer state, Integer group) {
		this(atomicNumber,name,symbol,atomicMass,state,group,1);
		this.atomicNumber = new SimpleIntegerProperty(atomicNumber);
		this.name = new SimpleStringProperty(name);
		this.symbol = new SimpleStringProperty(symbol);
		this.atomicMass = new SimpleDoubleProperty(atomicMass);
		this.state = new SimpleStringProperty(State.values()[state -1].getState());
		this.group = new SimpleStringProperty(Group.values()[group -1].getGroup());
		
	}
	
	public Integer getAtomicNumber() {
		return atomicNumber.get();
	}
	
	public String getName() {
		return name.get();
	}
	
	public String getSymbol() {
		return symbol.get();
	}
	
	public Double getAtomicMass() {
		return atomicMass.get();
	}
	
	public String getState() {
		return state.get();
	}
	
	public String getGroup() {
		return group.get();
	}
	
	public Integer getNumAtoms() {
		return numAtoms.get();
	}
	
//	public void setAtomicNumber(Integer atomicNumber) {
//		this.atomicNumber = new SimpleIntegerProperty(atomicNumber);
//	}
//	
//	public void setName(String name) {
//		this.name = new SimpleStringProperty(name);
//	}
//	
//	public void setSymbol(String symbol) {
//		this.symbol = new SimpleStringProperty(symbol);
//	}
//	
//	public void setAtomicMass (Double atomicMass) {
//		this.atomicMass = new SimpleDoubleProperty(atomicMass);
//	}
//	
//	public void setState(Integer state) {
//		this.state = new SimpleStringProperty(State.values()[state -1].getState());
//	}
//	
//	public void setGroup(Integer group) {
//		this.group = new SimpleStringProperty(Group.values()[group -1].getGroup());
//	}
//	
	public String setNumAtoms(Integer i) {
		if (i > 0) {
			this.numAtoms = new SimpleIntegerProperty(i);
			return "Number of atoms changed to: " + i;
		}
		return "Number of atoms must be positive";
	}

	
	public int compareTo(Element element) {
		return this.getAtomicNumber() - element.getAtomicNumber();
	}
	
	@Override
	public String toString() {
		return getSymbol() + "    " + getName();
	}
	
//	@Override
//	public String toString() {
//		String result = atomicNumber + " " 
//						+ name + " "
//						+ symbol + " "
//						+ atomicMass + " "
//						+ state + " "
//						+ group;
//		return result;
//	}
}