package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class ChemicalPlaygroundTest {
  @Test
  @DisplayName("Test to create an element from a given formula")
  void test_CreateElementFromFormula(){
    //creating a ChemicalPlayground object to call the createElementFromFormula method
    ChemicalPlayground cp = new ChemicalPlayground();
    String compounds = cp.importCompoundsFromFile();
    //creating a String for the formula for water H2O
    String formula = "H2O";
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
}
