package laboratory;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.Collections;

import static org.junit.jupiter.api.Assertions.*;

class PeriodicTableTest {

    @Test
    @DisplayName("Test get elements with gas attribute")
    void testGetGasElements() {
        // Create an observable list of expected elements (expecting gas elements)
        // Create observable lists to hold all the elements from the Periodic Table and
        // to hold all of the gas elements by using pt.getGasElements()
        ObservableList<Element> expected_Elements = FXCollections.observableArrayList();
        ObservableList<Element> pt_AllElements = pt.getAllElements();
        ObservableList<Element> pt_GasElements = pt.getGasElements();

        // Check each element for the Gas state and add to expected elements
        for (Element e : pt_AllElements) {
            if (e.getState().equals("Gas")) {
                expected_Elements.add(e);
            }
        }

        // Sort both lists using the same comparator to ensure they are uniform
        Collections.sort(expected_Elements, new ElementNameCompare());
        Collections.sort(pt_GasElements, new ElementNameCompare());

        // Assert that pt_AllElements is not empty
        // Assert that our expected elements list equals the gas elements list
        assertAll(() -> assertFalse(pt_AllElements.isEmpty()),
                  () -> assertEquals(expected_Elements, pt_GasElements));
    }

    @Test
    @DisplayName("Test get element with given symbol")
    void testGetElementBySymbol() {
        // Initialize comparison variables for comparison with those obtained from getElementBySymbol()
        Element element_N = new Element(0, "", "", 0.0, 1, 1);
        Element element_Si = new Element(0, "", "", 0.0, 1, 1);

        // Grab N and Si elements using getElementBySymbol() and assign them to the test variables
        Element pt_ElementN = pt.getElementBySymbol("N");
        Element pt_ElementSi = pt.getElementBySymbol("Si");

        // Check for N and Si elements from the periodic table and assign them to our comparison variables
        for (Element e : pt.getAllElements()) {
            if (e.getSymbol().equals("N")) {
                element_N = e;
            }
            if (e.getSymbol().equals("Si")) {
                element_Si = e;
            }
        }

        // Unchanged comparison variables were needed to run assertEquals
        Element finalElement_N = element_N;
        Element finalElement_Si = element_Si;

        // Assert that the test variables are equal to our comparison variables
        assertAll(() -> assertEquals(finalElement_N, pt_ElementN),
                  () -> assertEquals(finalElement_Si, pt_ElementSi));
    }
  

  @Test
  @DisplayName("Test GetSolidElements()")
  // The expected behavior of the method is to return an ObservableList containing only elements with a solid state of matter.
  void test_getSolidElements() {
    // Create a list which contains all elements, this will be filtered in the test code, and then sorted.
    ObservableList<Element> allElements = periodicTable.getAllElements();

    // Create a list of solid elements from using the method to be tested.
    ObservableList<Element> solidElements = periodicTable.getSolidElements();

    // Create an arrayList as you can't make an empty ObservableList.
    ArrayList<Element> expectedElementsArrayList = new ArrayList<>();

    // Filter the allElementsList and element's who's state matches solid, making sure to standardize case.
    allElements.stream().filter(x -> x.getState().toLowerCase().equals("solid")).forEach(expectedElementsArrayList::add);

    // Transform the arraylist into an ObservableList.
    ObservableList<Element> expectedElements = FXCollections.observableArrayList(expectedElementsArrayList);

    // Sort both lists using the same comparator, in order to make sure that elements are ordered correctly for comparison.
    solidElements.sort(new AtomicNumberComparator());
    expectedElements.sort(new AtomicNumberComparator());

    // Assert that the expectedElements list matches theSolidElements list.
    assertEquals(expectedElements, solidElements);

    // Assert that the expectedElements list is not the same as allElements list.
    assertNotEquals(allElements, expectedElements);
  }

  @Test
  @DisplayName("Test GetLiquidElements()")
    // The expected behavior of the method is to return an ObservableList containing only elements with a liquid state of matter.
  void test_getLiquidElements() {
    // Create a list which contains all elements, this will be filtered in the test code, and then sorted.
    ObservableList<Element> allElements = periodicTable.getAllElements();

    // Create a list of solid elements from using the method to be tested.
    ObservableList<Element> liquidElements = periodicTable.getLiquidElements();

    // Create an arrayList as you can't make an empty ObservableList.
    ArrayList<Element> expectedElementsArrayList = new ArrayList<>();

    // Filter the allElementsList and element's who's state matches liquid, making sure to standardize case.
    allElements.stream().filter(x -> x.getState().toLowerCase().equals("liquid")).forEach(expectedElementsArrayList::add);

    // Transform the arraylist into an ObservableList.
    ObservableList<Element> expectedElements = FXCollections.observableArrayList(expectedElementsArrayList);

    // Sort both lists using the same comparator, in order to make sure that elements are ordered correctly for comparison.
    liquidElements.sort(new AtomicNumberComparator());
    expectedElements.sort(new AtomicNumberComparator());

    // Assert that the expectedElements list matches theSolidElements list.
    assertEquals(expectedElements, liquidElements);

    // Assert that the expectedElements list is not the same as allElements list.
    assertNotEquals(allElements, expectedElements);
  }

  @Test
  @DisplayName("Test GetUnknownElements")
    // The expected behavior of the method is to return an ObservableList containing only elements with a unknown state of matter.
  void test_getUnknownElements() {
    // Create a list which contains all elements, this will be filtered in the test code, and then sorted.
    ObservableList<Element> allElements = periodicTable.getAllElements();

    // Create a list of solid elements from using the method to be tested.
    ObservableList<Element> unknownElements = periodicTable.getUnknownElements();

    // Create an arrayList as you can't make an empty ObservableList.
    ArrayList<Element> expectedElementsArrayList = new ArrayList<>();

    // Filter the allElementsList and element's who's state matches unknown, making sure to standardize case.
    allElements.stream().filter(x -> x.getState().toLowerCase().equals("unknown")).forEach(expectedElementsArrayList::add);

    // Transform the arraylist into an ObservableList.
    ObservableList<Element> expectedElements = FXCollections.observableArrayList(expectedElementsArrayList);

    // Sort both lists using the same comparator, in order to make sure that elements are ordered correctly for comparison.
    unknownElements.sort(new AtomicNumberComparator());
    expectedElements.sort(new AtomicNumberComparator());

    // Assert that the expectedElements list matches theSolidElements list.
    assertEquals(expectedElements, unknownElements);

    // Assert that the expectedElements list is not the same as allElements list.
    assertNotEquals(allElements, expectedElements);
  }
}