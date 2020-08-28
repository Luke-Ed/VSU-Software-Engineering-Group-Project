package laboratory;
//
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;
import javafx.scene.control.MenuItem;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.event.ActionEvent;
import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.ResourceBundle;

public class ScienceLabController implements Initializable{
	private PeriodicTable pt;
	private ChemicalPlayground playground;
	
	// these really shouldn't be used here
	// they should be used in ChemicalPlayground.java
	ArrayList<Element> newCompoundsElements;
	ArrayList<Element> editedCompoundsElements;
	
	
	// Element menu items
	@FXML	// "All Elements" calls tvDisplayElements
	private MenuItem miDisplayElements;
	@FXML	// "Solid Elements" calls tvDisplaySolidElements
	private MenuItem miDisplaySolidElements;
	@FXML	// "Liquid Elements" calls tvDisplayLiquidElements
	private MenuItem miDisplayLiquidElements;
	@FXML	// "Gas Elements" calls tvDisplayGasElements
	private MenuItem miDisplayGasElements;
	@FXML	// "Unknown Elements" calls tvDisplayUnknownElements
	private MenuItem miDisplayUnknownElements;

	//compound menu items
	@FXML	// "By Name" calls tvDisplayCompounds_ByName
	private MenuItem miDisplayCompoundsByName;
	@FXML	// "By Formula" calls tvDisplayCompounds_ByFormula
	private MenuItem miDisplayCompoundsByFormula;
	@FXML	// "By HillFormula" calls tvDisplayCompounds_ByHillFormula
	private MenuItem miDisplayCompoundsByHillFormula;
	@FXML	// "By Mass" calls tvDisplayCompounds_ByMass
	private MenuItem miDisplayCompoundsByMolecularMass;

	// File tab
	// Read/Write buttons that read/write to/from ListOfCompounds.txt
	@FXML	// "Import from ListOfCompounds.txt" on the File tab calls readListOfCompounds
	private MenuItem miReadCompounds;
	@FXML	// "Export to ListOfCompounds.txt" on the File tab calls writeListOfCompounds
	private MenuItem miWriteCompounds;
	
	
	// "+" adds an element to an edited compound. not to be confused
	// with btnAddElementToCompound calls addElementToEditCompound
	@FXML 
	private Button btnAddElementToEditCompound;
	@FXML // "Remove Compound" calls removeCompound 
	private Button btnRemoveCompound;
		
	@FXML	// "Merge" calls combine2Compounds
	private Button btnCombineCompounds;
	@FXML	// "Display" (On View tab below Cmpd1 and Cmpd 2) calls displayCommonElements
	private Button btnFindCommonElements;
	@FXML	// "Make Changes" calls addEditedCompound
	private Button btnAddEditedCompound;

	
	// "+" adds an element to a NEW compound. not to be confused
	// with btnAddElementToEditCompound calls addElementToCompound
	@FXML 
	private Button btnAddElementToCompound;
	@FXML	// "Add Compound" calls addNewCompound
	private Button btnCreateCompound;
	
	// Mass range 
	@FXML // "Display" calls tvDisplayCompounds_InMassRange
	private Button btnCompoundsInRange;
	
	// table view for the elements
	@FXML
	private TableView<Element> elementTableView;
	// element table view's columns
	@FXML
	private TableColumn<Element,String> eNameCol;
	@FXML
	private TableColumn<Element,String> eSymbolCol;
	@FXML
	private TableColumn<Element,Double> eAtomMassCol;
	@FXML
	private TableColumn<Element,String> eStateCol;
	@FXML
	private TableColumn<Element,String> eGroupCol;
	@FXML
	private TableColumn<Element,Integer> eAtomNumCol;
	
	// compound table view
	@FXML
	private TableView<CompoundElement> compoundTableView;
	// compound table view's columns
	@FXML
	private TableColumn<CompoundElement, String> cNameCol;
	@FXML
	private TableColumn<CompoundElement, String> cFormulaCol;
	@FXML
	private TableColumn<CompoundElement, String> cHillFormCol;
	@FXML
	private TableColumn<CompoundElement, Double> cMassCol;
	
	// compound combo boxes for finding common Elements (View tab)
	@FXML // "Cmpd 1"
	private ComboBox <CompoundElement> allCompoundsComboBoxA;
	@FXML // "Cmpd 2"
	private ComboBox <CompoundElement> allCompoundsComboBoxB;
	
	// compound combo boxes for combining compounds (Edit tab)
	@FXML // "Cmpd 1"
	private ComboBox <CompoundElement>allCompoundsComboBoxC;
	@FXML // "Cmpd 2"
	private ComboBox <CompoundElement> allCompoundsComboBoxD;
	
	
	@FXML // "Element" (View tab) calls tvDisplayCompounds_WithElement
	private ComboBox<Element> elementComboBox;
	
	@FXML // "Elements" combo box used when creating a new compound 
	private ComboBox<Element> elementComboBox2;
	
	@FXML // combo box used when editing a compound 
	private ComboBox<Element> elementComboBox3;
	
	@FXML // "#" used to specify how many of each atom when creating a new compound
	private ComboBox<Integer> intComboBox;
	@FXML // "#" used to specify how many of each atom when editing a compound
	private ComboBox<Integer> intComboBox2;
	
	@FXML // "Mass Min" (View tab)
	private ComboBox <Double> doublesComboBoxA;
	@FXML // "Mass Max" (View tab)
	private ComboBox <Double> doublesComboBoxB;
	
	// compounds with elements in state + Group
	@FXML // "State" calls tvDisplayCompounds_WithElementInState
	private ComboBox<String> stateComboBox;
	@FXML // "Group" calls tvDisplayCompounds_WithElementInGroup
	private ComboBox<String> groupComboBox;
		
	@FXML // "Name" (Edit tab) name given to new compound 
	private TextField combinedName;
	@FXML // "Edit Name" (Edit tab) new name given to compound
	private TextField editCompound;
	@FXML // "New Compound" name of new compound
	private TextField addCompound;
	
	// Labels
	@FXML 	// bottom right
	private Label rightStatus;
	@FXML	// bottom left
	private Label leftStatus;
	@FXML	// below "Periodic Table of the Elements"
	private Label elementsLabel;
	

	
	public ScienceLabController() {
		// TODO Auto-generated constructor stub
		pt = new PeriodicTable();
		playground = new ChemicalPlayground();
		// Element columns
		eNameCol = new TableColumn<>(" Name    ");
		eSymbolCol = new TableColumn<>(" Symbol ");
		eAtomMassCol = new TableColumn<>(" Atomic Mass   ");
		eStateCol = new TableColumn<>("State    ");
		eGroupCol = new TableColumn<>("Group    ");
		eAtomNumCol = new TableColumn<>(" Atomic Number ");
		// Compound columns
		cNameCol = new TableColumn<>(" Name    ");
		cFormulaCol = new TableColumn<>(" Formula   ");
		cHillFormCol = new TableColumn<>(" Hill Formula   ");
		cMassCol = new TableColumn<>(" Molecular Mass   ");

		// need to be moved to Chemical Playground
		newCompoundsElements = new ArrayList<Element>();
		editedCompoundsElements = new ArrayList<Element>();

	}	
	
	// When btnAddElementToEditCompound "+" button is pressed, the selected element in the
	// combobox3 and the number of atoms (number of atoms comes from the value selected in intComboBox2) 
	// is added to an arrayList of Elements that belong in the compound the user is editing
	@FXML
	private void addElementToEditCompound(ActionEvent event) throws IOException {
		Element addedElement = elementComboBox3.getValue();
		
		addedElement.setNumAtoms(intComboBox2.getValue());
		
		editedCompoundsElements.add(addedElement);
		leftStatus.setText("added "+intComboBox2.getValue().toString() + " atoms of " + elementComboBox3.getValue().getName() + " to " + editCompound.getText());
		//leftStatus.setText();
	}
	
	// When btnAddEditedCompound "Make Changes" is pressed, the selected compound from the tableview
	// is edited and it's new name will be the String in editCompound textField "Edit Name". The compound's
	// new elements will be the Arraylist created in addElementToEditCompound(). The ArrayList will
	// be cleared after the compound is added to the list
	@FXML
	private void addEditedCompound(ActionEvent event) throws IOException {
		String newName = editCompound.getText();
		String oldName = compoundTableView.getSelectionModel().getSelectedItem().getName();
		String oldFormula = compoundTableView.getSelectionModel().getSelectedItem().getFormula();
		ArrayList<Element> elementList = new ArrayList<Element>();
		elementList.addAll(editedCompoundsElements);
		
		CompoundElement editedCompound = compoundTableView.getSelectionModel().getSelectedItem();
		
		editedCompound.setName(newName);
		editedCompound.replaceElementsInCompound(elementList);

		allCompoundsComboBoxA.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxB.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxC.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxD.setItems(playground.getAllCompounds_OrderedName());
		
		editedCompoundsElements.clear();
		
		leftStatus.setText("edited " + oldName +", new name: " + editedCompound.getName() + 
				", old formula: " + oldFormula +", new formula: " + editedCompound.getFormula()); 
	}
	
	// calls the saveCompounds() method in ChemicalPlayground
	@FXML
	private void writeListOfCompounds(ActionEvent event) throws IOException {
		
		leftStatus.setText(playground.saveCompounds());
		
		allCompoundsComboBoxA.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxB.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxC.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxD.setItems(playground.getAllCompounds_OrderedName());
	}
	
	// calls the importCompoundsFromFile() method in ChemicalPlayground
	@FXML
	private void readListOfCompounds(ActionEvent event) throws IOException {
		
		leftStatus.setText(playground.importCompoundsFromFile());
		
		allCompoundsComboBoxA.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxB.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxC.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxD.setItems(playground.getAllCompounds_OrderedName());
//		allCompounds = playground.getAllCompounds_OrderedName();
	}
	
	// This is called when btnCombineCompounds "Merge" is pressed. A new Compound is made by taking the
	// compounds selected in allCompoundsComboBoxC and allCompoundsComboBoxD and passing them to the CompoundElement
	// constructor that takes 2 compounds and a String. The Name of the compound will be the value that is written into
	// the editCompound TextField "Edit Name". The compound is then added to the list of all compounds
	@FXML
	private void combine2Compounds(ActionEvent event) throws IOException {
		
		String name = combinedName.getText();
		CompoundElement compoundsCombined = new CompoundElement(name, allCompoundsComboBoxC.getValue(),allCompoundsComboBoxD.getValue());
		playground.addCompounds(compoundsCombined);
		
		allCompoundsComboBoxA.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxB.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxC.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxD.setItems(playground.getAllCompounds_OrderedName());
		
		leftStatus.setText("combined " + allCompoundsComboBoxC.getValue().getName() + " with " + allCompoundsComboBoxD.getValue().getName()
				+ " to make " + compoundsCombined.getName());
	}
	
	// When btnAddElementToCompound "+" button is pressed, the selected element in the
	// combobox2 and the number of atoms (number of atoms comes from the value selected in intComboBox) 
	// is added to an arrayList of Elements that belong in the compound the user is creating
	@FXML
	private void addElementToNewCompound(ActionEvent event) throws IOException {
		Element addedElement = elementComboBox2.getValue();
		
		addedElement.setNumAtoms(intComboBox.getValue());
		
		newCompoundsElements.add(addedElement);
		leftStatus.setText("added "+intComboBox.getValue().toString() + " atoms of " + elementComboBox2.getValue().getName() + " to " + addCompound.getText());
	}
	
	// Removes the compound selected in the tableview when 
	// btnRemoveCompound "Remove Compound" is pressed
	@FXML
	private void removeCompound(ActionEvent event) throws IOException {
		leftStatus.setText("compound " + compoundTableView.getSelectionModel().getSelectedItem().getName() + " removed from compound list");
		CompoundElement removed = compoundTableView.getSelectionModel().getSelectedItem();
		
		playground.removeCompound(removed);	
	}
	
	// When btnCreateCompound "Add Compound" is pressed it creates a Compound
	// and adds it to the list of Compounds
	@FXML
	private void addNewCompound(ActionEvent event) throws IOException {
		String name = addCompound.getText();
		ArrayList<Element> elementList = new ArrayList<Element>();
		elementList.addAll(newCompoundsElements);
		
		CompoundElement newCompound = new CompoundElement(name, elementList);
		playground.addCompounds(newCompound);
		allCompoundsComboBoxA.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxB.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxC.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxD.setItems(playground.getAllCompounds_OrderedName());
		
		newCompoundsElements.clear();
		leftStatus.setText("added " + newCompound.getName() + " to compounds"); 
	}
	
	// when btnFindCommonElements "Display" is pressed, the element tableview 
	// will display what elements the compounds selected in allCompoundsComboBoxA and 
	// allCompoundsComboBoxB have in common
	@SuppressWarnings("unchecked")
	@FXML
	private void displayCommonElements (ActionEvent event) throws IOException {
		CompoundElement compoundA = allCompoundsComboBoxA.getSelectionModel().getSelectedItem();
		CompoundElement compoundB =  allCompoundsComboBoxB.getSelectionModel().getSelectedItem();

		elementsLabel.setText("Common between " + compoundA.getName() + " and " + compoundB.getName());

		elementTableView.setEditable(true);
		elementTableView.getColumns().clear();
		elementTableView.setItems(playground.getElementsInCommon(compoundA, compoundB));

		eNameCol.setCellValueFactory(new PropertyValueFactory<Element, String>("Name"));
		eSymbolCol.setCellValueFactory(new PropertyValueFactory<Element, String>("Symbol"));
		eAtomMassCol.setCellValueFactory(new PropertyValueFactory<Element,Double>("AtomicMass"));
		eStateCol.setCellValueFactory(new PropertyValueFactory<Element,String>("State"));
		eGroupCol.setCellValueFactory(new PropertyValueFactory<Element,String>("Group"));
		eAtomNumCol.setCellValueFactory(new PropertyValueFactory<Element,Integer>("AtomicNumber"));
		
		elementTableView.getColumns().addAll(eAtomNumCol,eNameCol,eSymbolCol, eAtomMassCol,eStateCol,eGroupCol);
		rightStatus.setText("viewing common elements between " + compoundA.getName() + " and " + compoundB.getName());
	}
	
	// When an element in elementComboBox is selected, the compound tableview will
	// display all the compounds that contain that element
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayCompounds_WithElement(ActionEvent event) throws IOException {
		
		compoundTableView.setEditable(true);
		compoundTableView.getColumns().clear();//.getItems().clear();
		compoundTableView.setItems(playground.getCompoundsContainElement(elementComboBox.getValue()));
		
		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));

		compoundTableView.getColumns().addAll(cNameCol,cFormulaCol,cHillFormCol,cMassCol);		
		leftStatus.setText("viewing compounds that contain " + elementComboBox.getValue().getName());
	}
	
	// When btnCompoundsInRange "Display" is pressed, the list of compounds whose molecular mass is
	// in range of the values selected in doublesComboBoxA and doublesComboBoxB is displayed in the 
	// compound tableview
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayCompounds_InMassRange(ActionEvent event) throws IOException {
		
		compoundTableView.setEditable(true);
		compoundTableView.getColumns().clear();//.getItems().clear();
		compoundTableView.setItems(playground.getCompounds_WithinMassRange(doublesComboBoxA.getValue(), doublesComboBoxB.getValue()));
		
		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));

		compoundTableView.getColumns().addAll(cNameCol,cFormulaCol,cHillFormCol,cMassCol);		
		leftStatus.setText("viewing compounds with molecular mass between " + doublesComboBoxA.getValue() + " and " +doublesComboBoxB.getValue());
	}
	
	// When a state is selected in stateComboBox "State", all compounds, ordered by their name, that contain  
	// at least one element for that particular state are shown in the compound tableview. 
	// For example, if the state was “Gas”, then a list of all compounds with any of 
	// these elements should show in the tableview: H, He, N, O, F, Ne, Cl, Ar, Kr, Xe, Rn.
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayCompounds_WithElementInState(ActionEvent event) throws IOException {
		String state = stateComboBox.getValue();
		
		compoundTableView.setEditable(true);
		compoundTableView.getColumns().clear();//.getItems().clear();
		compoundTableView.setItems(playground.getCompoundsContainElement_WithState(stateComboBox.getValue()));
		
		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));

		compoundTableView.getColumns().addAll(cNameCol,cFormulaCol,cHillFormCol,cMassCol);
	
		if(state.equals("gas") ) {
			leftStatus.setText("viewing compounds containing elements that are " + state + "es" );
		}else if(state.equals("unknown")) {
			leftStatus.setText("viewing compounds containing elements with " + state + " state");
		}else {
			leftStatus.setText("viewing compounds containing elements that are " + state + "s");
		}
	}
	
	// When a group is selected in groupComboBox "Group",all compounds, ordered by their name, that contain 
	// at least one element for a particular group are shown in the compound tableview. 
	// For example, if the group was “Alkali metals”, then a list of all compounds with any 
	// of these elements should be returned: Li, Na, K, Rb, Cs, Fr.
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayCompounds_WithElementInGroup(ActionEvent event) throws IOException {
		String group = groupComboBox.getValue();
		
		compoundTableView.setEditable(true);
		compoundTableView.getColumns().clear();//.getItems().clear();
		compoundTableView.setItems(playground.getCompoundsContainElement_WithGroup(groupComboBox.getValue()));
		
		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));
		
		compoundTableView.getColumns().addAll(cNameCol,cFormulaCol,cHillFormCol,cMassCol);	
		leftStatus.setText("viewing compounds containing elements in " + group +" group");
		
	}
	
	// when miDisplayElements "All Elements" is pressed, the element tableview
	// will display all elements
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayElements(ActionEvent event) throws IOException {
		elementTableView.setEditable(true);
		elementTableView.getColumns().clear();//.getItems().clear();
		elementTableView.setItems(pt.getAllElements());

		elementTableView.getColumns().addAll(eAtomNumCol,eNameCol,eSymbolCol, eAtomMassCol,eStateCol,eGroupCol);
		rightStatus.setText("viewing all elements");
		elementsLabel.setText("All Elements");
		
	}
	
	// when miDisplaySolidElements "Solid Elements" is pressed, the element
	// tableview will display all solid elements
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplaySolidElements(ActionEvent event) throws IOException {
		
		elementTableView.setEditable(true);
		elementTableView.getColumns().clear();//	elementTableView.getItems().clear();
		elementTableView.setItems(pt.getSolidElements());

		elementTableView.getColumns().addAll(eAtomNumCol,eNameCol,eSymbolCol, eAtomMassCol,eStateCol,eGroupCol);
		rightStatus.setText("viewing elements with solid State");
		elementsLabel.setText("Solid Elements");
	}
	
	// when miDisplayLiquidElements "Liquid Elements" is pressed, the element
	// tableview will display all liquid elements
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayLiquidElements(ActionEvent event) throws IOException {
		
		elementTableView.setEditable(true);
		elementTableView.getColumns().clear();//	elementTableView.getItems().clear();
		elementTableView.setItems(pt.getLiquidElements());
		
		elementTableView.getColumns().addAll(eAtomNumCol,eNameCol,eSymbolCol, eAtomMassCol,eStateCol,eGroupCol);
		rightStatus.setText("viewing elements with liquid State");
		elementsLabel.setText("Liquid Elements");
	}
	
	// when miDisplayGasElements "Gas Elements" is pressed, the element
	// tableview will display all Gas elements
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayGasElements(ActionEvent event) throws IOException {
		
		elementTableView.setEditable(true);
		elementTableView.getColumns().clear();
		elementTableView.setItems(pt.getGasElements());
		
		elementTableView.getColumns().addAll(eAtomNumCol,eNameCol,eSymbolCol, eAtomMassCol,eStateCol,eGroupCol);
		
		rightStatus.setText("viewing elements with gas State");
		elementsLabel.setText("Gas Elements");
	}
	
	// when miDisplayUnknownElements "Unknown Elements" is pressed, the element
	// tableview will display all Unknown elements
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayUnknownElements(ActionEvent event) throws IOException {
		
		elementTableView.setEditable(true);
		elementTableView.getColumns().clear();
		elementTableView.setItems(pt.getUnknownElements());
		
		elementTableView.getColumns().addAll(eAtomNumCol,eNameCol,eSymbolCol, eAtomMassCol,eStateCol,eGroupCol);
		rightStatus.setText("viewing elements with unknown State");
		elementsLabel.setText("Unknown Elements");
	}
	
	// When miDisplayCompoundsByName "By Name" is pressed, all compounds will be displayed
	// in the compound tableview ordered by their names
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayCompounds_ByName(ActionEvent event) throws IOException {
	
		compoundTableView.setEditable(true);
		compoundTableView.getColumns().clear();
		compoundTableView.setItems(playground.getAllCompounds_OrderedName());
		
		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));

		compoundTableView.getColumns().addAll(cNameCol,cFormulaCol,cHillFormCol,cMassCol);

		leftStatus.setText("viewing all compounds by Name");
	}
	
	// When miDisplayCompoundsByFormula "By Formula" is pressed, all compounds will be displayed
	// in the compound tableview ordered by their formula
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayCompounds_ByFormula(ActionEvent event) throws IOException {
		compoundTableView.setEditable(true);
		compoundTableView.getColumns().clear();//.getItems().clear();
		compoundTableView.setItems(playground.getAllCompounds_OrderedFormula());
		
		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));
		
		compoundTableView.getColumns().addAll(cNameCol,cFormulaCol,cHillFormCol,cMassCol);

		leftStatus.setText("viewing all compounds by Formula");
	}
	
	// When miDisplayCompoundsByHillFormula "By HillFormula" is pressed, all compounds will be displayed
	// in the compound tableview ordered by their hill formula
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayCompounds_ByHillFormula(ActionEvent event) throws IOException {
		compoundTableView.setEditable(true);
		compoundTableView.getColumns().clear();//.getItems().clear();
		compoundTableView.setItems(playground.getAllCompounds_OrderedHillFormula());

		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));
			
		compoundTableView.getColumns().addAll(cNameCol,cFormulaCol,cHillFormCol,cMassCol);

		leftStatus.setText("viewing all compounds by HillFormula");
	}
	
	// When miDisplayCompoundsByMolecularMass "By Mass" is pressed, all compounds will be displayed
	// in the compound tableview ordered by their molecular mass
	@SuppressWarnings("unchecked")
	@FXML
	private void tvDisplayCompounds_ByMass(ActionEvent event) throws IOException {
		compoundTableView.setEditable(true);
		compoundTableView.getColumns().clear();//.getItems().clear();
		compoundTableView.setItems(playground.getAllCompounds_OrderedMolecularMass());
		
		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));
		
		compoundTableView.getColumns().addAll(cNameCol,cFormulaCol,cHillFormCol,cMassCol);
		
		leftStatus.setText("viewing all compounds by mass");
	}
	
	@SuppressWarnings("unchecked")
	@Override
	public void initialize(URL url, ResourceBundle rb) {
		
		elementComboBox.setItems(pt.getAllElements());
		elementComboBox2.setItems(pt.getAllElements());
		elementComboBox3.setItems(pt.getAllElements());
		
		allCompoundsComboBoxA.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxB.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxC.setItems(playground.getAllCompounds_OrderedName());
		allCompoundsComboBoxD.setItems(playground.getAllCompounds_OrderedName());
		
		doublesComboBoxA.setItems(playground.getMassRange());
		doublesComboBoxB.setItems(playground.getMassRange());
		
		intComboBox.setItems(playground.getIntegers());
		intComboBox2.setItems(playground.getIntegers());
		
		stateComboBox.getItems().addAll("Solid","Liquid","Gas","Unknown");
		groupComboBox.getItems().addAll( "Alkali Metal", "Alkaline Earth Metal", "Lanthanoid",
		"Actinoid","Transition Metal","Post Transition Metal","Metalloid","Reactive Nonmetal",
		"Noble Gas","Unknown");		
		
		// element table and columns
		elementTableView.setItems(pt.getAllElements());
		eNameCol.setCellValueFactory(new PropertyValueFactory<Element, String>("Name"));
		eSymbolCol.setCellValueFactory(new PropertyValueFactory<Element, String>("Symbol"));
		eAtomMassCol.setCellValueFactory(new PropertyValueFactory<Element,Double>("AtomicMass"));
		eStateCol.setCellValueFactory(new PropertyValueFactory<Element,String>("State"));
		eGroupCol.setCellValueFactory(new PropertyValueFactory<Element,String>("Group"));
		eAtomNumCol.setCellValueFactory(new PropertyValueFactory<Element,Integer>("AtomicNumber"));
		elementTableView.getColumns().addAll(eAtomNumCol,eNameCol,eSymbolCol, eAtomMassCol,eStateCol,eGroupCol);
		
		// compound table and columns
		compoundTableView.setItems(playground.getAllCompounds_OrderedName());		
		cNameCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Name"));
		cFormulaCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("Formula"));
		cHillFormCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, String>("HillFormula"));
		cMassCol.setCellValueFactory(new PropertyValueFactory<CompoundElement, Double>("MolecularMass"));
		compoundTableView.getColumns().addAll(cNameCol, cFormulaCol, cHillFormCol,cMassCol);

	}	
}