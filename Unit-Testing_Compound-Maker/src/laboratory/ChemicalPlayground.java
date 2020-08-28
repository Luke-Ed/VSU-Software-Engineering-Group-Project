package laboratory;
//
import java.util.ArrayList;
import java.util.Collections;
import java.util.Scanner;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;

public class ChemicalPlayground {
	
	private PeriodicTable pT;
	private ObservableList<CompoundElement> allCompounds;
	private ObservableList<Double> massRange;
	private ObservableList<Integer> numAtomsList;
	
	public ChemicalPlayground() {
		pT = new PeriodicTable();
		
		allCompounds = FXCollections.observableArrayList();
		importCompoundsFromFile();
		massRange = generate();
		numAtomsList = generateIntegers();
		
	}
	
	// Scans the ListOfCompounds.txt file. If the compound is not already in
	// the observable list allCompounds, the compound will be added 
	// to the list. Returns a string that says how many files were
	// added to the list
    public String importCompoundsFromFile() {
    	int counter = 0;
    	try{
            File compoundText = new File ("ListOfCompounds.txt");
            Scanner compoundScanner = new Scanner (compoundText);
            
            while (compoundScanner.hasNextLine()) {
                String name;
                String formula;
                ArrayList<Element>elements;
                
                name = compoundScanner.nextLine();
                formula = compoundScanner.nextLine();
                elements = createElementsFromFormula(formula);
                
                CompoundElement compoundElement = new CompoundElement(name, elements);
                
                if (!allCompounds.contains(compoundElement)) {
                	allCompounds.add(compoundElement);
                	counter++;
              
                }
                
            }
            compoundScanner.close();
        }
        
        catch(FileNotFoundException e){

        	return "Can't find the file";
        }
    	return "added " + counter + " compounds from ListOfCompounds.txt";
    }
    // Saves all the compounds in the list to the 
    // ListOfCompounds.txt file
    
    public String saveCompounds() {
    	int counter = 0;
    	
    	try {
    		FileWriter fileWriter = new FileWriter ("ListOfCompounds.txt");
    		PrintWriter printWriter = new PrintWriter (fileWriter);
    		
    		for (CompoundElement compound : allCompounds) {
    			printWriter.println(compound.getName());
    			printWriter.println(compound.getFormula());
    			counter++;
    		}
    		fileWriter.close();
    		printWriter.close();
    	}
    	catch(IOException e) {
    		return "Something went wrong while tyring to save to the file";
    	}
    	return " saved " + counter + " compounds to ListOfCompounds.txt";
    }
    
    // Scans the compound's formula and picks out it's elements
    // to generate that compound's ArrayList of elements

  public ArrayList<Element> createElementsFromFormula(String formula) {
  	ArrayList<Element> elementsList = new ArrayList<Element>();
      String elementSymbol = "";
      String numOfAtoms = "";
      Element element;
      
     for (int i = 0; i < formula.length(); i++) {
    	 if(Character.isUpperCase(formula.charAt(i)) && i !=0) {
    		 element = pT.getElementBySymbol(elementSymbol);
    		 if(numOfAtoms.equals("")) {
    			 element.setNumAtoms(1);
    		 }
    		 else {
    			 element.setNumAtoms(Integer.parseInt(numOfAtoms));
    		 }
    		 elementsList.add(element);
    		 elementSymbol = "";
    		 numOfAtoms = "";
    		 element = null;
    	 }
    	 if(!(Character.isDigit(formula.charAt(i)))) {
    		 elementSymbol += formula.charAt(i);
    	 }
    	 else {
    		 numOfAtoms += formula.charAt(i);
//    		 System.out.println(numOfAtoms);
    	 }
     }
     
     element = pT.getElementBySymbol(elementSymbol);
     if (numOfAtoms.equals("")) {
    	 element.setNumAtoms(1);
     }
     else {
    	 element.setNumAtoms(Integer.valueOf(numOfAtoms));
     }
     elementsList.add(element);  			
     
     return elementsList;
  }
    
  	// Receives a compound as a parameter and returns an observable list of all compounds that 
  	//contain that element, ordered by their name
	
  public ObservableList<CompoundElement> getCompoundsContainElement(Element element) {
		ObservableList<CompoundElement> compounds_That_Contain_Element  = FXCollections.observableArrayList();
		for (int i = 0; i < allCompounds.size(); i++) {
			for (int j = 0; j < allCompounds.get(i).getElements().size(); j++) {
				if(allCompounds.get(i).getElements().get(j).getName().equals(element.getName())) {
					compounds_That_Contain_Element.add(allCompounds.get(i));
				}
			}
		}	
		Collections.sort(compounds_That_Contain_Element, new CompoundNameComparator());
		return compounds_That_Contain_Element;
	}
	
  	// Receives a State as a parameter and returns an observable list of
  	// all compounds, ordered by their name,  that contain at 
  	// least one element for a particular state. For example, 
  	// if the state was “Gas”, then a list of all compounds with any of
  	// these elements should be returned: H, He, N, O, F, Ne, Cl, Ar, Kr, Xe, Rn.
  
	public ObservableList<CompoundElement> getCompoundsContainElement_WithState (String elementState) {
		ObservableList<CompoundElement> compounds_That_Contain_Element_With_State  = FXCollections.observableArrayList();
		for(int i = 0; i < allCompounds.size(); i++) {
			for (int j = 0; j <allCompounds.get(i).getElements().size(); j++) {
				if(allCompounds.get(i).getElements().get(j).getState().equals(elementState)) {
					if(!(compounds_That_Contain_Element_With_State.contains(allCompounds.get(i)))) {
						compounds_That_Contain_Element_With_State.add(allCompounds.get(i));
					}
				}
			}
		}
		Collections.sort(compounds_That_Contain_Element_With_State, new CompoundNameComparator());
		return compounds_That_Contain_Element_With_State;
	}
	
// Receives a Group as a parameter and returns an observable list of all compounds,
	// ordered by their name, that contain at least one element for a particular group. 
	// For example, if the group was “Alkali metals”, then a list of all compounds with 
	// any of these elements should be returned: Li, Na, K, Rb, Cs, Fr.
	public ObservableList<CompoundElement> getCompoundsContainElement_WithGroup (String elementGroup) {
		
		ObservableList<CompoundElement> compounds_That_Contain_Element_With_Group  = FXCollections.observableArrayList();
		for(int i = 0; i < allCompounds.size(); i++) {
			for (int j = 0; j < allCompounds.get(i).getElements().size(); j++) {
				if(allCompounds.get(i).getElements().get(j).getGroup().equals(elementGroup)) {
					if(!(compounds_That_Contain_Element_With_Group.contains(allCompounds.get(i)))) {
						compounds_That_Contain_Element_With_Group.add(allCompounds.get(i));
					}
				}
			}
		}
		Collections.sort(compounds_That_Contain_Element_With_Group, new CompoundNameComparator());
		return compounds_That_Contain_Element_With_Group;
	}
	
	// Receives two compounds as parameters and returns the list of elements that are in common
	// between those two elements
	public ObservableList<Element> getElementsInCommon(CompoundElement compoundA, CompoundElement compoundB) {
		ObservableList<Element> commonElements = FXCollections.observableArrayList();
		for(int i = 0; i < compoundA.getElements().size(); i++) {
				if(compoundB.getElements().contains(compoundA.getElements().get(i))) {
					if(!(commonElements.contains(compoundA.getElements().get(i)))) {
						commonElements.add(compoundA.getElements().get(i));
					}
				}
		}
		Collections.sort(commonElements, new AtomicNumberComparator());
		return commonElements;
	}
	
	// Receives two doubles as parameters and returns the list of compounds with molecular
	// mass value between those two doubles
	public ObservableList<CompoundElement> getCompounds_WithinMassRange(double mMassA, double  mMassB) {
		
		double upperLimit, lowerLimit;
		if(mMassA > mMassB) {
			upperLimit = mMassA;
			lowerLimit = mMassB;
		}else {
			upperLimit = mMassB;
			lowerLimit = mMassA;
		}
		
		ObservableList<CompoundElement> compounds_In_Mass_Range  = FXCollections.observableArrayList();
		for(int i = 0; i < allCompounds.size(); i++) {
			if(allCompounds.get(i).getMolecularMass() < upperLimit  && allCompounds.get(i).getMolecularMass() > lowerLimit) {
				compounds_In_Mass_Range.add(allCompounds.get(i));
			}
		}
		Collections.sort(compounds_In_Mass_Range, new CompoundNameComparator());

		return compounds_In_Mass_Range;
	}	
	
	public ObservableList<CompoundElement> getAllCompounds_OrderedName() {
		Collections.sort(allCompounds, new CompoundNameComparator());
		return allCompounds;
	}
	
	public ObservableList<CompoundElement> getAllCompounds_OrderedFormula() {
		Collections.sort(allCompounds, new FormulaComparator());
		return allCompounds;
	}
	
	public ObservableList<CompoundElement> getAllCompounds_OrderedHillFormula() {
		Collections.sort(allCompounds, new HillFormulaComparator());
		return allCompounds;
	}
	
	public ObservableList<CompoundElement> getAllCompounds_OrderedMolecularMass() {
		Collections.sort(allCompounds, new MolecularMassComparator());
		return allCompounds;
	}
	
	
	// generates a List of doubles to be used in the gui's mass range combobox
	// (used when you find compounds in between two mass ranges)
	private ObservableList<Double> generate() {
		double lowest = 0.0;
		ObservableList<Double> list = FXCollections.observableArrayList();
		for (int i = 0; i < 48; i++ ) {
			list.add(lowest);
			if(lowest < 100.0) {
				lowest+= 10.0;
				continue;
			}
			if(lowest < 1000.0) {
				lowest +=100.0;
				continue;
			}
			if(lowest < 10000.0) {
				lowest += 1000;
				continue;
			}
			if(lowest < 100000.0) {
				lowest += 10000.0;
				continue;
			}
			if(lowest == 100000) {
				lowest +=400000;
			}
			else {
				lowest += 500000.0;
				continue;
			}
			
		}
		return list;
	}
	
	// generates a list of integers to put in the comboboxes
	// when deciding how many of each atom will be in a compound
	private ObservableList<Integer> generateIntegers() {
		ObservableList<Integer> list = FXCollections.observableArrayList(); 
		int number = 0;
		for(int i =0; i < 300; i++ ) {
			number++;
			list.add(number);
		}
		return list;
	}
	public ObservableList<Integer> getIntegers(){
		return numAtomsList;
	}
	
	public ObservableList<Double> getMassRange(){
		return massRange;
	}
	
	public void addCompounds(CompoundElement compound) {
		if(!this.allCompounds.contains(compound)) {
			this.allCompounds.add(compound);
		}
	}
	
	public void removeCompound(CompoundElement compound) {
		if(this.allCompounds.contains(compound)) {
			this.allCompounds.remove(compound);
		}
	}
}