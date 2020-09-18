package laboratory;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class PeriodicTableTest {

  PeriodicTable periodicTable = new PeriodicTable();

  @Test
  @DisplayName("Test GetSolidElements()")
  // The expected behavior of the method is to return an ObservableList containing only elements with a solid state of matter.
  void getSolidElements() {
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
  void getLiquidElements() {
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
  void getUnknownElements() {
    // Create a list which contains all elements, this will be filtered in the test code, and then sorted.
    ObservableList<Element> allElements = periodicTable.getAllElements();

    // Create a list of solid elements from using the method to be tested.
    ObservableList<Element> liquidElements = periodicTable.getUnknownElements();

    // Create an arrayList as you can't make an empty ObservableList.
    ArrayList<Element> expectedElementsArrayList = new ArrayList<>();

    // Filter the allElementsList and element's who's state matches unknown, making sure to standardize case.
    allElements.stream().filter(x -> x.getState().toLowerCase().equals("unknown")).forEach(expectedElementsArrayList::add);

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
}