package laboratory;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class PeriodicTableTest {

  private PeriodicTable periodicTable;
  private ObservableList<Element> allElements;
  private ObservableList<Element> expectedElements;

  @BeforeEach
  public void init(){
    // Although we aren't changing the periodicTable, we want to make sure a fresh copy is made every time.
    periodicTable = new PeriodicTable();
    // Create a list which contains all elements, this will be filtered in the test code, and then sorted.
    // This is created before each method is run to keep the method call in a single place.
    allElements = periodicTable.getAllElements();
    expectedElements = FXCollections.observableArrayList();
  }

  @Test
  @DisplayName("Test getElementBySymbol()")
  void test_getElementBySymbol() {
    // Initialize comparison variables for comparison with those obtained from getElementBySymbol()
    Element element_N = new Element(0, "", "", 0.0, 1, 1);
    Element element_Si = new Element(0, "", "", 0.0, 1, 1);

    // Grab N and Si elements using getElementBySymbol() and assign them to the test variables
    Element pt_ElementN = periodicTable.getElementBySymbol("N");
    Element pt_ElementSi = periodicTable.getElementBySymbol("Si");

    // Check for N and Si elements from the periodic table and assign them to our comparison variables
    for (Element e : periodicTable.getAllElements()) {
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
  @DisplayName("Test getSolidElements()")
    // The expected behavior of the method is to return an ObservableList containing only elements with a solid state of matter.
  void test_getSolidElements() {
    // Create a list of solid elements from using the method to be tested.
    ObservableList<Element> solidElements = periodicTable.getSolidElements();

    // Filter the allElementsList and element's who's state matches solid, making sure to standardize case.
    allElements.stream().filter(x -> x.getState().toLowerCase().equals("solid")).forEach(expectedElements::add);

    // Sort both lists using the same comparator, in order to make sure that elements are ordered correctly for comparison.
    solidElements.sort(new AtomicNumberComparator());
    expectedElements.sort(new AtomicNumberComparator());

    // Assert that the expectedElements list matches theSolidElements list.
    assertEquals(expectedElements, solidElements);

    // Assert that the expectedElements list is not the same as allElements list.
    assertNotEquals(allElements, expectedElements);
  }

  @Test
  @DisplayName("Test getLiquidElements()")
    // The expected behavior of the method is to return an ObservableList containing only elements with a liquid state of matter.
  void test_getLiquidElements() {
    // Create a list of solid elements from using the method to be tested.
    ObservableList<Element> liquidElements = periodicTable.getLiquidElements();


    // Filter the allElementsList and element's who's state matches liquid, making sure to standardize case.
    allElements.stream().filter(x -> x.getState().toLowerCase().equals("liquid")).forEach(expectedElements::add);

    // Sort both lists using the same comparator, in order to make sure that elements are ordered correctly for comparison.
    liquidElements.sort(new AtomicNumberComparator());
    expectedElements.sort(new AtomicNumberComparator());

    // Assert that the expectedElements list matches theSolidElements list.
    assertEquals(expectedElements, liquidElements);

    // Assert that the expectedElements list is not the same as allElements list.
    assertNotEquals(allElements, expectedElements);
  }

  @Test
  @DisplayName("test GetGasElements()")
  void test_getGasElements() {
    // Create an observable list to hold result from getGasElements
    ObservableList<Element> pt_GasElements = periodicTable.getGasElements();

    // Check each element for the Gas state and add to expected elements
    for (Element e : allElements) {
      if (e.getState().equals("Gas")) {
        expectedElements.add(e);
      }
    }

    // Sort both lists using the same comparator to ensure they are uniform
    expectedElements.sort(new ElementNameCompare());
    pt_GasElements.sort(new ElementNameCompare());

    // Assert that pt_AllElements is not empty
    // Assert that our expected elements list equals the gas elements list
    assertAll(() -> assertFalse(allElements.isEmpty()),
        () -> assertEquals(expectedElements, pt_GasElements));
  }

  @Test
  @DisplayName("Test getUnknownElements()")
    // The expected behavior of the method is to return an ObservableList containing only elements with a unknown state of matter.
  void test_getUnknownElements() {
    // Create a list of solid elements from using the method to be tested.
    ObservableList<Element> unknownElements = periodicTable.getUnknownElements();

    // Filter the allElementsList and element's who's state matches unknown, making sure to standardize case.
    allElements.stream().filter(x -> x.getState().toLowerCase().equals("unknown")).forEach(expectedElements::add);

    // Sort both lists using the same comparator, in order to make sure that elements are ordered correctly for comparison.
    unknownElements.sort(new AtomicNumberComparator());
    expectedElements.sort(new AtomicNumberComparator());

    // Assert that the expectedElements list matches theSolidElements list.
    assertEquals(expectedElements, unknownElements);

    // Assert that the expectedElements list is not the same as allElements list.
    assertNotEquals(allElements, expectedElements);
  }
}