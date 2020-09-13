package laboratory;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.stream.Collectors;

import static org.junit.jupiter.api.Assertions.*;

class PeriodicTableTest {

  PeriodicTable periodicTable = new PeriodicTable();

  @Test
  void getSolidElements() {
    ObservableList<Element> allElements = periodicTable.getAllElements();
    ObservableList<Element> solidElements = periodicTable.getSolidElements();
    ArrayList<Element> expectedElementsArrayList = new ArrayList<>();
    allElements.stream().filter(x -> x.getState().charAt(0) == 1).forEach(expectedElementsArrayList::add);
    ObservableList<Element> expectedElements = FXCollections.observableArrayList(expectedElementsArrayList);
    allElements.sort(new AtomicNumberComparator());
    expectedElements.sort(new AtomicNumberComparator());
    assertEquals(solidElements, expectedElements);
    //assertNotEquals(allElements, expectedElements);
  }

  @Test
  void getLiquidElements() {

  }

  @Test
  void getUnknownElements() {

  }
}