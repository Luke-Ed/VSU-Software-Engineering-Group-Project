package laboratory;
//
import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;

import java.util.ArrayList;
import java.util.Collections;

public class PeriodicTable{
	private ArrayList<Element> periodicTable;// = new ArrayList<Element>();
//	private ArrayList<CompoundElement> compoundList = new ArrayList<>();
		
	public PeriodicTable() {
		try {
			//Copy Elements from a file
			periodicTable = new ArrayList<Element>();
			File elementText = new File("src/ListOfElements.txt");
			Scanner elementReader = new Scanner(elementText);
			while (elementReader.hasNext()) {
				//Getting values from file for creating a new element
				int atmNum = Integer.valueOf(elementReader.nextLine());
				String name = elementReader.nextLine();
				String symbol = elementReader.nextLine();
				double atmMass = Double.valueOf(elementReader.nextLine());
				int state = Integer.valueOf(elementReader.nextLine());
				int group = Integer.valueOf(elementReader.nextLine());
				//Using values from file to create and add new element to arraylist
				Element element = new Element(atmNum, name, symbol, atmMass, state, group);
				periodicTable.add(element);
			}
			elementReader.close();
		}
		catch(FileNotFoundException e) {
			System.out.println("Can't find file");
		}
	}

	public ArrayList<Element> getPeriodicTable() {
		return this.periodicTable;
	}
	
	public ObservableList<Element> getAllElements() {
		ObservableList<Element> elements = FXCollections.observableArrayList(periodicTable);
		return elements;
	}
	
	// need to sort solidElements, liquidElements, gasElements, and unknownElements
	// by atomicMass in the next 4 methods
	
	public ObservableList<Element> getSolidElements() {
		ObservableList<Element> solidElements = FXCollections.observableArrayList();
		
		for (int i = 0; i < periodicTable.size() ; i++) {
			if(periodicTable.get(i).getState().equalsIgnoreCase("solid") ) {
				solidElements.add(periodicTable.get(i));
			}
		}
		Collections.sort(solidElements, new MassCompare());
		return solidElements;
	}
	
	public ObservableList<Element> getLiquidElements() {
		ObservableList<Element> liquidElements = FXCollections.observableArrayList();
		
		for (int i = 0; i < periodicTable.size() ; i++) {
			if(periodicTable.get(i).getState().equalsIgnoreCase("liquid") ) {
				liquidElements.add(periodicTable.get(i));
			}
		}
		Collections.sort(liquidElements,new MassCompare()); 
		return liquidElements;
	}
	
	public ObservableList<Element> getGasElements() {
		ObservableList<Element> gasElements = FXCollections.observableArrayList();
		
		for (int i = 0; i < periodicTable.size() ; i++) {
			if(periodicTable.get(i).getState().equalsIgnoreCase("gas") ) {
				gasElements.add(periodicTable.get(i));
			}
		}
		Collections.sort(gasElements,new MassCompare()); 
		return gasElements;
	}
	
	public ObservableList<Element> getUnknownElements() {
		ObservableList<Element> unknownElements =  FXCollections.observableArrayList();
		for (int i = 0; i < periodicTable.size() ; i++) {
			if(periodicTable.get(i).getState().equalsIgnoreCase("unknown") ) {
				unknownElements.add(periodicTable.get(i));
			}
		}
		Collections.sort(unknownElements,new MassCompare()); 
		return unknownElements;
	}
	
	public void printPeriodicTable() {
		for (Element e : periodicTable) {
			System.out.println(e);
		}
		System.out.println();
	}
	
	public void printElementsOfState (String state) {
		
	}
	
	public Element getElementBySymbol(String symbol) {
		for (Element e : periodicTable) {
			if (e.getSymbol().equals(symbol)) {
				Element element = e;
				return element;
			}
		}
		return null;
	}
	
	public void sortByAtomicNumber() {
		Collections.sort(periodicTable);
	}
	
	public void sortBySymbol () {
		SymbolCompare symbolCompare = new SymbolCompare();
		Collections.sort(periodicTable, symbolCompare);
	}
	
	public void sortByName() {
		ElementNameCompare nameCompare = new ElementNameCompare();
		Collections.sort(periodicTable, nameCompare);
	}
	
	public void sortByMass() {
		MassCompare massCompare = new MassCompare();
		Collections.sort(periodicTable, massCompare);
	}
}
