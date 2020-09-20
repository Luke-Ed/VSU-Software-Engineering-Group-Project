package laboratory;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.lang.reflect.Array;
import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;


public class ChemicalPlaygroundTest {


  @Test
  @DisplayName("Test to create an element from a given formula")
  void testCreateElementFromFomula(){
    //creating a ChemicalPlayground object to call the createElementFromFormula method
    ChemicalPlayground cp = new ChemicalPlayground();
    String compounds = cp.importCompoundsFromFile();

    //creating a String for the formula for water H2O
    String formula = "H2O1";

    //making an array list with the createElementFromFormula method called elementsByFormula.
    System.out.println(formula);
    ArrayList<Element> elementsByFormula = cp.createElementsFromFormula(formula);

    //Creating a second array list to compare to the first one but put the elements in one by one
    ArrayList<Element> elementsByHand = new ArrayList<>();

    //Creating the elements to put into the array elementsByHand
    Element hydrogen = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
    Element oxygen = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
    elementsByHand.add(hydrogen);
    elementsByHand.add(oxygen);

    //sorting both the array to compare them
    ElementNameCompare elementNameCompare = new ElementNameCompare();
    elementsByFormula.sort(elementNameCompare);
    elementsByHand.sort(elementNameCompare);

    //Assert the both the arrays are equal that they both contain Hydrogen and Oxygen.
    assertEquals(elementsByFormula,elementsByHand);

  }

  @Test
  @DisplayName("Test getCompounds_WithinMassRange")
  void testGetCompounds_WithinMassRange(){
    //Creating a ChemicalPlayground to use the getCompounds_WithinMassRange
    ChemicalPlayground cp = new ChemicalPlayground();

    //Creating the ObservableList through the getCompounds_WithinMassRange
    ObservableList<CompoundElement> compounds_withinMassRange = cp.getCompounds_WithinMassRange(10.0, 60.0);

    //Creating another ObservableList to compare the the other one.
    ObservableList<CompoundElement> allCompounds = cp.getAllCompounds_OrderedMolecularMass();

    //ArrayList to have the expectedCompoundElements within the Range
    ArrayList<CompoundElement> expectedCompoundElementsArrayList = new ArrayList<>();


    //For each loop to get all the compounds within the MassRange of 10 and 60
    for(CompoundElement ce : allCompounds){
      if(ce.getMolecularMass() >= 10.0 && ce.getMolecularMass()<= 60.0){
        expectedCompoundElementsArrayList.add(ce);
      }
    }

    ObservableList<CompoundElement> expectedCompoundElements = FXCollections.observableArrayList(expectedCompoundElementsArrayList);

    //Sort them by there MolecularMass
    compounds_withinMassRange.sort(new MolecularMassComparator());
    allCompounds.sort(new MolecularMassComparator());

    //assertEquals to see if the 2 ObservableList are equal to each other to show that the compounds_WithinMassRange is the same as expectedCompoundElements
    assertEquals(expectedCompoundElements,compounds_withinMassRange);

    //assert that the expectedCompoundElements list is not the same as the allCompounds list.
    assertNotEquals(allCompounds,expectedCompoundElements);


  }

  @Test
  @DisplayName("Test getCompoundsContainElement")
  void testGetCompoundsContainElement(){
    //Creating a ChemicalPlayground and an Element to use for the method getCompoundsContainElement.
    ChemicalPlayground cp = new ChemicalPlayground();
    Element oxygen = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);

    //Making the ObservableList one with all the compounds and the other with the compounds that contain oxygen.
    ObservableList<CompoundElement> allCompoundElements = cp.getAllCompounds_OrderedName();
    ObservableList<CompoundElement> elementsWithElement = cp.getCompoundsContainElement(oxygen);

    //Create an ArrayList of all the CompoundElements that contain oxygen in it.
    ArrayList<CompoundElement> expectedCompoundElementsArrayList = new ArrayList<>();


    //Filter the allCompoundElements to who contain the element oxygen in it.
    allCompoundElements.stream().filter(x -> x.getElements().contains(oxygen)).forEach(expectedCompoundElementsArrayList::add);

    //Take the ArrayList and make it into an ObservableList
    ObservableList<CompoundElement> expectedCompoundElements = FXCollections.observableArrayList(expectedCompoundElementsArrayList);

    //Sorting the ObservableList by there name so they are both ordered the same
    elementsWithElement.sort(new CompoundNameComparator());
    expectedCompoundElements.sort(new CompoundNameComparator());

    //Assert the expectedCompoundElements list is the same as the ElementsWithElement
    assertEquals(expectedCompoundElements, elementsWithElement);

    //Assert the the expectedCompoundElements does not equal the allCompoundElements
    assertNotEquals(allCompoundElements,expectedCompoundElements);


  }

  @Test
  @DisplayName("Test getCompoundsContainElement_WithGroup")
  void testGetCompoundsContainElement_WithGroup(){
    //Creating a ChemicalPlayground to use the getCompoundsContainElement_WithGroup
    ChemicalPlayground cp = new ChemicalPlayground();

    //String for the group of Alkali metals
    String group = "Alkali metals";

    //ObservableList with all the compound elements
    ObservableList<CompoundElement> allCompoundElements = cp.getAllCompounds_OrderedName();

    //ObservableList with only the elements from the group Alkali metals.
    ObservableList<CompoundElement> elementsWithGroup = cp.getCompoundsContainElement_WithGroup(group);

    //ArrayList to put the elements with the group Alkali Metal into it.
    ArrayList<CompoundElement> expectedCompoundElementsArrayList = new ArrayList<>();

    //Loop going through all the CompoundElements then the Elements to get the group and if it equals Alkali metal then put it into the ArrayList
    for(CompoundElement ce: allCompoundElements){
        for(Element e : ce.getElements()){
            if(e.getGroup().equals(group)){
              expectedCompoundElementsArrayList.add(ce);
            }
        }
    }

    //Take the ArrayList and turn it into an ObservableList
    ObservableList<CompoundElement> expectedCompoundElements = FXCollections.observableArrayList(expectedCompoundElementsArrayList);

    //Sort both the list using the CompoundNameComparator to make sure that both list are in the same order.
    elementsWithGroup.sort(new CompoundNameComparator());
    expectedCompoundElements.sort(new CompoundNameComparator());

    //Assert that the expectedCompoundElements list is the same as the elementsWithGroup list
    assertEquals(expectedCompoundElements,elementsWithGroup);

    //Assert to show that the allCompoundElements list is not equal to the expectedCompoundElements
    assertNotEquals(allCompoundElements,expectedCompoundElements);


  }

  @Test
  @DisplayName("Test getCompoundsContainElement_WithState")
  void testGetCompoundsContainElement_WithState(){
    //Creating a ChemicalPlayground to use the getCompoundsContainElement_WithState
    ChemicalPlayground cp = new ChemicalPlayground();

    String gas = "Gas";
    String liquid = "Liquid";

    //ObservableList with all the compound elements
    ObservableList<CompoundElement> allCompoundElements = cp.getAllCompounds_OrderedName();

    //ObservableList with all the compounds that have an element with the state gas in it.
    ObservableList<CompoundElement> elementsWithStateGas = cp.getCompoundsContainElement_WithState(gas);
    ObservableList<CompoundElement> elementsWithStateLiquid = cp.getCompoundsContainElement_WithState(liquid);

    //ArrayList to put the elements with the state Gas into it.
    ArrayList<CompoundElement> expectedCompoundElementsArrayList = new ArrayList<>();

    //ArrayList with the intent to put the state liquid.
    ArrayList<CompoundElement> elementsWithStateLiquidArrayList = new ArrayList<>();

    //Loop going through all the CompoundElements then the Elements to get the State and if it equals the state Gas then put it into the ArrayList
    for(CompoundElement ce: allCompoundElements){
      for(Element e : ce.getElements()){
        if(e.getState().equals(gas)){
          expectedCompoundElementsArrayList.add(ce);
        }
        if(e.getState().equals(liquid)){
          elementsWithStateLiquid.add(ce);
        }
      }
    }

    //Take the ArrayList and turn it into an ObservableList
    ObservableList<CompoundElement> expectedCompoundElementsWithStateGas = FXCollections.observableArrayList(expectedCompoundElementsArrayList);
    ObservableList<CompoundElement> expectedCompoundElementsLiquid = FXCollections.observableArrayList(elementsWithStateLiquidArrayList);

    //Sort both the list using the CompoundNameComparator to make sure that both list are in the same order.
    elementsWithStateGas.sort(new CompoundNameComparator());
    expectedCompoundElementsWithStateGas.sort(new CompoundNameComparator());
    elementsWithStateLiquid.sort(new CompoundNameComparator());
    expectedCompoundElementsLiquid.sort(new CompoundNameComparator());


    //Assert that the expectedCompoundElements list is the same as the elements WithState list
    assertEquals(expectedCompoundElementsWithStateGas,elementsWithStateGas);

    //Assert that the expectedCompoundElements list is not equal to the elementsWithStateLiquid list.
    assertNotEquals(expectedCompoundElementsWithStateGas, expectedCompoundElementsLiquid);

    //Assert to see if the expectedCompoundElementsLiquid is equal to the elementsWithStateLiquid list.
    assertEquals(expectedCompoundElementsLiquid,elementsWithStateLiquid);

    //Assert to show that the elementsWithStateGas is not equal with the list elementsWithStateLiquid
    assertNotEquals(elementsWithStateGas,elementsWithStateLiquid);

    //Assert to show that the allCompoundElements list is not equal to the expectedCompoundElements
    assertNotEquals(allCompoundElements,expectedCompoundElementsWithStateGas);




  }



  @Test
  @DisplayName("Test getElementsInCommon")
  void testGetElementsInCommon(){

    //Creating a ChemicalPlayground to use the getCompoundsContainElement_WithState
    ChemicalPlayground cp = new ChemicalPlayground();

    ArrayList<Element> elements_1 = new ArrayList<>();
    ArrayList<Element> elements_2 = new ArrayList<>();
    ArrayList<Element> elements_3 = new ArrayList<>();

    // Create and add Hydrogen and Oxygen to elements_1 (Water)
    Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
    Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
    elements_1.add(hydrogen_E1);
    elements_1.add(oxygen_E1);

    // Create and add Hydrogen and Nitrogen to elements_2 (Ammonia)
    Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
    Element nitrogen_E2 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
    elements_2.add(hydrogen_E2);
    elements_2.add(nitrogen_E2);

    //Create and add Silver and Bromine to elements_3 (Silver Bromide)
    Element silver_E3 = new Element(47,"Silver", "Ag", 107.8682,1, 11,1);
    Element bromine_E3 = new Element(35, "Bromine","Br", 79.904, 2,8,1);
    elements_3.add(silver_E3);
    elements_3.add(bromine_E3);

    // Create compound_1 (Water) and compound_2 (Ammonia) and compound_3 (Silver Bromide)
    CompoundElement compound_1 = new CompoundElement("Water", elements_1);
    CompoundElement compound_2 = new CompoundElement("Ammonia", elements_2);
    CompoundElement compound_3 = new CompoundElement("Silver Bromide",elements_3);

    //Make 2 ObservableList one where Hydrogen is the common element and the other where there is no element in common.
    ObservableList<Element> compoundsWithElementInCommonHydrogen = cp.getElementsInCommon(compound_1,compound_2);
    ObservableList<Element> compoundWithNoElementsInCommon = cp.getElementsInCommon(compound_1,compound_3);
    
    //Assert to see if compoundWithNoelementsInCommon is not equal to compoundsWithElementInCommonHydrogen.
    assertNotEquals(compoundWithNoElementsInCommon,compoundsWithElementInCommonHydrogen);




  }






}

